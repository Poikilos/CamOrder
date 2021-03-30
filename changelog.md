# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).


## [git] - 2021-03-30
### Changed
- Rename the project from IntegratorEduImport to CamOrder.
  - Change the name in files.
  - Rename files.
- Move the changes from the readme to this changelog (and improve the formatting and wording).
- Use `%APPDATA%` in blnk files.


## [unreleased] - 2017-12-01
### Fixed
- Improve destination listing to more often check if "Exists" at appropriate times.
- Fix startup (start timer tick) crash when xml file is not on a theoretical destination.


## [unreleased] - 2017-11-30
### Added
- Add a monodevelop project.
  - Create an empty project in .NET category under "Other", named IntegratorEduImport_monodevelop
  - Move the files to IntegratorEduImport
  - Add relevant cs files including AssemblyInfo.cs in Properties folder
  - Add assembly references as needed
  - Resolve "could not find any resources for the specified culture" when setting icon at runtime:
    Right-click project, Options, change default namespace to ExpertMultimedia

### Changed
- For "Copy command" drop-down box, initially select last command where any file exists (instead of just selecting first command in list).

### Fixed
- Finish `split_respecting_quotes`.
- If the source doesn't exist (or other exception accessing source), clear the input list.
- Change ffmpeg option from `-y` to `-nostdin` to avoid any stopping.


## [unreleased] - 2017-10-27
### Fixed
- Clear the Camera Name on "Reload Camera".
- Clear the batch listbox on "Reload Camera".


## [unreleased] - 2016-12-16
## Fixed
- If exception double-clicking, show dialog that file is missing instead of just showing message in status bar:
  ```
Could not finish starting file "M:\AVCHD\BDMV\STREAM\01166.MTS":System.ComponentModel.Win32Exception: The system cannot find the drive specified
   at System.Diagnostics.Process.StartWithShellExecuteEx(ProcessStartInfo startInfo)
   at System.Diagnostics.Process.Start()
   at System.Diagnostics.Process.Start(ProcessStartInfo startInfo)
   at System.Diagnostics.Process.Start(String fileName)
   at ExpertMultimedia.MainForm.InputListViewDoubleClick(Object sender, EventArgs e)
```


## [unreleased] - 2016-09-29
## Fixed
- Change from `inputListView.Clear()` to `inputListView.Items.Clear()` (resolves: Clear reload camera not working)


## [unreleased] - 2015-11-24
### Fixed
- Reload the settings object when reloading the source (from xml that was saved to device) even if it exists.


## [unreleased] - 2015-11-10
### Fixed
- Reload source should call `RefreshCamera()` to refresh the device label from the settings file on the device (so that finished videos can be determined when marking unprocessed files runs).
