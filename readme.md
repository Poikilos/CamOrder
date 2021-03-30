# CamOrder
Import and organize media from your camera in one step. This program was formerly named IntegratorEduImport before released. 

## Key Features
* Keep a record of files already copied to prevent re-importing.


## Known Issues
See <https://github.com/poikilos/CamOrder/issues> and ensure that the issue isn't listed there (or under "Closed") before reporting it there using a GitHub username. If the issue appears when you click "Closed", the program must be upgraded to take advantage of the new feature or fix.


## Changes
See changelog.


## Developer Notes
* The project is designed and tested for C# 5.0 and Mono or .NET Framework 4.0 (The project used SharpDevelop 3.2.1.6466 along with C# 3.0 and .NET Framework 3.5 before <https://github.com/poikilos/CamOrder/commit/0198ce6559613e0ee844c07a972a404abc13de75>).
  * Only edit CamOrder.sln and the related project file and metadata using SharpDevelop 4.4 
  * Only edit CamOrder_monodevelop.sln and the related project file and metadata using MonoDevelop.
* YML file names are generated based on UTC time. This is necessary since LastWriteTime of files will DIFFER depending on whether program read the file during DST or not--thus if LastWriteTime were used (as opposed to LastWriteTimeUtc), the program would not be able to tell whether the video file had a corresponding metadata (yml) file.


## Video File Size Notes
### Stats for convert upon import:
- 1440*1080=1555200 pixels original
- 6040Kbps@LP original (29.97fps, 256Kbps AVC audio)
- such as 00507.MTS on CamA
- 1280*720=921600 pixels as imported
- 921600/1555200=0.5925925925925926
- 0.5925925925925926*6040=3579.259259259259Kbps expected bitrate @720x480
- 2910Kbps actual bitrate as imported
- @1280x720 30.000 fps MPEG Video (Version 2) 
- (Main@High 1440) (HDV 720p)
- using:
- "C:\Program Files (x86)\WinFF\ffmpeg.exe" -y -i "%INFILE%" -r 30 -s 1280x720 -q:v 4 -vcodec mpeg2video -acodec copy -f mpegts "%OUTFILE%"
- such as 2013-03-02 18_18_01 CamA-00498 720p30.MTS
