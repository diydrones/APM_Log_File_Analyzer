Imports uint8_t = System.Byte
Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Text
Imports System.Collections.Generic
Imports System

Module modBinFileConversion

    Public logformat As New Dictionary(Of String, log_Format)()

    Public Structure log_Format
        Public type As uint8_t
        Public length As uint8_t
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)> _
        Public name As Byte()
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=16)> _
        Public format As Byte()
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=64)> _
        Public labels As Byte()
    End Structure


    ' This part is Key, once the in file and out file are known we jump here.
    ' It then keeps reading a line and processes the data using the ReadMessage procedure below.
    Public Sub ConvertBin(inputfn As String, outputfn As String)
        Using stream As Object = File.Open(outputfn, FileMode.Create)
            Using br As New BinaryReader(File.OpenRead(inputfn))
                Dim MyMSGdone As Boolean = False
                Dim data As Byte()
                While br.BaseStream.Position < br.BaseStream.Length
                    Dim MyTemp As String = ReadMessage(br.BaseStream)

                    If Mid(MyTemp, 1, 3) <> "FMT" And MyMSGdone = False Then
                        ' Write my Bin Converter MSG at the top to identify this was converted by my Converter and not Mission Planner.
                        Dim MyMSG = "MSG, 0, APM Log File Analyser BinToLog Converter " & MyCurrentVersionNumber & vbNewLine
                        data = ASCIIEncoding.ASCII.GetBytes(MyMSG)
                        stream.Write(data, 0, data.Length)
                        MyMSGdone = True
                    End If

                    data = ASCIIEncoding.ASCII.GetBytes(MyTemp)
                    stream.Write(data, 0, data.Length)
                End While
            End Using
        End Using
    End Sub


    Public Function ReadMessage(br As Stream) As String
        Dim log_step As Integer = 0

        While br.Position < br.Length

            ' ## CHANGED ##
            'Dim data As Byte = DirectCast(br.ReadByte(), Byte)
            Dim data As Byte = System.Convert.ToByte(br.ReadByte())

            ' The first two bytes seems to be ignored as they are HEAD_BYTE1 & HEAD_BYTE2

            Select Case log_step
                Case 0
                    If data = HEAD_BYTE1 Then
                        log_step += 1
                    End If
                    Exit Select

                Case 1
                    If data = HEAD_BYTE2 Then
                        log_step += 1
                    Else
                        log_step = 0
                    End If
                    Exit Select

                Case 2
                    log_step = 0

                    Try

                        ' The next statement is Key, it seems to work out how the Log DataLine should be formatted
                        Dim line As String = logEntry(data, br)

                        Return line
                    Catch
                        Debug.Print("Bad Binary log line {0}", data)
                    End Try
                    Exit Select
            End Select
        End While

        Return ""
    End Function


    ''' <summary>
    ''' Process each log entry
    ''' </summary>
    ''' <param name="packettype">packet type</param>
    ''' <param name="br">input file</param>
    ''' <returns>string of converted data</returns>
    Public Function logEntry(packettype As Byte, br As Stream) As String

        Select Case packettype

            Case &H80
                ' FMT
                Dim logfmt As New log_Format()

                Dim obj As Object = logfmt

                Dim len As Integer = Marshal.SizeOf(obj)

                Dim bytearray As Byte() = New Byte(len - 1) {}

                br.Read(bytearray, 0, bytearray.Length)

                Dim i As IntPtr = Marshal.AllocHGlobal(len)

                ' create structure from ptr
                obj = Marshal.PtrToStructure(i, obj.[GetType]())

                ' copy byte array to ptr
                Marshal.Copy(bytearray, 0, i, len)

                obj = Marshal.PtrToStructure(i, obj.[GetType]())

                Marshal.FreeHGlobal(i)

                logfmt = DirectCast(obj, log_Format)

                Dim lgname As String = ASCIIEncoding.ASCII.GetString(logfmt.name).Trim(New Char() {ControlChars.NullChar})
                Dim lgformat As String = ASCIIEncoding.ASCII.GetString(logfmt.format).Trim(New Char() {ControlChars.NullChar})
                Dim lglabels As String = ASCIIEncoding.ASCII.GetString(logfmt.labels).Trim(New Char() {ControlChars.NullChar})

                logformat(lgname) = logfmt

                ' ## CHANGED ## 
                'Dim line As String = [String].Format("FMT, {0}, {1}, {2}, {3}, {4}" & vbCr & vbLf, logfmt.type, logfmt.length, lgname, lgformat, lglabels)
                Dim line As String = [String].Format("FMT, {0}, {1}, {2}, {3}, {4}" & vbCr & vbLf, logfmt.type, logfmt.length, lgname, lgformat, lglabels)

                Return line
            Case Else

                Dim format As String = ""
                Dim name As String = ""
                Dim size As Integer = 0

                For Each fmt As log_Format In logformat.Values
                    If fmt.type = packettype Then
                        name = ASCIIEncoding.ASCII.GetString(fmt.name).Trim(New Char() {ControlChars.NullChar})
                        format = ASCIIEncoding.ASCII.GetString(fmt.format).Trim(New Char() {ControlChars.NullChar})
                        size = fmt.length

                        If name <> "PARM" Then
                            ' Breakpoint
                            name = name
                        End If

                        Exit For
                    End If
                Next

                ' didnt find a match, return unknown packet type
                If size = 0 Then
                    Return "UNKW, " + packettype
                End If

                Dim data As Byte() = New Byte(size - 4) {}
                ' size - 3 = message - messagetype - (header *2)
                br.Read(data, 0, data.Length)

                If name <> "PARM" Then
                End If

                Return ProcessMessage(data, name, format)
        End Select
    End Function


    '  
    '    105    +Format characters in the format string for binary log messages  
    '    106    +  b   : int8_t  
    '    107    +  B   : uint8_t  
    '    108    +  h   : int16_t  
    '    109    +  H   : uint16_t  
    '    110    +  i   : int32_t  
    '    111    +  I   : uint32_t  
    '    112    +  f   : float  
    '    113    +  N   : char[16]  
    '    114    +  c   : int16_t * 100  
    '    115    +  C   : uint16_t * 100  
    '    116    +  e   : int32_t * 100  
    '    117    +  E   : uint32_t * 100  
    '    118    +  L   : uint32_t latitude/longitude  
    '    119    + 


    ''' <summary>
    ''' Convert to ascii based on the existing format message
    ''' </summary>
    ''' <param name="message">raw binary message</param>
    ''' <param name="name">Message type name</param>
    ''' <param name="format">format string containing packet structure</param>
    ''' <returns>formated ascii string</returns>
    Public Function ProcessMessage(message As Byte(), name As String, format As String) As String
        Dim form As Char() = format.ToCharArray()

        Dim offset As Integer = 0

        Dim line As New StringBuilder(name)

        For Each ch As Char In form

            'Debug.Print("Processing Message: " & ch & " Converting: " & message(offset))

            Select Case ch
                Case "b"
                    ' ## CHANGED ##
                    ' WARNING - b can not always be changed to a SBYTE, use byte instead.
                    'line.Append(", " + DirectCast(message(offset), SByte))

                    If Mid(line.ToString, 1, 4) = "UBX2" Then
                        ' BreakPoint
                        Dim a As String = ""
                    End If

                    ' Get Signed Byte (SBYTE) this way as the inbuilt convert.tosbyte is not working!
                    Dim MySbyte As SByte = 0
                    If message(offset) < 128 Then
                        MySbyte = message(offset)
                    Else
                        MySbyte = message(offset) - 256
                    End If

                    line.Append(", " & MySbyte)

                    'line.Append(", " & System.Convert.ToByte(message(offset)))

                    offset += 1
                    Exit Select
                Case "B"
                    line.Append(", " & message(offset))
                    offset += 1
                    Exit Select
                Case "h"
                    line.Append(", " + BitConverter.ToInt16(message, offset).ToString(System.Globalization.CultureInfo.InvariantCulture))
                    offset += 2
                    Exit Select
                Case "H"
                    line.Append(", " + BitConverter.ToUInt16(message, offset).ToString(System.Globalization.CultureInfo.InvariantCulture))
                    offset += 2
                    Exit Select
                Case "i"
                    line.Append(", " + BitConverter.ToInt32(message, offset).ToString(System.Globalization.CultureInfo.InvariantCulture))
                    offset += 4
                    Exit Select
                Case "I"
                    line.Append(", " + BitConverter.ToUInt32(message, offset).ToString(System.Globalization.CultureInfo.InvariantCulture))
                    offset += 4
                    Exit Select
                Case "q"
                    line.Append(", " + BitConverter.ToInt64(message, offset).ToString(System.Globalization.CultureInfo.InvariantCulture))
                    offset += 8
                    Exit Select
                Case "Q"
                    line.Append(", " + BitConverter.ToUInt64(message, offset).ToString(System.Globalization.CultureInfo.InvariantCulture))
                    offset += 8
                    Exit Select
                Case "f"
                    line.Append(", " + BitConverter.ToSingle(message, offset).ToString(System.Globalization.CultureInfo.InvariantCulture))
                    offset += 4
                    Exit Select
                Case "d"
                    line.Append(", " + BitConverter.ToDouble(message, offset).ToString(System.Globalization.CultureInfo.InvariantCulture))
                    offset += 8
                    Exit Select
                Case "c"
                    line.Append(", " + (BitConverter.ToInt16(message, offset) / 100.0).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture))
                    offset += 2
                    Exit Select
                Case "C"
                    line.Append(", " + (BitConverter.ToUInt16(message, offset) / 100.0).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture))
                    offset += 2
                    Exit Select
                Case "e"
                    line.Append(", " + (BitConverter.ToInt32(message, offset) / 100.0).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture))
                    offset += 4
                    Exit Select
                Case "E"
                    line.Append(", " + (BitConverter.ToUInt32(message, offset) / 100.0).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture))
                    offset += 4
                    Exit Select
                Case "L"

                    ' ### CHANGED  - Validated ###
                    'line.Append(", " + (DirectCast(BitConverter.ToInt32(message, offset), Double) / 10000000.0).ToString(System.Globalization.CultureInfo.InvariantCulture))
                    line.Append(", " & (System.Convert.ToDouble(BitConverter.ToInt32(message, offset)) / 10000000.0).ToString(System.Globalization.CultureInfo.InvariantCulture))

                    offset += 4
                    Exit Select
                Case "n"
                    line.Append(", " + ASCIIEncoding.ASCII.GetString(message, offset, 4).Trim(New Char() {ControlChars.NullChar}))
                    offset += 4
                    Exit Select
                Case "N"
                    line.Append(", " + ASCIIEncoding.ASCII.GetString(message, offset, 16).Trim(New Char() {ControlChars.NullChar}))
                    offset += 16
                    Exit Select
                Case "M"
                    Dim modeno As Integer = message(offset)

                    Dim currentmode As String = ""

                    Select Case modeno
                        ' Modes can be found in a local file once mission planner has been executed.
                        ' C:\Program Files (x86)\Mission Planner\ParameterMetaData.xml
                        ' Search for <FLTMODE1>
                        ' 0:Stabilize,1:Acro,2:AltHold,3:Auto,4:Guided,5:Loiter,6:RTL,7:Circle,9:Land,11:Drift,13:Sport,14:Flip,15:AutoTune,16:PosHold,17:Brake</Values>
                        Case 0
                            currentmode = "Stabilize"
                            Exit Select

                        Case 1
                            currentmode = "Acro"
                            Exit Select

                        Case 2
                            currentmode = "AltHold"
                            Exit Select

                        Case 3
                            currentmode = "Auto"
                            Exit Select

                        Case 4
                            currentmode = "Guided"
                            Exit Select

                        Case 5
                            currentmode = "Loiter"
                            Exit Select

                        Case 6
                            currentmode = "RTL"
                            Exit Select

                        Case 7
                            currentmode = "Circle"
                            Exit Select

                        Case 9
                            currentmode = "Land"
                            Exit Select

                        Case 10
                            currentmode = "OF_Loiter"
                            Exit Select

                        Case 11
                            currentmode = "Drift"
                            Exit Select

                        Case 13
                            currentmode = "Sport"
                            Exit Select

                            ' 14:Flip,15:AutoTune,16:PosHold,17:Brake
                        Case 14
                            currentmode = "Flip"
                            Exit Select

                        Case 15
                            currentmode = "AutoTune"
                            Exit Select

                        Case 16
                            currentmode = "PosHold"
                            Exit Select

                        Case 17
                            currentmode = "Brake"
                            Exit Select

                        Case Else
                            currentmode = "UNKNOWN -- Mode No. = " & modeno
                            Exit Select

                    End Select

                    line.Append(", " + currentmode)
                    offset += 1
                    Exit Select
                Case "Z"
                    line.Append(", " + ASCIIEncoding.ASCII.GetString(message, offset, 64).Trim(New Char() {ControlChars.NullChar}))
                    offset += 64
                    Exit Select
                Case Else
                    Return "Bad Conversion"
                    Exit Select
            End Select
        Next

        line.Append(vbCr & vbLf)
        Return line.ToString()
    End Function

End Module
