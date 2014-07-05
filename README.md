APM-Log-File-Analyzer
=====================

'v0.1 BETA 21/04/2014 - First Issue on Net.
'   BugFix: TheQuestor reported issues with US / UK date conversions, issued an update under the same version number 
'           but TheQuestor reports still the same. This maybe the "Expired" flaging in the LFA file - Released
'
'v0.2 BETA 22/04/2014 - Released
'           Issued a new issue that can write a new LFA file when a new version number of program is detected.
'           Also altered the Dates calculations again in an attempt to fix TheQuestors issue.
'
'v0.21 BETA 22/04/2014
'           Changed the Filename Template from ????-??-?? ??-??*.log to *.log, this should allow any log file
'           to be opened - Requested by JNJO
'           
'v0.3 BETA 23/04/20114 - Released
'           Altered the Dates calculations again in an attempt to fix TheQuestors issue, now using a Val(yyyyMMdd) method.
'           Removed the Date Formatting bug, dd/mm/yyyy is now dd/MM/yyyy.
'
'v1.0.0.0   02/05/2014 - Released on Forum and the installer
'           Added the new code to do automatic updates, this part checks for updates and brings the file back to local machine.
'           Removes all the old expiry code and LFA file generation code.
'           Added code to clean up any LFA files that previous versions may have created.
'
'v1.0.0.1   02/05/2014 - Released on Server
'           Addded the Successfully updated to the latest version!" message just to prove the updater works.
'
'v1.0.0.2   03/05/2014 - Released on Server
'           Added the vibration analysis code.
'               - Data is only analysed when we have 500 successive readings (10 seconds) while the UAV is above
'                 3 meters to eliminate ground wash and <1 meter per second ground speed (may need to be extended for planes) 
'               - X and Y need to be within the -3 to +3 with Z within -15 to -5 for the summary to report
'                 "Excellent: Levels of vibration detected are within recommended limits."
'               - Levels outside these limit will issue a warning 
'                 "WARNING: Level of vibrations are above recommended values."
'               - Where X and Y are over -5 to +5 with Z within -20 to 0 a different warning is displayed.
'                 "WARNING: HIGH Levels of Vibration Detected, recommended not to fly!"
'v1.0.0.3   03/05/2014 - Released on Server
'           BUG FIX: Coding erorr on the Y and Z vibration results
'           Added the Standard Deviation from the Average result to the Vibration Results.
'
'v1.0.0.4   03/05/2014 - Released on Server
'           Added the filtering on the Vibration Checks
'           Added the DataLine reference that were used for the Vibration Check.
'
'v1.0.0.5   03/05/2014 - Released on Server
'           BUG FIX: fix a bug that caused an update each time the user selected to search for updates.
'           Commented out some code that allows users to see the FTP server name.
'
'v1.0.0.6   03/05/2014 - Released on Server
'           BUG FIX: fix a bug that caused the flight times to be incorrect where a user loading or reads a second log.
'
'v1.0.0.7   04/05/2014 - Released on Server
'           Added some checks to ensure the log file being read is compatible with the program.
'               - Check that GPS and CTUN are being log, where not display message and exit log reading.
'               - Check the log is >= version 3.1, where not display message and exit log reading.
'               - Check the log was created by an ArduCopter, where not display message and exit log reading.
'           Changed text box type to a rich text box with Hyperlinks.
'
'v1.0.0.8   04/05/2014 - Released on Server 
'           Added a progress bar for the file read status.
'           Started adding some code for Planes.
'               - Dection of the "ArduPlane" log type.
'               - Mode Change, and summary section.
'           Added code to calculate altitude from GPS data if BarAlt is not available.
'
'v1.0.0.9   05/05/2014 - Released on Server
'           Added warnings for HDop < 2 and Satellites < 9 for both mode and overall summaries.
'           Added Brown Out Detection code.
'           Started looking at the ATT and NTUN Pitch and Roll data, debug info is in but it is complicated.
'           Added some colours for the formatting of diplayed data.
'
'v1.0.0.10  08/05/2014 - Released on Server
'           Changed the Eff description to (mA/mi) from (mA/mi)
'           Changed the reporting of Vibration in Overall Summary to look at IMU_Vibration_Check = true instead on DLs>=500 to allow changing.
'           Due to a report that French Canadian did not work I added some Culture Detection Code.
'           BUG: Update code does not work as expected.
'
'v1.0.1.0   08/05/2014 - Released on Server
'           Indentical to above.
'
'v1.0.1.1   08/05/2014 - Released on Server
'           Added the code to force the Thread to us en-GB Culture and UICulture to keep the programming simple.
'
'v1.0.1.2   10/05/2014 - Released on Server
'           Adding some resilience to both the 1st & 2nd passes of the log file to ensure values are within expectations.
'           Added the Log File Corruption Handled label along with basic file corruption code 
'           Added the abiltiy to automatically display a Vibration Chart where vibrations are higher than allowed.
'           Bug FIx: that caused GPS Distances and Times to be incorrect when AUTO mode takes off very quickly.
'           Tested the AutoTune mode reporting.
'
'v1.0.1.3   13/05/2014 - Released on Server
'           Bug Fix: Removed filtering from the Vibration Alt Code that sometimes failed to get min altitude down in time.
'           Bug Fix: Eff error after landing and taking off again.
'           WARNING has been added when > 80% of the capacity has been used.
'           Bug Fix: when trying to handle an APM detected GPS Glitch
'           Changed code to ignore GPS data while there are currently GPS issues!
'           Added the ability to change the vibration detection parameter on the main screen, also a Force Chart button has been added.
'           WARNING has been added to inform the user the Current Sensor may be faulty.
'   
'
'v1.0.1.4   16/05/2014 - Released on Server
'           WARNING has been added if the Log File Version is a RC (Release Candidate) rather than an official release.
'           Basic support added for V3.2 (RC) files, there may be some issues though with data layouts.
'
'v1.0.1.5   18/05/2014 - Released on Server
'           Added the PM Checking Code.
'           Fixed Battery Capacity Bug.
'
'v1.0.1.6   08/06/2014 - Released on Server
'           Updated the spelling of Determine, or at least I think I have as I can not find any references to the miss spelling.
'           INAVerr code has been removed as it does not seem useful in real world testing.
'           Added a check to ensure the current Log_In_Flight status makes sense, 
'               if the vehicle altitude is higher than the ground and throttle is out then the take_off code is executed.
'               this solves the issues with 2014-06-01 07-13-53.log where the take off event was missing. 
'           Added a log file error counter near the progress bar.
'           Suppress the "Not_Landed" event from the display where it follows the "Take_off" event.
'           Add some testing data to the remaining "Not_Landed" event to see if this is useful data or should be removed.
'           Added code the suppress GCS issues until we have had at least one 100% signal, also informs user when GCS is detected for the first time.
'           Added charts for Altitude and Speed to the vibration charts and also increased the data collection by 900%
'           Removed the ability to alter the Vibration Detection Parameters, these are now set at speed<40m/s and alt>0m in flight.
'           Vibration Averages have been removed while I get time to work on them.
'           Added compatibility for V3.1.5 logs
'           Updated some screen formatting issues 
'
'v1.0.1.7   14/06/2014 - Released on Server
'           Changed the way the "LOG Analyser" Take_Off code detects a take off. it now needs to see the throttle at 40% not just up, and Alt @ +0.5m above ground.
'           Added the RCG Code Copy function to the edit menu.
'
'v1.0.1.8   14/06/2014 - Released on Server
'           Added the DU32 code.
'           Killed the NOT_LANDED display
'           Added the Esc Key to abort the Analysis
'           Added the auto scroll to the analysis window.
'
'v1.0.1.9   28/06/2014 - Released on Server
'           Added some basic CMD checks for the auto flights.
'           Removed the Check of Battery Capacity against the Model Capacity - put back when we add models
'           Added GPS co-ords when Mode Changes
'           Added GPS co-ords when Home is Set (note this is based on last GPS co-ords, actual set home in APM could be different)
'               If NTUN is available then we try to validate the Home Posistion against the Current Position.
'           Frame Type Added, determined from PARM FRAME
'           Number of Motors determined from FMT MOT line.
'
'v1.0.2.0   28/06/2014 - Released on Server
'           Battery Capacity Bug Fix, because I had removed the check against the model.
'
'v1.0.2.1
'           Added detection when an Auto Mission is struggling to maintain the desired altitude.
'           Added a safety warning when a mission does not start with a take off.
'           Altered the AUTO mission formatting on screen to make it more readable and understandable.
'           Deactivated the INAV code until I work some more on it.
'           Changed TABS to 2 spaces to help code flow.
'v1.0.2.2
'Craig's Additions
'           Added Form position memory. Will remember where form was last closed no matter which window and what location.
'           Redisgned the interface to better match Mission Planner
'           Created ToolTips for each option
'           Finalized initial GUI layout. Still need to figure out a few things as they no longer display after being moved.
'v1.0.2.3
'Craig's Additions
'           Changed copy to clip so it can be called via anything. Since I am hiding the menu strip I need to be able to  
'           call it's info via buttons
'           Moved Help/About into its own sub for the same reason and added a button to call it
'           Moved Update Now into its own sub for the same reason and added a button to call it
'           All items present in the now hidden toolstrip are available via large buttons at top of form

'v1.0.2.4
'Craig's Additions
'           Added a little joke :) MCB
'v1.0.2.5
'Craig's Additions
'           Added a donate button 
'           Redid the cowbell wav to add actual cowbells :)
'           changed the black text output to white to deal with the new background color.
'           Added more ToolTips for all buttons and checkboxes
'

'           ...
' ##TODO##
'Because there is no EV logs in my Plane sample then the Take Off is not recorded, because of this there are no mode times.
'Finish the Pitch and Roll stuff for crash detection.
'Look at Mission Planner Code to see how they download logs from the APM.
'Need to check FMT for any "New" entries that I do not know about and report back when found.
'Need more v3.2 files from reliable source before continuing.
'Write detected errors to FTP, upload log if user agrees.

'Can I use The Questors Failing GPS logs to give users better information that the GPS unit is faulty?
'	TQ just changed the unit out.

'Resize the text window when the Window changes size, or stop the window changing size?

'Calculate Spd Eff (mi/A) and create Spd % over flight within Ranges, then calculate the eff within each range.


'Add support for Planes - I am still investigating whether this is possible
'   - need to find a take off and land solution if EV in not available for planes

'On TX/RX issue we have direction of UAV but need to reference where the Pilot is.
'   - I have a quad that can fly away from me to 1/2 mile, however turn to face me at that distance a RTL kicks in
'   - Knowing these angles could help placement of RX aerials to reduce such issues

'Detect if Battery Voltage / Capacity calibration has been performed, need to know default parameter for BATT_AMP_PERVOLT & BATT_AMP_OFFSET

