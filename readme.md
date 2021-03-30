# IntegratorEduImport

## Key Features:
* keeps a record of files already copied, and prevents redoing


## Known Issues:
* Make commands keyed by suffix (instead of having suffix based on camera)
* Removes source from list if ffmpeg has a protocol error (such as smb://) but they should be added to the failed list so they will be added back in after the list is cleared
* If source name is different when doing Reload Source, batch should be cleared.
* If output file exists and is >0KB, the line should be removed from batch
* If the file already exists, warning should be issued before adding to batch (or running?)
* should not throw Exception if command is copy instead of ffmpeg
* Remove 720p30 suffix if command is copy
* Should account for camera being unplugged before clicking any button where camera presence is required (but allow you to mark files as done/unusable still)
* (save&load commands.txt in standardized location) shouldn't throw error when has commands.txt but working directory is set incorrectly: 'No copy commands! Please add to "commands.txt" e.g. add the line'
* detect whether already running
* Fix refresh after unplugging camera when 2 were plugged in (behavior is that settings such as tbPrependCam.Text do not refresh)
* allow adding to batch even if did already (currently prevents import even after moving file if was imported before, so probably has a marker)
* require project name (?)
* finish mark unusable and check whether unusable methods: check for existing files (or markers) etc
	* two buttons: mark as unusable, add to project, clear markers
		* each is color coded
	* only mark as imported if batch has been run
* mark files as processed even if only imported, in case files are deleted later (on test cameras, should start from CamA 507 and CamB 466)
* If present after converting and >0 bytes, remove from batch
* should more reliably mark which files are finished (find out why not; may be related to DST or other time issue)
* For each line, confirm overwrite if already present on dest with EXACT given filename (not just same source)

## Changes
* (2017-12-01) improved destination listing to make more "Exists" checks at appropriate times
    * (2017-12-01) fixed startup (start timer tick) crash when xml file is not on a theoretical destination
* (2017-11-30) for "Copy command" drop-down box, initially select last command where any file exists (instead of just selecting first command in list)
	* finish split_respecting_quotes
* (2017-11-30) if source doesn't exist (or other exception accessing source), clear input list
* (2017-11-30) changed ffmpeg option from -y to -nostdin to avoid any stopping
* (2017-11-30) added monodevelop project
    * created empty project in .NET category under "Other", named IntegratorEduImport_monodevelop
    * moved the files to IntegratorEduImport
    * added relevant cs files including AssemblyInfo.cs in Properties folder
    * added assembly references as needed
    * still had "could not find any resources for the specified culture" when setting icon
    * right-clicked project, Options, changed default namespace to ExpertMultimedia (resolved)
* (2017-10-27) clear Camera Name on "Reload Camera"
* (2017-10-27) clear batch listbox on "Reload Camera"
* (2016-12-16) If exception double-clicking, show dialog that file is missing instead of just showing message in status bar: Could not finish starting file "M:\AVCHD\BDMV\STREAM\01166.MTS":System.ComponentModel.Win32Exception: The system cannot find the drive specified
   at System.Diagnostics.Process.StartWithShellExecuteEx(ProcessStartInfo startInfo)
   at System.Diagnostics.Process.Start()
   at System.Diagnostics.Process.Start(ProcessStartInfo startInfo)
   at System.Diagnostics.Process.Start(String fileName)
   at ExpertMultimedia.MainForm.InputListViewDoubleClick(Object sender, EventArgs e)
* (2016-09-29) Clear reload camera not working (RESOLVED: changed from inputListView.Clear() to inputListView.Items.Clear())
* (2015-11-24) Reload Source should reload settings object (from xml that was saved to device) even if exists
* (2015-11-10) Reload source should RefreshCamera device label from settings file on device (so that finished videos can be determined when marking unprocessed files runs)

## Developer Notes
* YML file names are generated based on UTC time. This is necessary since LastWriteTime of files will DIFFER depending on whether program read the file during DST or not--thus if LastWriteTime were used (as opposed to LastWriteTimeUtc), the program would not be able to tell whether the video file had a corresponding metadata (yml) file.

## Video File Size Notes

### Stats for convert upon import:
1440*1080=1555200 pixels original
6040Kbps@LP original (29.97fps, 256Kbps AVC audio)
e.g. 00507.MTS on CamA
1280*720=921600 pixels as imported
921600/1555200=0.5925925925925926
0.5925925925925926*6040=3579.259259259259Kbps expected bitrate @720x480
2910Kbps actual bitrate as imported
@1280x720 30.000 fps MPEG Video (Version 2) 
(Main@High 1440) (HDV 720p)
using:
"C:\Program Files (x86)\WinFF\ffmpeg.exe" -y -i "%INFILE%" -r 30 -s 1280x720 -q:v 4 -vcodec mpeg2video -acodec copy -f mpegts "%OUTFILE%"
e.g. 2013-03-02 18_18_01 CamA-00498 720p30.MTS
