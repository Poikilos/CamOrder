/*
 * Created by SharpDevelop.
 * User: jgustafson
 * Date: 10/11/2012
 * Time: 1:51 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
//IEduSettings is from IntegratorEdu-2012
//IEduYaml is from IntegratorEdu
using System;
using System.Collections;
using System.Collections.Generic; //List<T> etc
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics; //Process etc
using System.Globalization;//DaylightTime etc
//using System.ComponentModel;
//using System.Data;

namespace ExpertMultimedia {
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form {
		/// <summary>
		/// Do not encode the file if placeholder e.g. dest.MPG.txt exists
		/// </summary>
		public static string sMyName="IntegratorEduImport"; //used for user interaction AND subfolder of special folder
		public static string settingsFolder_FullName="";
		public static bool setCheckboxForEachVideoNotYetCopied_enable = false;
		public static string batch_DeleteCommandString="del";
		public static string batch_CopyCommandString="copy";
		public static string batch_RemarkPrefix = "REM ";
		public static string sSecondDotThenSecondExtForPlaceholder=".txt";
		public static string sDirSep=char.ToString(Path.DirectorySeparatorChar);
		public static ArrayList alSources=new ArrayList();
		public static ArrayList alDestinations=new ArrayList();
		public static ArrayList alNotAVideoFile_LowerCaseStrings_ToCompareCaseInsensitively=new ArrayList();
		public const string IEduSettings_Dest_File_Name="integratoreduimport.xml";
		public const string IEduSettings_Source_Destinations_File_Name="destinations.txt";
		public static string var_PrependCam="PrependCam";
		public const string sUnusableFolder_Name="unusable";
		public static string metadataSubfolderName="metadata";
		public static ArrayList alAddedToBatch = new ArrayList();//keeps track of what was added to batch, for easy reflection of batch lines (finding which params are files)
		Color colorSourceFound=Color.Green;
		Color colorSourceNotFound=Color.Red;
		public static IEduSettings settings=null;
		public static bool bSource=false;
		public static string IEduSettings_Commands_File_Name="commands.txt";
		private bool doAutoRefreshNextTime = true;
		public static string RefreshAll_used_source = null;
		public static Color DefaultTextColor = Color.Black; //set according to window manager during MainFormLoad
		private static Dictionary<string, string> command_to_dest_path = null;

		public static int iFilesAdded=0;
		public static int iFilesDeleted=0;
		public static int iSkipped=0;
		public static int iFilesTotal=-1;
		
		public const int Column_Name=0;
		public const int Column_CreationTime=1;
		public const int Column_Size=2;
		public const int Column_FullName=3;
		public static int Columns_Count=0;
		private const int Columns_Max=4;
		//TODO: private Timer refreshTimer = null;
		private bool IsInitialized=false;
		bool is_list_view_mouse_down = false;
		int double_click_tick = -9999;

		public MainForm() {
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			if (Path.DirectorySeparatorChar!='\\') {
				batch_CopyCommandString = "cp";
				batch_DeleteCommandString = "rm";
				batch_RemarkPrefix = "#";
			}
			
		}//end MainForm
		//public static bool ContainsI(ArrayList haystack, string needle) {
		//}
		public static bool bBadDestMsgShownDuringThisBatch=false;
		public static string SourceFileNameToDestFileName(string sFileName, DateTime dtNow, string dateFormatString, string deviceName, string sFormatSuffix, string projectPrefixThenSpaceElseNull) {
			//GetExtension INCLUDES THE DOT!:
			string result=dtNow.ToString(dateFormatString);
			if (deviceName!=null) result+=" "+deviceName+"-";
			else throw new ApplicationException("missing camera name (would corrupt metadata)");
			result+=Path.GetFileNameWithoutExtension(sFileName);
			if (sFormatSuffix!=null) result+=sFormatSuffix;
			result+=Path.GetExtension(sFileName);
			if (projectPrefixThenSpaceElseNull!=null) {
				result=projectPrefixThenSpaceElseNull+result;
			}
			return result;
		}
		
		//public static string WithoutTrailingSlash(
		public static string ToOneLine(string val) {
			if (val!=null) {
				val = val.Replace("\n", String.Empty);
				val = val.Replace("\r", String.Empty);
				val = val.Replace("\v", String.Empty);
			}
			else {
				val = "";
			}
			return val;
		}
		void checkAppDataFolder() {
			try {
				if (!Directory.Exists(this.tbAppDataFolder.Text)) {
					Directory.CreateDirectory(this.tbAppDataFolder.Text);
				}
			}
			catch {
				this.tbAppDataFolder.Text="";
				Application.DoEvents();
			}
		}
		void CheckSources() {
			bSource=false;
			cbFolderIn.ForeColor=colorSourceNotFound;
			try {
				if (Directory.Exists(cbFolderIn.Text)) {
					bSource=true;
					cbFolderIn.ForeColor=colorSourceFound;
					inputListView.Items.Clear();
				}
			}
			catch {}
			try {
				int not_already_found_count = 0;
				if (!bSource) {
					Console.Error.WriteLine("msg: no source, so filling combo box list with "+alSources.Count.ToString()+" item(s)");// (only ones that exist)");
					//Console.Error.WriteLine(ThisBreaksEverything.ManualSerialization.SerializeObject(alSources));
					//Console.Error.WriteLine("alSources: "+ExpertMultimedia.ManualSerialization.SerializeObject(alSources));
					string good_path = null;
					int good_count = 0;
					
					ArrayList new_items = new ArrayList();
					
					//TODO: keep list of user-entered fields so they are never cleared (and save them)
					//foreach (string sFolder in alSources) { //doesn't work even though count is >1 as shown in console by code above! only shows 1! Bad c#! Bad! You stop that!
					for (int i = 0; i < alSources.Count; i++) {
						string sFolder = alSources[i].ToString();
						//if (Directory.Exists(sFolder)) {
							DirectoryInfo sourceDI = null;
							int count = 0;
							try {
								//I've even seen Exist throw some kind of invalid path exception before, so all is in "try" block so doesn't stop at first source
								sourceDI = new DirectoryInfo(sFolder);//even this throws an exception...wha??
								new_items.Add(sFolder);
								bool is_present = false;
								foreach (string s in cbFolderIn.Items) {
									if (s==sFolder) is_present = true;
								}
								if (!is_present) {
									not_already_found_count++;
								}
								Console.Error.WriteLine("show_source: "+sFolder);
								if (sourceDI.Exists) {
									FileInfo[] fis = sourceDI.GetFiles(); //this may throw an exception (such as if sourceDI doesn't exist)!
									foreach (FileInfo thisFI in fis) {
										count++;
										Console.Error.WriteLine("  - "+thisFI.FullName);
									}
								}
							}
							catch (System.IO.DirectoryNotFoundException dnf_exn) {
								//don't care if doesn't exist
								string msg = "WARNING: System.IO.DirectoryNotFoundException for '"+sourceDI.FullName+"'"; //doesn't really matter
								Console.Error.WriteLine(msg);
							}
							catch (Exception exn) {
								string msg = "WARNING: Couldn't finish accessing '"+sourceDI.FullName+"': "+exn.ToString(); //doesn't really matter
								Console.Error.WriteLine(msg);
							}
							Console.Error.WriteLine("  count: "+count.ToString());
							if ( (good_path == null) || (count>good_count) ) {
								good_path = sFolder;
								good_count = count;
							}
						//}
					}
					if (not_already_found_count>0 || new_items.Count!=cbFolderIn.Items.Count) {
						cbFolderIn.Items.Clear();
						foreach (string s in new_items) cbFolderIn.Items.Add(s);
					}
					if (good_path!=null) {
						bSource=true;
						if (not_already_found_count>0 || new_items.Count!=cbFolderIn.Items.Count)
							cbFolderIn.Text=good_path;
						cbFolderIn.ForeColor=colorSourceFound;
					}
				}
				else {
					Console.Error.WriteLine("msg: source in combo box exists, so not filling combo box list");
				}
			}
			catch (Exception exn) {
				string msg = "Couldn't finish CheckSources: "+exn.ToString();
				tbStatus.Text=msg;
				Console.Error.WriteLine(msg);
			}
			try {
				if (bSource) {
					Application.DoEvents();
					cbFolderIn.Select(0,0);
				}
			}
			catch (Exception exn) {
				Console.Error.WriteLine("Could not finish selecting source after loaded in CheckSources: "+exn.ToString());
			}
		}//end CheckSources
		public static string WithNoEndSlash(string val, bool bAllowChangingRootFolderToEmptyString) {
			if (val==null) val="";
			else {
				if (sDirSep==char.ToString(Path.DirectorySeparatorChar)) {
					while (val.EndsWith(sDirSep)) {
						if (bAllowChangingRootFolderToEmptyString||(val!=sDirSep)) {
							val=val.Substring(0,val.Length-1);
						}
					}
				}
				else {
					Console.Error.WriteLine("ERROR: wrong DirectorySeparatorChar (this should never happen)");
				}
			}
			return val;
		}
		public string getSettingsFile_FullName() {
			return Path.Combine(WithNoEndSlash(cbFolderIn.Text,true), IEduSettings_Dest_File_Name);
		}
		private bool LoadSettings() {
			bool bFound=false;
			string SettingsFile_FullName=getSettingsFile_FullName();
			if (!File.Exists(SettingsFile_FullName)) {
				CheckSources();
				SettingsFile_FullName=getSettingsFile_FullName();
			}
			if ( File.Exists(SettingsFile_FullName) ) {
				bFound=true;
				Console.Error.WriteLine("LoadSettings Found '" + SettingsFile_FullName + "'");
				settings=new IEduSettings(SettingsFile_FullName);
				tbPrependCam.Text=settings.GetSetting(var_PrependCam,tbPrependCam.Text);
				tbPrependCam.ForeColor=colorSourceFound;
				Console.Error.WriteLine("Loaded device XML from \""+SettingsFile_FullName+"\".");
			}
			else {
				StackTrace st=new StackTrace();
				Console.Error.WriteLine("Device XML file not found: \""+SettingsFile_FullName+"\" (called by "+st.GetFrame(1).GetMethod().Name+")");
			}
			return bFound;
		}//end LoadSettings
		
		private void SaveSettings() {
			string SettingsFile_FullName=getSettingsFile_FullName();
			//StreamWriter streamOut=new StreamWriter(WithNoEndSlash(tbFolderIn.Text)+sDirSep+IEduSettings_Dest_File_Name);
			if (settings==null) {
				settings=new IEduSettings();
			}
			settings.PutSetting(var_PrependCam,tbPrependCam.Text);
			settings.Save(SettingsFile_FullName);
			//streamOut.Close();
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="inputFullNames">Full file names from camera/camcorder [first parameter was formerly SourceFolder_FullName]</param>
		/// <param name="sDestFolder_FullName"></param>
		/// <param name="sCopyCommand"></param>
		/// <param name="myAppDataSubfolder_FullName"></param>
		/// <param name="dateFormatString"></param>
		/// <param name="deviceName"></param>
		/// <param name="sFormatSuffix"></param>
		/// <param name="sProjectName"></param>
		/// <param name="bMarkAsUnusable"></param>
		/// <param name="bDeleteFromDestOrDoNotCopy"></param>
		/// <param name="bDeleteFromSource"></param>
		/// <param name="tbStatusNow"></param>
		/// <param name="lbOutputNow"></param>
		public static void ProcessFiles(ArrayList inputFullNames, string sDestFolder_FullName, string sCopyCommand, string myAppDataSubfolder_FullName, string dateFormatString, string deviceName, string sFormatSuffix, string sProjectName, bool IsProjectNamePrepended, bool bMarkAsUnusable, bool bDeleteFromDestOrDoNotCopy, bool bDeleteFromSource, TextBox tbStatusNow, ListBox lbOutputNow) {
			//command_to_dest_path = new Dictionary<string, string>();
			bBadDestMsgShownDuringThisBatch=false;
			tbStatusNow.ForeColor=Color.Black;
			tbStatusNow.Text="Starting ProcessFiles...";
			Application.DoEvents();
			
			try {
				if (sDestFolder_FullName.EndsWith(sDirSep)) {
					sDestFolder_FullName=sDestFolder_FullName.Substring(0,sDestFolder_FullName.Length-1);
					Application.DoEvents();
				}
				
				ArrayList fialIn_ALLEXCEPTSETTINGSFILE=new ArrayList();
				if (inputFullNames!=null) {
					foreach (string inputFullName in inputFullNames) {
						try {
							FileInfo inputFileInfo=new FileInfo(inputFullName);
							if (inputFileInfo!=null) fialIn_ALLEXCEPTSETTINGSFILE.Add(inputFileInfo);
						}
						catch {}
					}
				}
				string projectPrefixThenSpaceElseNull=null;
				if (IsProjectNamePrepended) {
					projectPrefixThenSpaceElseNull=sProjectName;
					if (projectPrefixThenSpaceElseNull!=null) {
						projectPrefixThenSpaceElseNull=projectPrefixThenSpaceElseNull.Trim();
						if (!string.IsNullOrEmpty(projectPrefixThenSpaceElseNull)) projectPrefixThenSpaceElseNull+=" ";
					}
				}
				
				if (fialIn_ALLEXCEPTSETTINGSFILE.Count>0) { // (fiarrNow!=null) {
					iFilesTotal=fialIn_ALLEXCEPTSETTINGSFILE.Count;//fiarrNow.Length;
					foreach (FileInfo fiNow in fialIn_ALLEXCEPTSETTINGSFILE) {//fiarrNow) {
						ProcessFile(fiNow, sDestFolder_FullName, sCopyCommand, myAppDataSubfolder_FullName, dateFormatString, deviceName, sFormatSuffix, projectPrefixThenSpaceElseNull, false, false, bDeleteFromSource,lbOutputNow);
						Application.DoEvents();
					}//end foreach fiNow in fialIn_ALLEXCEPTSETTINGSFILE
				}//if fiarrNow!=null
				else {
					string sErrNow=batch_RemarkPrefix+"null file list";//string sErrNow=batch_RemarkPrefix+"null file list for:"+SourceFolder_FullName;
					lbOutputNow.Items.Add(batch_RemarkPrefix+sErrNow);
					Application.DoEvents();
					//batchStream.WriteLine(sErrNow);
				}
				tbStatusNow.Text="Finished Adding to Batch {iFilesAdded:"+iFilesAdded.ToString()+"; iSkipped:"+iSkipped.ToString()+"; iFilesTotal:"+iFilesTotal.ToString()+"}";
				//lbOutputNow.Items.Add("pause");
			}
			catch (Exception exn) {
				string msg = "Could not finish Import: "+exn.ToString();
				Console.Error.WriteLine(msg);
				tbStatusNow.Text=msg;
				tbStatusNow.ForeColor=Color.Red;
			}
			Application.DoEvents();
		}//end ProcessFiles
		
		public static bool ProcessFile(FileInfo fiSrc, string sDestFolder_FullName, string sCopyCommand, string myAppDataSubfolder_FullName, string dateFormatString, string deviceName, string sFormatSuffix, string projectPrefixThenSpaceElseNull, bool bMarkAsUnusable, bool bDeleteFromDestOrDoNotCopy, bool bDeleteFromSource, ListBox lbOutputNow) {
			bool bOK=false;
			if (command_to_dest_path==null) command_to_dest_path = new Dictionary<string, string>();
			try {
				/// Vars:
				/// fiSrc
				/// sDestFolder_FullName
				
				string sCommandNow=sCopyCommand;
				
				bool ThisOperationRequiresLookingAtAllDestinations=true;
				if (bMarkAsUnusable) {//TODO: asdf
					ThisOperationRequiresLookingAtAllDestinations=false;
					
				}
				
				if (bDeleteFromSource) {
					ThisOperationRequiresLookingAtAllDestinations=false;
					fiSrc.Delete();
				}
				
				sDestFolder_FullName=WithNoEndSlash(sDestFolder_FullName,true);//	if ( !string.IsNullOrEmpty(sDestFolder_FullName) && sDestFolder_FullName.EndsWith(sDirSep) && sDestFolder_FullName!=sDirSep) { sDestFolder_FullName=sDestFolder_FullName.Substring(0,sDestFolder_FullName.Length-1) ); }
				sCommandNow=sCommandNow.Replace("%INFILE%",fiSrc.FullName);
				
				string sDesiredDestName=SourceFileNameToDestFileName(fiSrc.Name, fiSrc.CreationTime, dateFormatString, deviceName, sFormatSuffix,projectPrefixThenSpaceElseNull);
				string sDesiredDest_FullName=Path.Combine(sDestFolder_FullName,sDesiredDestName);
				Console.Error.WriteLine("sDesiredDest_FullName: "+sDesiredDest_FullName);
				sCommandNow=sCommandNow.Replace("%OUTFILE%",sDesiredDest_FullName);
				bool bExistsOnAnyDest=false;
				
				if (ThisOperationRequiresLookingAtAllDestinations) {
					//KEEP LIST OF DESTINATIONS TO SEE IF THE FILE EXISTS ANYWHERE, INCLUDING CUSTOM PLACE USER IS PUTTING IT RIGHT NOW
					bExistsOnAnyDest = getWhetherExistsOnAnyDestination(lbOutputNow, fiSrc, sDestFolder_FullName, bDeleteFromDestOrDoNotCopy, dateFormatString, deviceName, myAppDataSubfolder_FullName);

					command_to_dest_path[sCommandNow] = sDesiredDest_FullName;
					if (!bExistsOnAnyDest
						&&!bDeleteFromDestOrDoNotCopy
						//&&!File.Exists(tbFolderOut.Text+sDirSep+sHiddenSubfolder+sDirSep+DestFile_Name)
						//&&!File.Exists(tbFolderOut.Text+sDirSep+sHiddenSubfolder+sDirSep+DestFile_Name+sSecondDotThenSecondExtForPlaceholder)
					   ) {
						if (lbOutputNow!=null) {
							lbOutputNow.Items.Add(sCommandNow);
							Application.DoEvents();

						}
						

						iFilesAdded++;
					}
					else if (bDeleteFromDestOrDoNotCopy && (sCommandNow!=null)) {
						//see above for where delete line is appended to batch & iFilesDeleted is incremented for EACH place the file appears on the destination
					}
					else {
						iSkipped++;
						if (lbOutputNow!=null) lbOutputNow.Items.Add(batch_RemarkPrefix+"ALREADY DONE: "+sCommandNow);
					}
				}//if should actually copy file
				
				bOK=true;
			}
			catch (Exception exn) {
				bOK=false;
				string sFileNow_Name="null";
				if (fiSrc!=null) sFileNow_Name="\""+sFileNow_Name+"\"";
				string msg = ("Could not finish inititializing ProcessFile {fiSrc:"+sFileNow_Name+"}:"+exn.ToString());
				Console.Error.WriteLine(msg);
				lbOutputNow.Items.Add(msg);
			}
			return bOK;
		}//end ProcessFile
		
		/// <summary>
		/// (formerly getInputFilesArrayList)
		/// </summary>
		/// <returns></returns>
		public ArrayList getCheckedInputFilesArrayList(bool uncheck_enable) {
			ArrayList inputFilesArrayList=null;
			try {
				inputFilesArrayList=new ArrayList();
				for (int index=0; index<inputListView.Items.Count; index++) {
					if (inputListView.Items[index].Checked) {
						inputFilesArrayList.Add(inputListView.Items[index].SubItems[Column_FullName].Text);
						if (uncheck_enable) {
							inputListView.Items[index].Checked=false;
							inputListView.Items[index].ForeColor=Color.Gray;
						}
					}
				}
			}
			catch {}//don't care
			return inputFilesArrayList;
		}//end getCheckedInputFilesArrayList
		
		/// <summary>
		/// formerly ImportNew
		/// </summary>
		void AddCheckedFilesToBatch() {
			tbStatus.Text="Adding command to history combo box...";
			if (!cbxCopyCommand.Items.Contains(cbxCopyCommand.Text)) {
				cbxCopyCommand.Items.Add(cbxCopyCommand.Text);
			}
			Application.DoEvents();
			tbStatus.Text="Checking appdata...";
			checkAppDataFolder();
			bool bMarkAsUnusable=false;
			bool bDeleteFromDestOrDoNotCopy=false;
			bool bDeleteFromSource=cbDeleteFromSource.Checked;
			//bMarkAsUnusable,bDeleteFromDestOrDoNotCopy,bDeleteFromSource
			iFilesAdded=0;
			DialogResult dr = DialogResult.Yes;
			if (tbProjectName.Text.Trim().Length == 0)
			{
				dr = MessageBox.Show("Do you want to continue without a project name?", "ieduimport", MessageBoxButtons.YesNo);
			}
			if (dr == DialogResult.Yes)
				ProcessFiles(getCheckedInputFilesArrayList(true),		 tbFolderOut.Text,		 cbxCopyCommand.Text,tbAppDataFolder.Text,	 tbDTFormatString.Text,	tbPrependCam.Text,tbSuffix.Text,tbProjectName.Text,	this.prefixCheckBox.Checked, bMarkAsUnusable,bDeleteFromDestOrDoNotCopy,	bDeleteFromSource,	tbStatus,	outputListBox);
			//ProcessFiles(SourceFolder_FullName,sDestFolder_FullName,	CopyCommand,		myAppDataSubfolder_FullName,dateFormatString,	 deviceName,	sFormatSuffix,	sProjectName,						 bMarkAsUnusable,bDeleteFromDestOrDoNotCopy,	bDeleteFromSource,	tbStatusNow,lbOutputNow)
		}//end AddCheckedFilesToBatch
		
		void RefreshCamera() {
			tbPrependCam.Text="";
			tbPrependCam.Text="";
			outputListBox.Items.Clear();
			CheckSources();
			bool settings_enable = LoadSettings(); //must load settings to know which camera it is, or all or most files will be have checkmark
			this.tbStatus.Text="Listing camera files, please wait...";//TODO: this GUI hangs temporarily while this is shown
			Application.DoEvents();
			string SourceFolder_FullName=this.cbFolderIn.Text;
			DirectoryInfo diBase=null;
			if (SourceFolder_FullName.EndsWith(sDirSep)) {
				SourceFolder_FullName=SourceFolder_FullName.Substring(0,SourceFolder_FullName.Length-1);
				Application.DoEvents();
			}
			bool ok_enable = false;
			try {
				LockChecks();
				diBase=new DirectoryInfo(SourceFolder_FullName);
				FileInfo[] unsortedFileInfoArray=diBase.GetFiles();
				List<FileInfo> sortFileInfos = null;
				
				if (unsortedFileInfoArray!=null) {
					if (unsortedFileInfoArray.Length>0) {
						sortFileInfos = new List<FileInfo>(unsortedFileInfoArray);
						//sortFileInfos.Sort(delegate(FileInfo x, FileInfo y){ return y.CreationTimeUtc.CompareTo(x.CreationTimeUtc); }); //Descending
						sortFileInfos.Sort(delegate(FileInfo x, FileInfo y){ return x.CreationTimeUtc.CompareTo(y.CreationTimeUtc); }); //Descending
					}
				}
				//ArrayList fialIn_ALLEXCEPTSETTINGSFILE=new ArrayList();
				int index=0;

				addCheckedFilesButton.Visible=false;
				markDoneButton.Visible=false;
				markUnusableButton.Visible=false;
				inputListView.Items.Clear();
				foreach (FileInfo fiNow in sortFileInfos) {
					this.tbStatus.Text="Listing camera files ("+(index+1).ToString()+"/"+sortFileInfos.Count+"), please wait...";
					Application.DoEvents();
					if (fiNow.Name!=IEduSettings_Dest_File_Name) {
					   //&&fiNow.Name!="thumbs.db") {
						//fialIn_ALLEXCEPTSETTINGSFILE.Add(fiNow);
						string[] thisRowStrings = new string[Columns_Count];
						thisRowStrings[Column_Name]=fiNow.Name;
						thisRowStrings[Column_CreationTime]=fiNow.CreationTime.ToString("yyyy-MM-dd HH_mm_ss");
						//thisRowStrings[Column_CreationTime]=fiNow.CreationTime.Year.ToString()+"-"+fiNow.CreationTime.Month.ToString()+"-"+fiNow.CreationTime.Day.ToString();
						thisRowStrings[Column_Size]=(fiNow.Length/1024/1024).ToString()+"MB";
						thisRowStrings[Column_FullName]=fiNow.FullName;
						inputListView.Items.Add(new ListViewItem(thisRowStrings));
					}
					index++;
				}
				
				setCheckboxForEachVideoNotYetCopied();
				setGrayForEachVideoMarkedUnusable();
				ok_enable = true;
			}
			catch {
				inputListView.Items.Clear();
				cbFolderIn.ForeColor=colorSourceNotFound;
				this.tbStatus.Text="Cannot find camera/camcorder folder";
			}
			
			UnlockChecks();
			
			if (markDoneButton.Visible==false) markDoneButton.Visible=true;
			if (markUnusableButton.Visible==false) markUnusableButton.Visible=true;
			addCheckedFilesButton.Visible=true;
			if (ok_enable) this.tbStatus.Text="Checking camera...Done";
			UpdateCheckedCount();
		}//end RefreshCamera
		
		#region common utilities
		public static string ToOneLine(Exception exn) {
			return ToOneLine(exn.ToString());
		}
		public static string LiteralToValue(string s) {
			return SafeString(s,true,true,true);
		}
		public static string SafeString(string s, bool AddQuotes, bool ReturnTheWordNullIfNull, bool EscapeExistingQuotes) {
			string quoted="";
			if (s==null) {
				if (ReturnTheWordNullIfNull) quoted="null";
				else quoted="";
			}
			else {
				if (EscapeExistingQuotes) s=s.Replace("\"","\\\"");
				if (AddQuotes) quoted="\""+s+"\"";
				else quoted=s;
			}
			return quoted;
		}//end SafeString
		public static FileInfo SafeFileInfo(string fileFullName) {
			FileInfo thisFileInfo=null;
			try {
				thisFileInfo=new FileInfo(fileFullName);
			}
			catch (Exception exn) {
				Console.Error.WriteLine(batch_RemarkPrefix+"Could not finish getting file info for "+LiteralToValue(fileFullName)+": "+ToOneLine(exn));
			}
			return thisFileInfo;
		}//end SafeFileInfo(string)
		#endregion common utilities
		
		public static bool getWhetherFileWasProcessed(ListBox lbOutNow, string fileFullName, string destinationFolderDesired_FullName, string dateFormatString, string deviceName, string myAppDataSubfolder_FullName) {
			return getWhetherFileWasProcessed(lbOutNow, SafeFileInfo(fileFullName), destinationFolderDesired_FullName, dateFormatString, deviceName, myAppDataSubfolder_FullName);
		}//end getWhetherFileWasProcessed
		
		public static bool getWhetherFileWasProcessed(ListBox lbOutNow, FileInfo thisFileInfo, string destinationFolderDesired_FullName, string dateFormatString, string deviceName, string myAppDataSubfolder_FullName) {
			return getWhetherFileWasMarkedDone(thisFileInfo,dateFormatString,deviceName,myAppDataSubfolder_FullName) || getWhetherExistsOnAnyDestination(lbOutNow, thisFileInfo,destinationFolderDesired_FullName,false, dateFormatString, deviceName, myAppDataSubfolder_FullName);
		}//end getWhetherFileWasProcessed
		
		public static string getYMLFullName(FileInfo thisFileInfo,string dateFormatString, string deviceName, string myAppDataSubfolder_FullName) {
			string ymlName=SourceFileNameToYMLName(thisFileInfo,dateFormatString,deviceName);
			string ymlFullName=Path.Combine(myAppDataSubfolder_FullName,metadataSubfolderName);
			if (!string.IsNullOrEmpty(deviceName)) {
				ymlFullName = Path.Combine( ymlFullName, deviceName.Trim() );
			}
			ymlFullName = Path.Combine( ymlFullName, ymlName );
			return ymlFullName;
		}
		

		public static bool ToBool(string val) {
			bool IsTrue=false;
			if (!string.IsNullOrEmpty(val)) {
				if (val=="1") {
					IsTrue=true;
				}
				else {
					string val_ToLower=val.ToLower();
					if (val_ToLower=="yes" || val_ToLower=="true" || val_ToLower=="on" || val_ToLower=="y") {
						IsTrue=true;
					}
				}
			}
			return IsTrue;
		}//end ToBool
		
		public static void setWhetherFileWasMarkedDone(FileInfo thisFileInfo,string dateFormatString, string deviceName, string myAppDataSubfolder_FullName, bool set_Processed) {
			setMetaDataValue(thisFileInfo,dateFormatString,deviceName,myAppDataSubfolder_FullName,"processed",set_Processed?"yes":"no");
		}
		/// <summary>
		/// Sets metadata for a given file from a given device, else UNmarks as unusable if null value
		/// </summary>
		/// <param name="thisFileInfo"></param>
		/// <param name="dateFormatString"></param>
		/// <param name="deviceName"></param>
		/// <param name="myAppDataSubfolder_FullName"></param>
		/// <param name="unusableReasonString"></param>
		public static void setUnusableReason(FileInfo thisFileInfo,string dateFormatString, string deviceName, string myAppDataSubfolder_FullName, string unusableReasonString) {
			setMetaDataValue(thisFileInfo,dateFormatString,deviceName,myAppDataSubfolder_FullName,"unusable",unusableReasonString);
		}
		/// <summary>
		/// Sets metadata for a given file from a given device, else removes variable if null value 
		/// </summary>
		/// <param name="thisFileInfo"></param>
		/// <param name="dateFormatString"></param>
		/// <param name="deviceName"></param>
		/// <param name="myAppDataSubfolder_FullName"></param>
		/// <param name="setVar_Name"></param>
		/// <param name="setVar_Value"></param>
		public static void setMetaDataValue(FileInfo thisFileInfo,string dateFormatString, string deviceName, string myAppDataSubfolder_FullName, string setVar_Name, string setVar_Value) {
			string ymlFullName=getYMLFullName(thisFileInfo,dateFormatString,deviceName,myAppDataSubfolder_FullName);
			Console.Error.WriteLine("Writing \""+ymlFullName+"\"");
			StreamReader inStream=null;
			StreamWriter outStream=null;
			//bool marked=false;
			bool IsMarkChanged=false;
			bool IsLineToBeWrittenUnmodified=true;
			bool IsMarkFound=false;
			string tempFileFullName=ymlFullName+".tmp";
			
			if (File.Exists(ymlFullName)) {
				//marked=true; //assume processed if no line exists.
				inStream=new StreamReader(ymlFullName);
				outStream=new StreamWriter(tempFileFullName);
				try {
					string line=null;
					while ( (line=inStream.ReadLine()) != null ) {
						IsLineToBeWrittenUnmodified=true;
						string nameString=null;
						string valueString=null;
						string line_Trim=line.Trim();
						if (!string.IsNullOrEmpty(line_Trim)) {
							bool IsOK=IEduYaml.getNameAndValueFromYAMLLine(out nameString, out valueString, line);
							if (IsOK) {
								if (valueString!=null) {
									if (nameString.ToLower()==setVar_Name) {
										if (!IsMarkChanged) {
											//outStream.WriteLine(setVar_Name+":"+((setVar_Value!=null)?setVar_Value:"");
											if (setVar_Value!=null) {
												if (setVar_Value!="") outStream.WriteLine(setVar_Name+":"+setVar_Value);
												else outStream.WriteLine(setVar_Name+":"+"\"\"");
											}
											IsMarkChanged=true;
											IsLineToBeWrittenUnmodified=false;
											//if (ToBool(valueString)==false) {
											//	marked=false;
											//}
											//break;
										}
										else {
											IsLineToBeWrittenUnmodified=false;//redundant setVar_Name statement so cancel writing 
										}
										IsMarkFound=true;
									}
								}
							}
						}
						if (IsLineToBeWrittenUnmodified) { //must be a line other than the variable being changed
							outStream.WriteLine(line);
						}
					}//end while lines in file
					inStream.Close();
					inStream=null;
					if (!IsMarkFound) {
						if (setVar_Value!=null) outStream.WriteLine(setVar_Name+":"+setVar_Value);
					}
					outStream.Close();
					outStream=null;					
					File.Delete(ymlFullName);
					File.Move(tempFileFullName,ymlFullName);
				}
				catch (Exception exn) {
					Console.Error.WriteLine("Could not finish setMetaDataValue:"+exn.ToString());
				}
				try {
					if (inStream!=null) inStream.Close();}
				catch{}//don't care
				try {
					if (outStream!=null) outStream.Close();}
				catch{}//don't care
			}//end if file exists
			else {
				try {
					string directoryFullName=null;
					int lastSlashIndex=ymlFullName.LastIndexOf(Path.DirectorySeparatorChar);
					if (lastSlashIndex>=0) {
						directoryFullName=ymlFullName.Substring(0,lastSlashIndex);
					}
					if (!Directory.Exists(directoryFullName)) Directory.CreateDirectory(directoryFullName);
					outStream=new StreamWriter(ymlFullName);
					outStream.WriteLine(setVar_Name+":"+setVar_Value);
					outStream.Close();
					outStream=null;
				}
				catch (Exception exn) {
					Console.Error.WriteLine("Could not finish saving new yml file setMetaDataValue:"+exn.ToString());
					try {outStream.Close();
					outStream=null;}
					catch {}//don't care
					
				}
			}

		}//end setWhetherFileWasMarkedDone
		
		public static bool getWhetherFileWasMarkedDone(FileInfo thisFileInfo,string dateFormatString, string deviceName, string myAppDataSubfolder_FullName) {
			string resultString=getMetaDataValue(thisFileInfo,dateFormatString,deviceName,myAppDataSubfolder_FullName,"processed");
			bool resultBool=false;
			if (resultString!=null) resultBool=ToBool(resultString);
			return resultBool;
		}
		public static bool getWhetherFileWasMarkedUnusable(FileInfo thisFileInfo,string dateFormatString, string deviceName, string myAppDataSubfolder_FullName) {
			string resultString=getMetaDataValue(thisFileInfo,dateFormatString,deviceName,myAppDataSubfolder_FullName,"unusable");
			bool resultBool=false;
			if (resultString!=null) resultBool=true;
			return resultBool;
		}
		/// <summary>
		/// Get metadata from this program's appdata concerning a file
		/// </summary>
		/// <param name="thisFileInfo"></param>
		/// <param name="dateFormatString"></param>
		/// <param name="deviceName"></param>
		/// <param name="myAppDataSubfolder_FullName"></param>
		/// <returns>the metadata value, else null</returns>
		public static string getMetaDataValue(FileInfo thisFileInfo,string dateFormatString, string deviceName, string myAppDataSubfolder_FullName, string var_Name) {
			string var_Value=null;
			//bool marked=false;
			//string ymlName=SourceFileNameToYMLName(thisFileInfo,dateFormatString,deviceName,myAppDataSubfolder_FullName);
			string ymlFullName=getYMLFullName(thisFileInfo,dateFormatString,deviceName,myAppDataSubfolder_FullName);//string ymlFullName = Path.Combine( Path.Combine(myAppDataSubfolder_FullName,metadataSubfolderName), ymlName ) + ".yml";
			StreamReader inStream=null;
			string var_Name_ToLower=null;
			if (var_Name!=null) var_Name_ToLower=var_Name.ToLower();
			if (File.Exists(ymlFullName)) {
				//marked=true; //assume processed if no line exists but file does exist.
				try {
					inStream=new StreamReader(ymlFullName);
					string line=null;
					while ( (line=inStream.ReadLine()) != null ) {
						string nameString=null;
						string valueString=null;
						string line_Trim=line.Trim();
						if (!string.IsNullOrEmpty(line_Trim)) {
							bool IsOK=IEduYaml.getNameAndValueFromYAMLLine(out nameString, out valueString,line);
							if (IsOK) {
								if (valueString!=null) {
									if (nameString.ToLower()==var_Name_ToLower) {
										var_Value=valueString;
										break;
									}
								}
							}
						}
					}
				}
				catch (Exception exn) {
					Console.Error.WriteLine("Could not finish getMetaDataValue:"+exn.ToString());
				}
				try {
					inStream.Close();}
				catch{}//don't care
			}
			return var_Value;
		}//end getWhetherFileWasMarkedDone
		
		
		public static string SourceFileNameToYMLName(FileInfo thisFileInfo,string dateFormatString, string deviceName) {
			//string destFileName=SourceFileNameToDestFileName(thisFileInfo.Name,thisFileInfo.CreationTime,dateFormatString,deviceName,null,null);
			string destFileName=thisFileInfo.CreationTimeUtc.ToString("yyyy-MM-dd HH_mm_ss")+" "+Path.GetFileNameWithoutExtension(thisFileInfo.Name);//TODO: asdf
			string ymlName=destFileName+".yml";
			return ymlName;
		}
		
		private static ArrayList destinationDIs=new ArrayList(); //cache file list for each destination directory
		
		public void ReloadDestinations() {
			string profile_path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			try {
				DirectoryInfo profile_di = new DirectoryInfo(profile_path);
				if (profile_di.Name.ToLower()=="documents") {
					profile_path = profile_di.Parent.FullName;
				}
			}
			catch {} //don't care

			string videos_path = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
			try {
				if (!Directory.Exists(this.tbFolderOut.Text)) {
					if (Directory.Exists(videos_path)) {
						this.tbFolderOut.Text = videos_path;
					}
				}
			}
			catch {} //don't care
			string videos_meta_path = Path.Combine(videos_path, "_ieduimport_metadata");
			try {
				if (!Directory.Exists(this.tbAppDataFolder.Text)) {
					if (Directory.Exists(videos_path)) {
						if (!Directory.Exists(videos_meta_path))
							Directory.CreateDirectory(videos_meta_path);
						this.tbAppDataFolder.Text = videos_meta_path;
					}
				}
			}
			catch {} //don't care
			//this.tbFolderOut;
		}

		public static void ClearDestinationFileLists() {
			//ReloadDestinations();
			destinationDIs.Clear();
		}
		
		public static bool getWhetherExistsOnAnyDestination(ListBox lbOutputNow, FileInfo thisFileInfo, string sDestFolder_FullName, bool bDeleteFromDestinations, string dateFormatString, string deviceName, string myAppDataSubfolder_FullName) {
			bool bExistsOnAnyDest=false;
			//bool bOK=true;
			//sDateParsed=String.Format("{"+tbDTFormatString.Text+"}",thisFileInfo.CreationTime);
			//alPossibleDestDates.Clear();
			ArrayList alPossibleDestFile_Names=new ArrayList();
			ArrayList alPossibleDestFile_DTStrings=new ArrayList();
			//ArrayList alPossibleDestDates=new ArrayList();
			string numberString=Path.GetFileNameWithoutExtension(thisFileInfo.Name);
			DaylightTime daylighttimeYearOfThis =TimeZone.CurrentTimeZone.GetDaylightChanges(thisFileInfo.CreationTime.Year);
			bool bFileInFileYearDST = (thisFileInfo.CreationTimeUtc>daylighttimeYearOfThis.Start && thisFileInfo.CreationTimeUtc<daylighttimeYearOfThis.End);// iFileDateDiffFromFileYearNonDST=1;
			
			//must use utc for this, since adding offsets manually (to compensate for possibly mixed DST and non-DST):
			DateTime dtCreationManuallyCalculatedUsingDST=thisFileInfo.CreationTimeUtc.Add(bFileInFileYearDST?daylighttimeYearOfThis.Delta:TimeSpan.Zero);
			DateTime dtLastWriteManuallyCalculatedUsingDST=thisFileInfo.LastWriteTimeUtc.Add(bFileInFileYearDST?daylighttimeYearOfThis.Delta:TimeSpan.Zero);
			
			DateTime dtCreationEarlyFromRetroactivelyCalculatedDSTWrongly=thisFileInfo.CreationTime.AddHours(-1.0);
			DateTime dtLastWriteEarlyFromRetroactivelyCalculatedDSTWrongly=thisFileInfo.LastWriteTime.AddHours(-1.0);
			
			DateTime dtCreationLateFromRetroactivelyCalculatedDSTWrongly=thisFileInfo.CreationTime.AddHours(1.0);
			DateTime dtLastWriteLateFromRetroactivelyCalculatedDSTWrongly=thisFileInfo.LastWriteTime.AddHours(1.0);
			
			string sDesiredDestName=SourceFileNameToDestFileName(thisFileInfo.Name,thisFileInfo.CreationTime, dateFormatString, deviceName, null,null);
			alPossibleDestFile_Names.Clear();
			int POSSIBLEDESTFILE_DESIRED=0;
			
			alPossibleDestFile_Names.Add(sDesiredDestName);
			alPossibleDestFile_DTStrings.Add(thisFileInfo.CreationTime.ToString(dateFormatString));
			alPossibleDestFile_DTStrings.Add(thisFileInfo.LastWriteTime.ToString(dateFormatString));
			
			alPossibleDestFile_Names.Add(SourceFileNameToDestFileName(thisFileInfo.Name,dtCreationManuallyCalculatedUsingDST, dateFormatString, deviceName, null,null));//why wrong??? e.g. 2012-10-04 18_08_04 CamB-00255 720p30.MTS calculated 2013-04-02
			alPossibleDestFile_Names.Add(SourceFileNameToDestFileName(thisFileInfo.Name,dtLastWriteManuallyCalculatedUsingDST, dateFormatString, deviceName, null,null));//why wrong??? e.g. 2012-10-04 18_08_04 CamB-00255 720p30.MTS calculated 2013-04-02
			alPossibleDestFile_DTStrings.Add(dtCreationManuallyCalculatedUsingDST.ToString(dateFormatString));
			alPossibleDestFile_DTStrings.Add(dtLastWriteManuallyCalculatedUsingDST.ToString(dateFormatString));
			
			alPossibleDestFile_Names.Add(SourceFileNameToDestFileName(thisFileInfo.Name,dtCreationEarlyFromRetroactivelyCalculatedDSTWrongly, dateFormatString, deviceName, null,null));
			alPossibleDestFile_Names.Add(SourceFileNameToDestFileName(thisFileInfo.Name,dtLastWriteEarlyFromRetroactivelyCalculatedDSTWrongly, dateFormatString, deviceName, null,null));
			alPossibleDestFile_DTStrings.Add(dtCreationEarlyFromRetroactivelyCalculatedDSTWrongly.ToString(dateFormatString));
			alPossibleDestFile_DTStrings.Add(dtLastWriteEarlyFromRetroactivelyCalculatedDSTWrongly.ToString(dateFormatString));
			
			alPossibleDestFile_Names.Add(SourceFileNameToDestFileName(thisFileInfo.Name,dtCreationLateFromRetroactivelyCalculatedDSTWrongly, dateFormatString, deviceName, null,null));
			alPossibleDestFile_Names.Add(SourceFileNameToDestFileName(thisFileInfo.Name,dtLastWriteLateFromRetroactivelyCalculatedDSTWrongly, dateFormatString, deviceName, null,null));
			alPossibleDestFile_DTStrings.Add(dtCreationEarlyFromRetroactivelyCalculatedDSTWrongly.ToString(dateFormatString));
			alPossibleDestFile_DTStrings.Add(dtLastWriteEarlyFromRetroactivelyCalculatedDSTWrongly.ToString(dateFormatString));
			
			ArrayList alDestinationsTheoretical=new ArrayList();
			if (!alDestinations.Contains(sDestFolder_FullName)) {
				alDestinations.Add(sDestFolder_FullName);
			}
			foreach (string sDestNow_unparsedstring in alDestinations) {
				string sDestNow=sDestNow_unparsedstring;
				if (sDestNow.EndsWith(sDirSep)) sDestNow=sDestNow.Substring(0,sDestNow.Length-1);
				try {if (Directory.Exists(sDestNow)) alDestinationsTheoretical.Add(sDestNow);}
				catch {}//don't care since bad path doesn't need to be used because it is bad
				string sDestUnusableNow=Path.Combine(sDestNow,sUnusableFolder_Name);
				try {if (Directory.Exists(sDestUnusableNow)) alDestinationsTheoretical.Add(sDestUnusableNow);}
				catch {}//don't care since bad path doesn't need to be used because it is bad
			}
			
			//CHECK WHETHER FILE EXISTS ALREADY IN ANY DESTINATION
			foreach (string sDestNow_unparsedstring in alDestinationsTheoretical) {
				string sDestNow=sDestNow_unparsedstring;
				try {
					if (sDestNow.EndsWith(sDirSep)) sDestNow=sDestNow.Substring(0,sDestNow.Length-1);
					int POSSIBLEDESTFILE_Now=0;
					
					foreach (string DestFile_NameNow in alPossibleDestFile_Names) {
						try {
							string DestFile_FullNameNow=sDestNow+sDirSep+DestFile_NameNow;
							if (File.Exists(DestFile_FullNameNow)) {
								bExistsOnAnyDest=true;
								if (bDeleteFromDestinations) {
									string sDeleteCommandNow=batch_DeleteCommandString+" \""+DestFile_FullNameNow+"\"";
									if (lbOutputNow!=null) lbOutputNow.Items.Add(sDeleteCommandNow);
									Application.DoEvents();
									File.Delete(DestFile_FullNameNow);
									iFilesDeleted++;
								}
								if (POSSIBLEDESTFILE_Now!=POSSIBLEDESTFILE_DESIRED) {
									Console.Error.WriteLine("Unexpected destination filename \""+DestFile_FullNameNow+"\"--expected *"+sDirSep+"\""+sDesiredDestName+"\"");
								}
								if (!bDeleteFromDestinations) break;
							}//end if exact match
							else {
								//Console.Error.WriteLine("no theoretical \""+DestFile_FullNameNow+"\"");
							}
							if (File.Exists(DestFile_FullNameNow+sSecondDotThenSecondExtForPlaceholder)) {
								bExistsOnAnyDest=true;
								if (POSSIBLEDESTFILE_Now!=POSSIBLEDESTFILE_DESIRED) {
									Console.Error.WriteLine("Unexpected destination filename \""+DestFile_FullNameNow+"\"--expected *"+sDirSep+"\""+sDesiredDestName+"\"");
								}
								break;
							}
							else {
								//Console.Error.WriteLine("no theoretical \""+DestFile_FullNameNow+sSecondDotThenSecondExtForPlaceholder+"\"");
							}
						}
						catch (Exception exn) {
							string sMsg_ToOneLine="ProcessFile could not finish checking for potential destination file \""+DestFile_NameNow+"\":"+exn.ToString().Replace("\n"," ").Replace("\r"," ");
							lbOutputNow.Items.Add(batch_RemarkPrefix+sMsg_ToOneLine);
							Console.Error.WriteLine(sMsg_ToOneLine);
						}
						POSSIBLEDESTFILE_Now++;
					}//end for each filename
					try {
						if (!bExistsOnAnyDest) {
							DirectoryInfo thisDI=null;
							FileInfo[] allFIs=null;
							foreach (CachedDir thatCachedDir in destinationDIs) {
								//TODO: warn about not being case sensitive
								if (thatCachedDir.DirectoryFullName==sDestNow_unparsedstring) {
									thisDI=thatCachedDir.directoryInfo;
									allFIs=thatCachedDir.files;
								}
							}
							if (allFIs==null) {
								thisDI=new DirectoryInfo(sDestNow_unparsedstring);
								allFIs=thisDI.GetFiles();
								CachedDir cachedDir=new CachedDir();
								cachedDir.directoryInfo=thisDI;
								cachedDir.DirectoryFullName=sDestNow_unparsedstring;
								cachedDir.files=allFIs;
								destinationDIs.Add(cachedDir);
							}
							foreach (FileInfo destFI in allFIs) {
								if (destFI.Name.Contains(numberString)) {
									foreach (string thisDateString in alPossibleDestFile_DTStrings) {
										if (destFI.Name.Contains(thisDateString)) {
											bExistsOnAnyDest=true;
											break;
										}
									}
								}
								if (bExistsOnAnyDest) break;
							}
							if (bExistsOnAnyDest) break;
						}//end if not found yet
					}
					catch (Exception exn) {
						string sShowDest="null";
						if (sDestNow!=null) sShowDest="\""+sDestNow+"\"";
						Console.Error.WriteLine("Couldn't finish comparing name in destination \""+sShowDest+"\" specified in "+IEduSettings_Source_Destinations_File_Name+":"+exn.ToString());
					}
					
				}
				catch(Exception exn) {
					//bOK=false;
					//if (!bBadDestMsgShownDuringThisBatch) {
					//	Message.Show("Destination \""+tbFolderOut.Text+"\" could not be accessed");
					//	bBadDestMsgShownDuringThisBatch=true;
					//}
					string sShowDest="null";
					if (sDestNow!=null) sShowDest="\""+sDestNow+"\"";
					Console.Error.WriteLine("Couldn't finish accessing destination \""+sShowDest+"\" specified in "+IEduSettings_Source_Destinations_File_Name+":"+exn.ToString());
				}
			}//end for sDestNow (checking each possible destination to see whether file exists)

			return bExistsOnAnyDest;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="thisFileInfo"></param>
		/// <returns></returns>
		public static bool getWhetherExistsOnAnyDestination_ExactName(ListBox lbOutputNow, FileInfo thisFileInfo, string sDestFolder_FullName, bool bDeleteFromDestinations, string dateFormatString, string deviceName, string myAppDataSubfolder_FullName) {
			bool bExistsOnAnyDest=false;
			//bool bOK=true;
			//sDateParsed=String.Format("{"+tbDTFormatString.Text+"}",thisFileInfo.CreationTime);
			//alPossibleDestDates.Clear();
			ArrayList alPossibleDestFile_Names=new ArrayList();
			//ArrayList alPossibleDestDates=new ArrayList();
			DaylightTime daylighttimeYearOfThis =TimeZone.CurrentTimeZone.GetDaylightChanges(thisFileInfo.CreationTime.Year);
			bool bFileInFileYearDST = (thisFileInfo.CreationTimeUtc>daylighttimeYearOfThis .Start && thisFileInfo.CreationTimeUtc<daylighttimeYearOfThis .End);// iFileDateDiffFromFileYearNonDST=1;
			DateTime dtManuallyCalculatedUsingDST=thisFileInfo.CreationTimeUtc.Add(bFileInFileYearDST?daylighttimeYearOfThis .Delta:TimeSpan.Zero);
			DateTime dtEarlyFromRetroactivelyCalculatedDSTWrongly=thisFileInfo.CreationTime.AddHours(-1.0);
			DateTime dtLateFromRetroactivelyCalculatedDSTWrongly=thisFileInfo.CreationTime.AddHours(1.0);
			//alPossibleDestFile_Names.Add(sDateParsed);
			//alPossibleDestFile_Names.Add(dtEarlyFromRetroactivelyCalculatedDSTWrongly.CreationTime.ToString(tbDTFormatString.Text));
			//alPossibleDestFile_Names.Add(dtLateFromRetroactivelyCalculatedDSTWrongly.CreationTime.ToString(tbDTFormatString.Text));
			
			string sDesiredDestName=SourceFileNameToDestFileName(thisFileInfo.Name,thisFileInfo.CreationTime, dateFormatString, deviceName, null,null);
			alPossibleDestFile_Names.Clear();
			int POSSIBLEDESTFILE_DESIRED=0;
			alPossibleDestFile_Names.Add(sDesiredDestName);
			alPossibleDestFile_Names.Add(SourceFileNameToDestFileName(thisFileInfo.Name,dtManuallyCalculatedUsingDST, dateFormatString, deviceName, null,null));//why wrong??? e.g. 2012-10-04 18_08_04 CamB-00255 720p30.MTS calculated 2013-04-02
			alPossibleDestFile_Names.Add(SourceFileNameToDestFileName(thisFileInfo.Name,dtEarlyFromRetroactivelyCalculatedDSTWrongly, dateFormatString, deviceName, null,null));
			alPossibleDestFile_Names.Add(SourceFileNameToDestFileName(thisFileInfo.Name,dtLateFromRetroactivelyCalculatedDSTWrongly, dateFormatString, deviceName, null,null));
			//DaylightTime dtYearPrev=TimeZone.CurrentTimeZone.GetDaylightChanges(thisFileInfo.CreationTimeUtc.Year-1);
			//bool bFileInFileYearDST = thisFileInfo.CreationTimeUtc<daylighttimeYearOfThis .End && thisFileInfo.CreationTimeUtc>daylighttimeYearOfThis .Start;
			//int iFileDateDiffFromFileYearNonDST=0;
			//if (thisFileInfo.CreationTimeUtc<daylighttimeYearOfThis .Start) iFileDateDiffFromFileYearNonDST=-1;
			//else if (thisFileInfo.CreationTimeUtc>daylighttimeYearOfThis .End) iFileDateDiffFromFileYearNonDST=1;
			
			//DaylightTime dtYearOfNow=TimeZone.CurrentTimeZone.GetDaylightChanges(DateTime.Now.Year);
			//bool bNowInYearNowDST = (DateTime.Now>dtYearOfNow.Start && DateTime.Now<dtYearOfNow.End);
			
			//daylighttimeYearOfThis .Delta
			
			//int iDSTOffsetOfThis=;
			//int iDSTOffsetOfPrev=;
			//string DestFile_Expected_Name=sDateParsed+tbPrependCam.Text+thisFileInfo.Name;
			//alPossibleDestFile_Names.Clear();
			
			//GetExtension INCLUDES THE DOT!:
			//DestFile_Name=Path.GetFileNameWithoutExtension(DestFile_Name)+tbSuffix.Text+Path.GetExtension(DestFile_Name);//DestFile_Name.Replace(".MTS",tbSuffix.Text+".MTS");
			//string DestFile_FullName=tbFolderOut.Text+sDirSep+DestFile_Name;
			
			//try {
			//	if (File.Exists(DestFile_FullName)) {
			//		bExistsOnAnyDest=true;
			//	}
			//	else if (File.Exists(DestFile_FullName+sSecondDotThenSecondExtForPlaceholder)) {
			//		bExistsOnAnyDest=true;
			//	}
			//}
			//catch(Exception exn) {
			//	if (!bBadDestMsgShownDuringThisBatch) {
			//		MessageBox.Show("Destination \""+tbFolderOut.Text+"\" could not be accessed");
			//		bBadDestMsgShownDuringThisBatch=true;
			//	}
			//	Console.Error.WriteLine(exn.ToString());
			//}
			
			ArrayList alDestinationsTheoretical=new ArrayList();
			if (!alDestinations.Contains(sDestFolder_FullName)) {
				alDestinations.Add(sDestFolder_FullName);
			}
			foreach (string sDestNow_unparsedstring in alDestinations) {
				string sDestNow=sDestNow_unparsedstring;
				if (sDestNow.EndsWith(sDirSep)) sDestNow=sDestNow.Substring(0,sDestNow.Length-1);
				alDestinationsTheoretical.Add(sDestNow);
				alDestinationsTheoretical.Add(sDestNow+sDirSep+sUnusableFolder_Name);
			}
			
			//CHECK WHETHER FILE EXISTS ALREADY IN ANY DESTINATION
			foreach (string sDestNow_unparsedstring in alDestinationsTheoretical) {
				string sDestNow=sDestNow_unparsedstring;
				try {
					if (sDestNow.EndsWith(sDirSep)) sDestNow=sDestNow.Substring(0,sDestNow.Length-1);
					int POSSIBLEDESTFILE_Now=0;
					foreach (string DestFile_NameNow in alPossibleDestFile_Names) {
						try {
							string DestFile_FullNameNow=sDestNow+sDirSep+DestFile_NameNow;
							if (File.Exists(DestFile_FullNameNow)) {
								bExistsOnAnyDest=true;
								if (bDeleteFromDestinations) {
									string sDeleteCommandNow=batch_DeleteCommandString+" \""+DestFile_FullNameNow+"\"";
									if (lbOutputNow!=null) lbOutputNow.Items.Add(sDeleteCommandNow);
									Application.DoEvents();
									File.Delete(DestFile_FullNameNow);
									iFilesDeleted++;
								}
								if (POSSIBLEDESTFILE_Now!=POSSIBLEDESTFILE_DESIRED) {
									Console.Error.WriteLine("Unexpected destination filename \""+DestFile_FullNameNow+"\"--expected *"+sDirSep+"\""+sDesiredDestName+"\"");
								}
								if (!bDeleteFromDestinations) break;
							}
							else {
								//Console.Error.WriteLine("no theoretical \""+DestFile_FullNameNow+"\"");
							}
							if (File.Exists(DestFile_FullNameNow+sSecondDotThenSecondExtForPlaceholder)) {
								bExistsOnAnyDest=true;
								if (POSSIBLEDESTFILE_Now!=POSSIBLEDESTFILE_DESIRED) {
									Console.Error.WriteLine("Unexpected destination filename \""+DestFile_FullNameNow+"\"--expected *"+sDirSep+"\""+sDesiredDestName+"\"");
								}
								break;
							}
							else {
								//Console.Error.WriteLine("no theoretical \""+DestFile_FullNameNow+sSecondDotThenSecondExtForPlaceholder+"\"");
							}
						}
						catch (Exception exn) {
							string sMsg_ToOneLine="ProcessFile could not finish checking for potential destination file \""+DestFile_NameNow+"\":"+exn.ToString().Replace("\n"," ").Replace("\r"," ");
							lbOutputNow.Items.Add(batch_RemarkPrefix+sMsg_ToOneLine);
							Console.Error.WriteLine(sMsg_ToOneLine);
						}
						POSSIBLEDESTFILE_Now++;
					}//end for each filename
				}
				catch(Exception exn) {
					//bOK=false;
					//if (!bBadDestMsgShownDuringThisBatch) {
					//	Message.Show("Destination \""+tbFolderOut.Text+"\" could not be accessed");
					//	bBadDestMsgShownDuringThisBatch=true;
					//}
					string sShowDest="null";
					if (sDestNow!=null) sShowDest="\""+sDestNow+"\"";
					Console.Error.WriteLine("Couldn't finish accessing "+sShowDest+" specified in "+IEduSettings_Source_Destinations_File_Name+":"+exn.ToString());
				}
			}//end for sDestNow (checking each possible destination to see whether file exists)

			return bExistsOnAnyDest;
		}//end getWhetherExistsOnAnyDestination

		
		#region GUI actions
		
		void setGrayForEachVideoMarkedUnusable() {
			//TODO: asdf
			//TODO: also make a "Reason" column during FormLoad and set values here
		}
		void setCheckboxForEachVideoNotYetCopied() {
			if (setCheckboxForEachVideoNotYetCopied_enable) {
				string dateFormatString="";
				string deviceName="";
				string sFormatSuffix="";
				string destinationFolderDesired_FullName="";
				try {
					dateFormatString=this.tbDTFormatString.Text;
					deviceName=this.tbPrependCam.Text;
					sFormatSuffix=this.tbSuffix.Text;
					destinationFolderDesired_FullName=this.tbFolderOut.Text;
					LockChecks();
					for (int index=0; index<this.inputListView.Items.Count; index++) {
						if (tbStatus.Text!="Marking unprocessed files...") {
							tbStatus.Text="Marking unprocessed files...";
							//this.tbStatus.Text="Marking unprocessed files ("+(index+1).ToString()+"/"+this.inputListView.Items.Count.ToString()+")...";
						}
						this.progressBar1.Value=(int)(((double)(index+1)/(double)inputListView.Items.Count)*(double)this.progressBar1.Maximum+.5);
						Application.DoEvents();
						FileInfo thisFileInfo=null;
						string fileFullName="";
						try {
							fileFullName=inputListView.Items[index].SubItems[Column_FullName].Text;
							thisFileInfo=new FileInfo(fileFullName);
						}
						catch (Exception exn) {
							this.outputListBox.Items.Add(batch_RemarkPrefix+"Could not getting file info for "+LiteralToValue(fileFullName)+": "+ToOneLine(exn));
						}
						//lbOutNow, thisFileInfo, destinationFolderDesired_FullName, string dateFormatString, string deviceName, string sFormatSuffix
						if (!getWhetherFileWasProcessed(this.outputListBox, thisFileInfo, destinationFolderDesired_FullName, dateFormatString, deviceName, tbAppDataFolder.Text)
							&& !getWhetherFileWasMarkedUnusable(thisFileInfo, dateFormatString, deviceName, tbAppDataFolder.Text) ) {
							inputListView.Items[index].Checked=true;
						}
						else {
							inputListView.Items[index].ForeColor=Color.Gray;
						}
					}
					this.progressBar1.Value=0;
				}
				catch (Exception exn) {
					this.tbStatus.Text="Cannot finish checking which files were already copied: "+exn.ToString();
				}
				UnlockChecks();
			}
		}//end setCheckboxForEachVideoNotYetCopied
		void editCopy() {
			if (this.outputListBox.SelectedIndex>=0
				&& this.outputListBox.Items.Count>0) {
				Clipboard.SetText(this.outputListBox.Items[this.outputListBox.SelectedIndex].ToString());
			}
		}
		void editDelete() {
			this.outputListBox.SuspendLayout();
			try {
				if (this.outputListBox.SelectedIndex>-1
					&& this.outputListBox.Items.Count>0
					&& this.outputListBox.SelectedIndices.Count>0) {
					int[] SelectedIndeces_IntArray=new int[this.outputListBox.SelectedIndices.Count];
					int indexOfIndex=0;
					foreach (int index in this.outputListBox.SelectedIndices) {
						SelectedIndeces_IntArray[indexOfIndex]=index;
						indexOfIndex++;
					}
					for (indexOfIndex=this.outputListBox.SelectedIndices.Count-1; indexOfIndex>=0; indexOfIndex--) {
						this.outputListBox.Items.RemoveAt(SelectedIndeces_IntArray[indexOfIndex]);
					}
				}
			}
			catch {
				//don't care
			}
			this.outputListBox.ResumeLayout();
		}//end editDelete
		void selectAllInput() {
			try {
				if (this.inputListView.Items.Count>0) {
					this.inputListView.Focus();//needed, otherwise selected items won't show as highlighted until the ListView is clicked
					//this.inputListView.SuspendLayout();
					//this.inputListView.SelectedIndices.Clear();
					Application.DoEvents();
					for (int index=0; index<this.inputListView.Items.Count; index++) {
						//this.inputListView.SelectedIndices.Add(index);
						this.inputListView.Items[index].Selected=true;
					}
					//this.inputListView.ResumeLayout();
					this.inputListView.Invalidate();
					//this.inputListView.Refresh();
					Application.DoEvents();
				}
			}
			catch {} //don't care
			UpdateCheckedCount();
		}//end selectAllInput
		void deselectAllInput() {
			try {
				this.inputListView.Focus();//needed, otherwise selected items won't show as highlighted until the ListView is clicked
				foreach (ListViewItem lvi in inputListView.Items) {
					lvi.Selected=false;
				}
				this.inputListView.Invalidate();
			}
			catch { } //don't care
			UpdateCheckedCount();
		}//end deselectAllInput
		void editPaste() {
			if (this.outputListBox.SelectedIndex>=0
				&& this.outputListBox.Items.Count>0) {
				this.outputListBox.Items.Insert(this.outputListBox.SelectedIndex,Clipboard.GetText());
			}
			else {
				this.outputListBox.Items.Add(Clipboard.GetText());
			}
		}
		void editPasteAfter() {
			int nextIndex=0;
			if (this.outputListBox.SelectedIndex>=0
				&& this.outputListBox.Items.Count>0) {
				nextIndex=this.outputListBox.SelectedIndex+1;
			}
			if (nextIndex<this.outputListBox.Items.Count) {
				this.outputListBox.Items.Insert(nextIndex,Clipboard.GetText());
			}
			else {
				this.outputListBox.Items.Add(Clipboard.GetText());
			}
		}
		
		void editCut() {
			editCopy();
			editDelete();
		}
		void UpdateCheckedCount() {
			int count=0;
			string addButton_Text="Add";
			try {
				for (int index=0; index<this.inputListView.Items.Count; index++) {
					if (this.inputListView.Items[index].Checked) {
						count++;
					}
				}
			}
			catch {}//don't care
			//if (count>0) {
				addButton_Text+=" "+count.ToString()+"";
			//}
			addButton_Text+=" Checked File(s) to Batch";
			this.addCheckedFilesButton.Text=addButton_Text;
			this.markDoneButton.Text="Mark ("+count.ToString()+") as Done";
			this.markUnusableButton.Text="Mark ("+count.ToString()+") Unusable";
		}//end UpdateCheckedCount
		
		#endregion GUI actions
		
		
		#region Events
		void MainFormLoad(object sender, EventArgs e) {
			DefaultTextColor = cbFolderIn.ForeColor;
			initTimer.Start();
		}//end MainFormLoad
				
		void TbFolderInTextChanged(object sender, EventArgs e) {
			
		}
		/// <summary>
		/// Saves camera prefix to the camera (formerly BtnSaveCamPrefixClick)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void SaveCamSettingsButtonClick(object sender, EventArgs e)
		{
			
			SaveSettings();
			Application.DoEvents();
			bool settings_enable = LoadSettings();
		}
		
		void TsmiListFilesMarkedUnusableButClick(object sender, EventArgs e)
		{
			//string sDestination=null;
			DirectoryInfo diFootageNow=null;
			DirectoryInfo diUnusableNow=null;
			FileInfo[] fiarrFootage=null;
			string LastDestination=null;
			tbStatus.Text="List Files Marked Unusable But Exist in Destination...";
			Application.DoEvents();
			int iUnusableInPrimaryFolder=0;
			try {
				outputListBox.Items.Add(batch_RemarkPrefix+sMyName+" List Files Marked Unusable But Exist in Destination");
				outputListBox.Items.Add(batch_RemarkPrefix+"does not actually delete anything. ");
				outputListBox.Items.Add(batch_RemarkPrefix+"Run batch to ACTUALLY DELETE:");
				outputListBox.Items.Add(batch_RemarkPrefix+" wait until done than you can click");
				outputListBox.Items.Add(batch_RemarkPrefix+" Help, Save Output, name with .bat at end");
				outputListBox.Items.Add(batch_RemarkPrefix+" then run the batch file.");
				Application.DoEvents();
				foreach (string sDestinationNow in alDestinations) {
					LastDestination=sDestinationNow;
					try {
						diFootageNow=new DirectoryInfo(sDestinationNow); //(string)alDestinations[0]
					}
					catch (Exception exn2) {
						string sMsg="Could not finish accessing folder \""+sDestinationNow+"\" in TsmiListFilesMarkedUnusableButClick:"+exn2.ToString();
						Console.Error.WriteLine(sMsg);
						sMsg=ToOneLine(sMsg);
						outputListBox.Items.Add(batch_RemarkPrefix+sMsg);
					}
					fiarrFootage=diFootageNow.GetFiles();
					string sUnusableFolderNow=diFootageNow.FullName+sDirSep+sUnusableFolder_Name;
					diUnusableNow=new DirectoryInfo(sUnusableFolderNow);
					if (diUnusableNow.Exists) {
						tbStatus.Text="Checking for files in \""+LastDestination+"\" that have placeholders (e.g. filename.ext"+MainForm.sSecondDotThenSecondExtForPlaceholder+") or copies in \""+sUnusableFolderNow+"\"...";
						Application.DoEvents();
						foreach (FileInfo fiNow in fiarrFootage) {
							if (!alNotAVideoFile_LowerCaseStrings_ToCompareCaseInsensitively.Contains(fiNow.Name.ToLower())) {
								bool bFoundDup=false;
								foreach (FileInfo fiUnusable in diUnusableNow.GetFiles()) {
									if (fiNow.Name.ToLower()==fiUnusable.Name.ToLower()) {
										bFoundDup=true;
									}
									else if  ((fiNow.Name+MainForm.sSecondDotThenSecondExtForPlaceholder).ToLower()==fiUnusable.Name.ToLower()) {
										bFoundDup=true;
									}
								}
								if (bFoundDup) {
									iUnusableInPrimaryFolder++;
									outputListBox.Items.Add("del \""+fiNow.FullName+"\"");
								}
							}
						}
					}
					else this.outputListBox.Items.Add(batch_RemarkPrefix+"no folder \""+sUnusableFolderNow+"\" for unusable files, so skipping duplicate check for destination \""+sDestinationNow+"\"");
				}//end foreach sDestinationNow
				tbStatus.Text="List Files Marked Unusable But Exist in Destination...OK ("+iUnusableInPrimaryFolder.ToString()+" found)";
			}
			catch (Exception exn) {
				string sMsg="Could not finish TsmiListFilesMarkedUnusableButClick {LastDestination:\""+LastDestination+"\"}: "+exn.ToString();
				tbStatus.Text=sMsg;
				Console.Error.WriteLine(sMsg);
			}
		}//end marked as TsmiListFilesMarkedUnusableButClick
		
		void SaveOutputToolStripMenuItemClick(object sender, EventArgs e)
		{
			int iLine=-1;
			tbStatus.Text="Save Output...";
			Application.DoEvents();
			try {
				DialogResult dlgresultNow=savefiledlgMain.ShowDialog();
				if (dlgresultNow==DialogResult.OK) {
					if (!string.IsNullOrEmpty(savefiledlgMain.FileName)) {
						DialogResult dlgresultConfirm=DialogResult.Yes;
						
						if (File.Exists(savefiledlgMain.FileName)) {
							FileInfo fiDest=new FileInfo(savefiledlgMain.FileName);
							dlgresultConfirm=MessageBox.Show("Overwrite existing file (original contents will be lost) with the output (text!) only?","Confirm overwrite \""+fiDest.Name+"\"", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
						}
						if (dlgresultConfirm==DialogResult.Yes) {
							StreamWriter streamOut=new StreamWriter(savefiledlgMain.FileName);
							for (iLine=0; iLine<outputListBox.Items.Count; iLine++) {
								streamOut.WriteLine(outputListBox.Items[iLine].ToString());
							}
							streamOut.Close();
							tbStatus.Text="Save Output...OK";
						}
						else {
							tbStatus.Text="Save Output cancelled: user said \"no\" to overwrite existing file.";
						}
					}
					else {
						tbStatus.Text="Save Output cancelled: no filename chosen.";
					}
				}
				else {
					tbStatus.Text="User cancelled Save Output.";
				}
			}
			catch (Exception exn) {
				string sMsg="Could not finish SaveOutputToolStripMenuItemClick {savefiledlgMain.FileName:"+((savefiledlgMain.FileName==null)?"null":("\""+savefiledlgMain.FileName+"\""))+"}: "+exn.ToString();
				tbStatus.Text=sMsg;
				Console.Error.WriteLine(sMsg);
			}
		}//end SaveOutputToolStripMenuItemClick
		
		void MarkFilesAsUnusableToolStripMenuItemClick(object sender, EventArgs e)
		{
			tbStatus.Text="Mark Files As Unusable...";
			Application.DoEvents();
			string LastFileName="<before initialized>";
			
			try {
				tbProjectName.Text=tbProjectName.Text.Trim();
				DialogResult dlgresultConfirmBlankProjectName=DialogResult.Yes;
				if (string.IsNullOrEmpty(tbProjectName.Text)) {
					dlgresultConfirmBlankProjectName=MessageBox.Show("No project name--there will be no reason (group name) saved to the placeholder file in the \""+sUnusableFolder_Name+"\" folder. Continue anyway?","Confirm Blank Project Name",MessageBoxButtons.YesNo);
				}
				if (dlgresultConfirmBlankProjectName==DialogResult.Yes) {
					if (string.IsNullOrEmpty(openfiledlgMain.InitialDirectory)) {
						openfiledlgMain.InitialDirectory=this.tbFolderOut.Text;
					}
					openfiledlgMain.Multiselect=true;
					DialogResult dlgresultNow=openfiledlgMain.ShowDialog();
					
					if (dlgresultNow==DialogResult.OK) {
						string[] sarrSelectedFiles=openfiledlgMain.SafeFileNames;
						if (sarrSelectedFiles!=null && sarrSelectedFiles.Length>0) {
							string sLineToWrite=tbProjectName.Text;
							string sDestFolderNow_FullName=tbFolderOut.Text;
							FileInfo fiTemp=new FileInfo(openfiledlgMain.FileNames[0]);
							
							if (sDestFolderNow_FullName.EndsWith(sDirSep)) sDestFolderNow_FullName=sDestFolderNow_FullName.Substring(0,sDestFolderNow_FullName.Length-1);
							if (!Directory.Exists(sDestFolderNow_FullName)) {
								string sDirFailsafe=fiTemp.Directory.FullName;
								outputListBox.Items.Add(batch_RemarkPrefix+"could not find \""+sDestFolderNow_FullName+"\" so using \""+sDirFailsafe+"\" to contain unusable folder.");
								sDestFolderNow_FullName=sDirFailsafe;
							}
							string sUnusableFolderNow_FullName=sDestFolderNow_FullName+sDirSep+sUnusableFolder_Name;
							Directory.CreateDirectory(sUnusableFolderNow_FullName);
							foreach (string SelectedFiles_FileNow_Name in sarrSelectedFiles) {
								LastFileName=SelectedFiles_FileNow_Name;
								string PlaceHolderFileNow_FullName=sUnusableFolderNow_FullName+sDirSep+SelectedFiles_FileNow_Name+MainForm.sSecondDotThenSecondExtForPlaceholder;
								outputListBox.Items.Add("echo "+sLineToWrite+" > \""+PlaceHolderFileNow_FullName+"\"");
								StreamWriter srNow=new StreamWriter(PlaceHolderFileNow_FullName);
								srNow.WriteLine(sLineToWrite);
								srNow.Close();
							}
							tbStatus.Text="Mark Files As Unusable...OK";
						}
						else {
							tbStatus.Text="Mark Files As Unusable cancelled: no filename(s) chosen.";
						}
					}
					else {
						tbStatus.Text="User cancelled Mark Files As Unusable.";
					}
				}
				else {
					tbStatus.Text="User cancelled Mark Files As Unusable (blank project name).";
				}
			}
			catch (Exception exn) {
				string sMsg="Could not finish MarkFilesAsUnusableToolStripMenuItemClick {LastFileName:"+((LastFileName==null)?"null":("\""+LastFileName+"\""))+"}: "+exn.ToString();
				tbStatus.Text=sMsg;
				Console.Error.WriteLine(sMsg);
			}
		}//end MarkFilesAsUnusableToolStripMenuItemClick
		
		void ImportAndMarkAsUnusableToolStripMenuItemClick(object sender, EventArgs e)
		{
			checkAppDataFolder();
			bool bMarkAsUnusable=true;
			bool bDeleteFromDestOrDoNotCopy=false;
			bool bDeleteFromSource=false;
			//bMarkAsUnusable,bDeleteFromDestOrDoNotCopy,bDeleteFromSource
			DialogResult dr = DialogResult.Yes;
			if (tbProjectName.Text.Trim().Length==0) {
				dr = MessageBox.Show("Do you want to continue without a project name?", "ieduimport", MessageBoxButtons.YesNo);
			}
			if (dr==DialogResult.Yes) ProcessFiles(getCheckedInputFilesArrayList(true),		tbFolderOut.Text,			cbxCopyCommand.Text,	 tbAppDataFolder.Text,		tbDTFormatString.Text,		tbPrependCam.Text,	tbSuffix.Text,		tbProjectName.Text,	 prefixCheckBox.Checked,	 bMarkAsUnusable,bDeleteFromDestOrDoNotCopy,				bDeleteFromSource,				tbStatus,	outputListBox);
			//ProcessFiles(ArrayList inputFullNames,			 string sDestFolder_FullName, string sCopyCommand,string myAppDataSubfolder_FullName, string dateFormatString, string deviceName, string sFormatSuffix, string sProjectName, bool IsProjectNamePrepended, bool bMarkAsUnusable, bool bDeleteFromDestOrDoNotCopy,	 bool bDeleteFromSource, TextBox tbStatusNow, ListBox lbOutputNow) {
			//ProcessFiles(SourceFolder_FullName,sDestFolder_FullName,	CopyCommand,		myAppDataSubfolder_FullName,dateFormatString,	 deviceName,		sFormatSuffix,	sProjectName,		 bMarkAsUnusable,bDeleteFromDestOrDoNotCopy,	bDeleteFromSource,	tbStatusNow,lbOutputNow)
		}
		
		void DeleteAndMarkAsUnusableToolStripMenuItemClick(object sender, EventArgs e)
		{
			checkAppDataFolder();
			bool bMarkAsUnusable=true;
			bool bDeleteFromDestOrDoNotCopy=true;
			bool bDeleteFromSource=false;
			//bMarkAsUnusable,bDeleteFromDestOrDoNotCopy,bDeleteFromSource
			ProcessFiles(getCheckedInputFilesArrayList(true),		tbFolderOut.Text,			cbxCopyCommand.Text,	tbAppDataFolder.Text,	tbDTFormatString.Text,			tbPrependCam.Text,	tbSuffix.Text,		tbProjectName.Text,	 prefixCheckBox.Checked,	 bMarkAsUnusable,bDeleteFromDestOrDoNotCopy,bDeleteFromSource,				tbStatus,	outputListBox);
			//ProcessFiles(SourceFolder_FullName,sDestFolder_FullName,	CopyCommand,		myAppDataSubfolder_FullName,dateFormatString,	 deviceName,		sFormatSuffix,	sProjectName,		 bMarkAsUnusable,bDeleteFromDestOrDoNotCopy,	bDeleteFromSource,	tbStatusNow,lbOutputNow)
		}
		
		void RefreshAll(bool force_enable) {
			if ( force_enable || RefreshAll_used_source==null
				 || RefreshAll_used_source != cbFolderIn.Text ) {
				RefreshAll_used_source = cbFolderIn.Text;
				tbStatus.Text="Clearing lists...";
				Application.DoEvents();
				ClearDestinationFileLists();
				tbStatus.Text="Waiting for camera to become ready...";
				Application.DoEvents();
				RefreshCamera();
			}
		}
		void RefreshButtonClick(object sender, EventArgs e)
		{
			RefreshAll(true);
		}
		
		
		void HelpToolStripMenuItemClick(object sender, EventArgs e)
		{
			
		}
		
		void DeleteOutputButtonClick(object sender, EventArgs e)
		{
			editDelete();
		}
		
		void CopyOutputButtonClick(object sender, EventArgs e)
		{
			editCopy();
		}
		
		void PasteOutputButtonClick(object sender, EventArgs e)
		{
			editPaste();
		}
		//void LbCopyCommandPresetsMouseClick(object sender, MouseEventArgs e)
		//{
		//	//foreach (ListViewItem in cbxCopyCommand.SelectedItems
		//	if (cbxCopyCommand.SelectedItem!=null) {
		//		cbxCopyCommand.Text=cbxCopyCommand.SelectedItem.ToString();
		//	}
		//}
		
		void OutputListBoxMouseClick(object sender, MouseEventArgs e) {
			
		}
				
		void InputListViewItemChecked(object sender, ItemCheckedEventArgs e)
		{
			
			UpdateCheckedCount();
		}
		
		void FlowLayoutPanel4Paint(object sender, PaintEventArgs e)
		{
			
		}
		
		void LockChecks() {
			try {
				this.SuspendLayout();
				tableLayoutPanelOutput.SuspendLayout();
				inputListTableLayoutPanel.SuspendLayout();
				flowLayoutPanel1.SuspendLayout();
				formInputSettingsTableLayoutPanel.SuspendLayout();
				flowLayoutPanel2.SuspendLayout();
				flowLayoutPanelControlsLeftOfOutputBox.SuspendLayout();
				splitContainer1.SuspendLayout();
				splitContainerMainInAndOut.SuspendLayout();
				//inputListView.Visible=false;
				//outputListBox.Visible=false;
				inputListView.BeginUpdate();
				//outputListBox.BeginUpdate();
			}
			catch (Exception exn) {
				Console.Error.WriteLine("Could not finish UnlockChecks: "+exn.ToString());
			}
		}
		
		void UnlockChecks() {
			try {
				this.ResumeLayout();
				tableLayoutPanelOutput.ResumeLayout();
				inputListTableLayoutPanel.ResumeLayout();
				flowLayoutPanel1.ResumeLayout();
				formInputSettingsTableLayoutPanel.ResumeLayout();
				flowLayoutPanel2.ResumeLayout();
				flowLayoutPanelControlsLeftOfOutputBox.ResumeLayout();
				splitContainer1.ResumeLayout();
				splitContainerMainInAndOut.ResumeLayout();
				//inputListView.Visible=true;
				//outputListBox.Visible=true;
				inputListView.EndUpdate();
				//outputListBox.EndUpdate();
			}
			catch (Exception exn) {
				Console.Error.WriteLine("Could not finish UnlockChecks: "+exn.ToString());
			}
		}
		
		void UncheckAllButtonClick(object sender, EventArgs e)
		{
			try {
				tbStatus.Text="Unchecking files...";
				Application.DoEvents();
				LockChecks();
				foreach (ListViewItem lvi in this.inputListView.Items) {
					lvi.Checked=false;
				}
				tbStatus.Text="Unchecking files...OK";
			}
			catch {
				tbStatus.Text="Unchecking files...FAIL";
			} //don't care
			UnlockChecks();
		}
		
		void UncheckSelectedButtonClick(object sender, EventArgs e)
		{
			try {
				tbStatus.Text="Unchecking selected files...";
				Application.DoEvents();
				LockChecks();
				foreach (int selectedIndex in this.inputListView.SelectedIndices) {
					inputListView.Items[selectedIndex].Checked=false;
				}
				tbStatus.Text="Unchecking files...OK";
			}
			catch {
				tbStatus.Text="Unchecking selected files...FAIL";
			} //don't care
			UnlockChecks();
		}
		
		void CheckSelectedButtonClick(object sender, EventArgs e)
		{
			try {
				tbStatus.Text="Checking selected files...";
				Application.DoEvents();
				LockChecks();
				foreach (int selectedIndex in this.inputListView.SelectedIndices) {
					this.inputListView.Items[selectedIndex].Checked=true;
				}
				tbStatus.Text="Unchecking files...OK";
			}
			catch {
				tbStatus.Text="Unchecking selected files...FAIL";
			} //don't care
			UnlockChecks();
		}
		
		void CheckAllButtonClick(object sender, EventArgs e)
		{
			try {
				tbStatus.Text="Checking files...";
				Application.DoEvents();
				LockChecks();
				foreach (ListViewItem lvi in this.inputListView.Items) {
					lvi.Checked=true;
				}
				tbStatus.Text="Checking files...OK";
			}
			catch {
				tbStatus.Text="Checking files...FAIL";
			} //don't care
			UnlockChecks();
		}
		
		void SplitContainerMainInAndOutSplitterMoved(object sender, SplitterEventArgs e)
		{
			
		}
		
		/// <summary>
		/// Get the first string in a space-delimited command, taking quotes around the command into account
		/// </summary>
		/// <param name="val"></param>
		/// <returns>the full or relative executable name in the beginning of val; null if remark</returns>
		public static string getExecutableStringFromCommandString(string val) {
			string commandFullOrRelName=null;
			try {
				if (val.StartsWith("\"")) {
					int secondQuoteIndex=val.IndexOf('"',1);
					commandFullOrRelName=val.Substring(1,secondQuoteIndex);//1 to skip initial quote
				}
				else {//relative or other non-quoted executable
					int spaceIndex=val.IndexOf(" ");
					if (spaceIndex>=0) {
						//TODO: account for escaped space
						commandFullOrRelName=val.Substring(0,spaceIndex);
					}
					else {
						commandFullOrRelName=val;
					}
					if (commandFullOrRelName.Length>=batch_RemarkPrefix.Trim().Length) {
						if (commandFullOrRelName.ToLower().Trim()==batch_RemarkPrefix.ToLower().Trim()
							|| commandFullOrRelName.ToLower().StartsWith(batch_RemarkPrefix.ToLower()) ) {
							commandFullOrRelName = null;
						}
					}
				}
			}
			catch (Exception exn) {
				Console.Error.WriteLine("Could not finish parsing command getExecutableStringFromCommandString:"+exn.ToString());
			} //don't care
			return commandFullOrRelName;
		}//end getExecutableStringFromCommandString
		
		public static string FirstQuotedStringInString(string val, int startIndex, out int nextIndex, int lineForDebugOnly_ElseHideIfNegative, TextBox null_or_statusTextBox) {
			string firstQuotedString=null;
			string deconstructingThisCommandString = val;
			string msg="";
			nextIndex=startIndex;
			int firstQuoteIndex = deconstructingThisCommandString.IndexOf("\"",startIndex);
			int lineCountingNumber=lineForDebugOnly_ElseHideIfNegative;
			if (firstQuoteIndex>=0) {
				deconstructingThisCommandString = deconstructingThisCommandString.Substring(firstQuoteIndex+1);
				
				int secondQuoteIndex = deconstructingThisCommandString.IndexOf("\"");
				if (secondQuoteIndex>0) {
					nextIndex=firstQuoteIndex+1+secondQuoteIndex+1;//firstQuoteIndex+1 to move past removed beginning, then +1 more to move past second quote
					//Console.Error.WriteLine("NOTE: next search area after current quotation starts at ["+nextIndex.ToString()+"] in \""+val+"\"");
					deconstructingThisCommandString = deconstructingThisCommandString.Substring(0,secondQuoteIndex);
					firstQuotedString = deconstructingThisCommandString;
				}
				else {
					msg = "ERROR: could not detect end of quoted input in"+((lineCountingNumber>-1)?(" (line "+lineForDebugOnly_ElseHideIfNegative+")"):(""))+":\""+val+"\"";
					Console.Error.WriteLine(msg);
					if (null_or_statusTextBox!=null) null_or_statusTextBox.Text = msg;
				}
			}
			else {
				msg = "ERROR: could not detect start of quoted input in"+((lineCountingNumber>-1)?(" (line "+lineForDebugOnly_ElseHideIfNegative+")"):(""))+":\""+val+"\"";
				Console.Error.WriteLine(msg);
				if (null_or_statusTextBox!=null) null_or_statusTextBox.Text = msg;
			}
			return firstQuotedString;
		}//end FirstQuotedStringInString
		
		/// <summary>
		/// Split respecting quotes (quotes only count if not escaped with backslash)
		/// </summary>
		/// <param name="haystack"></param>
		/// <param name="delimiter"></param>
		/// <param name="quote"></param>
		/// <returns>DOES keep quotes</returns>
		public static ArrayList split_respecting_quotes(string haystack, char delimiter, char quote, bool escape_quote_enable) {
			bool is_quoted = false;
			int index=0;
			int start_index=0;
			ArrayList results = null;
			char prev_char = '\0';
			if (haystack!=null) {
				results = new ArrayList();
				haystack=haystack.Trim();
				while (index<=haystack.Length) {
					if ( index==haystack.Length || ((haystack[index]==delimiter)&&!is_quoted) ) {
						int endbefore=index;
						int this_length = endbefore-start_index;
						if (this_length>0) results.Add(haystack.Substring(start_index,this_length));
						else results.Add("");
						start_index=index+1;
					}
					else if ( haystack[index]==quote && (!escape_quote_enable || prev_char!='\\') ) {
						if (is_quoted) {
							is_quoted=false;
						}
						else {
							is_quoted=true;
						}
					}
					if (index<haystack.Length) prev_char = haystack[index];
					else prev_char='\0';  // doesn't really matter
					index++;
				}
			}
			return results;
		}
		
		bool show_noinputfileerror_enable=true;
		
		void RunBatchButtonClick(object sender, EventArgs e)
		{
			show_noinputfileerror_enable=true;
			bool IsToShowBadBatchError=true;
			this.progressBar1.Value=0;
			Application.DoEvents();
			//outputListBox.Items.Add("pause");
			Application.DoEvents();
			this.tbStatus.Text="Writing batch...";
			Application.DoEvents();
			StreamWriter batchStream=null;
			string sProjectName=null;
			string deviceName=null;
			bool doRun=true;
			long inputMBTotalCount = 0;
			int lineCountingNumber = 1;
			//ArrayList successList = new ArrayList();
			ArrayList failedList = new ArrayList();
			//ArrayList boxList = new ArrayList();
			foreach (string thisString in this.outputListBox.Items) {//foreach (ListViewItem thisItem in this.outputListBox.Items) {
				try {
					//boxList.Add(thisString);
					string thisCommandString = thisString;//thisItem.ToString();
					if (thisString==null) Console.Error.WriteLine("Command: null");
					else Console.Error.WriteLine("Command: \""+thisString+"\"");
					string this_executable = getExecutableStringFromCommandString(thisString);
					if (this_executable!=null) {
						if (this_executable==null) Console.Error.WriteLine("Looking for params for estimate after executable string: null");
						else Console.Error.WriteLine("Looking for params for estimate after executable string: \""+this_executable+"\"");
						int nextIndex=thisString.IndexOf(this_executable)+this_executable.Length;
						//next line is ok since SKIPS command
						string inputFileFullName = FirstQuotedStringInString(thisCommandString, nextIndex, out nextIndex, lineCountingNumber, tbStatus);
						//if (inputFileFullName!=null && inputFileFullName.Contains(this_executable)) { //if (inputFileFullName!=null && (inputFileFullName.ToLower().EndsWith(".exe") || inputFileFullName.ToLower().EndsWith("ffmpeg")) ) {
						//	inputFileFullName = FirstQuotedStringInString(thisCommandString,nextIndex, out nextIndex, lineCountingNumber, tbStatus);
						//}
						if (!string.IsNullOrEmpty(inputFileFullName)) {
							FileInfo thisFileInfo  = new FileInfo(inputFileFullName);
							inputMBTotalCount += thisFileInfo.Length/1024/1024;
						}
					}
					else Console.Error.WriteLine("Ignoring line during estimate since line is remark: "+thisString);
				}
				catch (Exception exn) {
					string msg = "Could not finish calculating size for input file in batch line "+lineCountingNumber.ToString();
					tbStatus.Text = msg;
					Console.Error.WriteLine(msg+": "+exn.ToString());
				}
				lineCountingNumber++;
			}
			string DoBatch_Name=sMyName+"-DoBatch "+DateTime.Now.ToString("yyyy-MM-dd");
			string dotExt="";
			string participle="reading form";
			int indeterminate_count = 0;
			ArrayList alNonDeletableFiles = new ArrayList();
			//int actually_written_more_than_zero_count = 0;
			ArrayList alZeroLengthFilePaths = new ArrayList();
			ArrayList alCommandsThatResultedInNonzeroLengthFiles = new ArrayList();
			ArrayList alCommandsThatFailed = new ArrayList();
			try {
				//TODO: asdf use inputMBTotalCount and file size and run each file as a separate batch and adjust mainProgressBar
				sProjectName=this.tbProjectName.Text;
				deviceName=this.tbPrependCam.Text;
				participle="getting batch name";
				if (!string.IsNullOrEmpty(sProjectName.Trim())) DoBatch_Name+=" "+sProjectName.Trim();
				participle="adding device name";
				DoBatch_Name+=deviceName;
				if (Path.DirectorySeparatorChar=='\\') {
					dotExt=".bat";
					DoBatch_Name+=".bat";
				}
				else {
					batch_RemarkPrefix="#";
					batch_DeleteCommandString="rm -f";
					dotExt=".sh";
					DoBatch_Name+=".sh";
				}
				string history_path = Path.Combine(tbAppDataFolder.Text, "history");
				if (!Directory.Exists(history_path)) Directory.CreateDirectory(history_path);
				string DoBatch_FullName=Path.Combine(history_path,DoBatch_Name);
				participle = "opening batch for writing";
				batchStream=new StreamWriter(DoBatch_FullName);//keep a list of all batch commands
				participle = "creating temp batch file name";
				string temp1CommandBatchName="temp"+dotExt;
				long doneMBCount=0;
				decimal doneRatio=0;
				for (int index=0; index<this.outputListBox.Items.Count; index++) {
					participle = "getting name from list item";
					string thisString = this.outputListBox.Items[index].ToString();
					if (thisString!=null) {
						string thisCommandString = thisString;//thisItem.ToString();
						participle = "getting executable from command";
						if (thisString==null) Console.Error.WriteLine("Command: null");
						else Console.Error.WriteLine("Command: \""+thisString+"\"");
						string this_executable = getExecutableStringFromCommandString(thisString);
						if (this_executable!=null) {
							if (this_executable==null) Console.Error.WriteLine("Detected executable string for run: null");
							else Console.Error.WriteLine("Detected executable string for run: \""+this_executable+"\"");
							//int nextIndex=-1;
							//int start_index=0;
							//if (this_executable!=null) start_index=this_executable.Length;
							participle = "getting input file path from command";
							bool IsAccidentallyCommand=false;
							int nextIndex=thisString.IndexOf(this_executable)+this_executable.Length;
							string inputFileFullName = FirstQuotedStringInString(thisCommandString, nextIndex, out nextIndex, lineCountingNumber, tbStatus);
							//if (inputFileFullName!=null && inputFileFullName.Contains(this_executable)) { //if (inputFileFullName!=null && (inputFileFullName.ToLower().EndsWith(".exe") || inputFileFullName.ToLower().EndsWith("ffmpeg")) ) {
							//	participle = "getting output file path from command";
							//	IsAccidentallyCommand=true;
							//	inputFileFullName = FirstQuotedStringInString(thisCommandString,nextIndex, out nextIndex, lineCountingNumber, tbStatus);
							//}
							if (IsAccidentallyCommand) { participle = "getting input file size (accidentally got command for first param)"; }
							else participle = "getting input file size";
							long thisFileMBCount=0;
							long thisFileByteCount=0;
							if (!string.IsNullOrEmpty(inputFileFullName)) {
								FileInfo thisFileInfo  = new FileInfo(inputFileFullName);
								if (thisFileInfo.Exists) {
									thisFileByteCount=thisFileInfo.Length;
									thisFileMBCount = thisFileInfo.Length/1024/1024;
								}
								else {
									if (show_noinputfileerror_enable) {
										MessageBox.Show("Warning: missing input file '"+inputFileFullName+"' --command may have been parsed wrongly"); //possibly parsed incorrectly
										show_noinputfileerror_enable=false;
									}
								}
							}
							if (thisFileMBCount==0) {
								Console.Error.WriteLine("Warning: "+thisFileMBCount.ToString()+"MB count ("+thisFileByteCount.ToString()+" bytes) for file \""+inputFileFullName+"\" in command string \""+thisCommandString+"\" for batch line "+lineCountingNumber.ToString());
							}
							
							if (batchStream!=null) {
								participle = "writing batch line";
								batchStream.WriteLine(thisString);
							}
							else {
								if (IsToShowBadBatchError) {
									IsToShowBadBatchError=false;
									MessageBox.Show("I could not write a line to the batch-style log since stream was null.");
								}
							}
							if (!thisString.ToUpper().StartsWith(batch_RemarkPrefix.ToUpper())) {
								try {
									participle="opening temp batch for writing";
									StreamWriter temp1CommandBatchStream=new StreamWriter(temp1CommandBatchName);
									participle="writing temp batch";
									temp1CommandBatchStream.WriteLine(thisString);
									participle="closing temp batch";
									temp1CommandBatchStream.Close();
									temp1CommandBatchStream=null;	
									doRun=true;
								}
								catch (Exception exn) {
									doRun=false;
									tbStatus.Text="Could not finish writing batch: "+exn.ToString()+"{previous_status:"+tbStatus.Text+"}";
									Console.Error.WriteLine(exn.ToString());
								}
								bool batch_enable = false;
								if (doRun) {
									//this.tbStatus.Text="Running batch (please wait for black console window to finish and disappear)...";
									Application.DoEvents();
									try {
										if (iFilesAdded>0) {
											participle="setting up batch process";
											tbStatus.Text="Running batch...("+doneMBCount.ToString()+"/"+inputMBTotalCount.ToString()+" MB) {iFilesAdded:"+iFilesAdded.ToString()+"; iSkipped:"+iSkipped.ToString()+"; iFilesTotal:"+iFilesTotal.ToString()+"}";
											Process p = new Process();
											//p.StartInfo.UseShellExecute = false;
											if (batch_enable) p.StartInfo.FileName = temp1CommandBatchName;
											else {
												ArrayList chunks = split_respecting_quotes(thisString,' ','"',true);
												bool is_process = true;
												foreach (string chunk_original in chunks) {
													string chunk = chunk_original;
													if ( chunk.Length>=2 && chunk.StartsWith("\"")
														 && chunk.EndsWith("\"") )
														chunk = chunk.Substring(1, chunk.Length - 2);
													if (is_process) {
														is_process = false;
														p.StartInfo.FileName = chunk;
														p.StartInfo.Arguments = "";
													}
													else {
														p.StartInfo.Arguments += ((p.StartInfo.Arguments=="")?(""):(" ")) + ((Path.DirectorySeparatorChar!='\\')?chunk_original.Replace("\\","\\\\"):chunk_original); //escape backslash since allowed on GNU/Linux systems
													}
												} 
											}
											participle="starting batch";
											string this_error = null;
											string this_notice = null;
											if (command_to_dest_path.ContainsKey(this.outputListBox.Items[index].ToString())) {
												FileInfo fi = new FileInfo(command_to_dest_path[this.outputListBox.Items[index].ToString()]);
												if (fi.Exists) {
													try {
														fi.Delete();
														this_notice = "overwriting";
													}
													catch (Exception exn){
														alNonDeletableFiles.Add(fi.FullName);
														this_error = "non-deletable existing file";
													}
												}
											}

											p.Start();
											p.WaitForExit();

											if (command_to_dest_path.ContainsKey(this.outputListBox.Items[index].ToString())) {
												FileInfo fi = new FileInfo(command_to_dest_path[this.outputListBox.Items[index].ToString()]);
												if (fi.Exists) {
													if (fi.Length>0) {
														alCommandsThatResultedInNonzeroLengthFiles.Add(this.outputListBox.Items[index].ToString());
													}
													else {
														alZeroLengthFilePaths.Add(fi.FullName);
														this_error = "conversion failure";
														alCommandsThatFailed.Add(this.outputListBox.Items[index].ToString());
													}
												}
											}
											else {
												indeterminate_count++;
												alCommandsThatFailed.Add(this.outputListBox.Items[index].ToString());
												this_error = "destination path unknown for command "+this.outputListBox.Items[index].ToString();
											}
											doneMBCount+=thisFileMBCount;
											string this_result = "OK ";
											if (this_error != null) this_result = "FAILED: "+this_error+" ";
											if (this_notice != null) this_result += " (" + this_notice + ")";
											tbStatus.Text="Running batch..."+this_result+"("+doneMBCount.ToString()+"/"+inputMBTotalCount.ToString()+" MB) {iFilesAdded:"+iFilesAdded.ToString()+"; iSkipped:"+iSkipped.ToString()+"; iFilesTotal:"+iFilesTotal.ToString()+"}";
											Application.DoEvents();
											try {
												if (inputMBTotalCount>0) {
													participle="setting progress";
													doneRatio=(decimal)doneMBCount/(decimal)inputMBTotalCount;
													int progressBar1_Value=(int)(doneRatio*(decimal)this.progressBar1.Maximum+.5m);
													progressBar1.Value = Math.Min(progressBar1_Value,progressBar1.Maximum);
												}
												else progressBar1.Value=progressBar1.Maximum;
												participle="done running batch";
											}
											catch {}
											Application.DoEvents();
										}
										else {
											tbStatus.Text="Running batch...Nothing to do. {iFilesAdded:"+iFilesAdded.ToString()+"; iSkipped:"+iSkipped.ToString()+"; iFilesTotal:"+iFilesTotal.ToString()+"}";
										}
									}
									catch (Exception exn) {
										tbStatus.Text="Could not finish writing batch in RunBatchButtonClick: "+exn.ToString();
										Console.Error.WriteLine(exn.ToString());
									}
								}//end if doRun (always runs batch unless exception during write batch further above)
								else {
									this.tbStatus.Text="Writing batch...Failed";
									Application.DoEvents();
								}
							}//end if is not remark
							else Console.Error.WriteLine("Ignoring line since is just a remark: "+thisString);
						}
						else Console.Error.WriteLine("Ignoring line during output since line is remark: "+thisString);
	
					}//end if thisString is not null
				}//end for item
				string status = "";
				if (alCommandsThatFailed.Count > 0)
				{
					status += "failed:" + alCommandsThatFailed.Count.ToString() + " ";
				}
				if (alZeroLengthFilePaths.Count > 0)
				{
					status += "conversion-failure:" + alZeroLengthFilePaths.Count.ToString() + " ";
				}
				if (alNonDeletableFiles.Count > 0)
				{
					status += "non-deletable:" + alNonDeletableFiles.Count.ToString() + " ";
				}
				if (status == "") status = "OK ";
				tbStatus.Text = "Imports..." + status + "(" + doneMBCount.ToString() + "/" + inputMBTotalCount.ToString() + " MB) {iFilesAdded:" + iFilesAdded.ToString() + "; iSkipped:" + iSkipped.ToString() + "; iFilesTotal:" + iFilesTotal.ToString() + "}";
				outputListBox.SuspendLayout();
				outputListBox.Items.Clear();
				foreach (string failed_command in alCommandsThatFailed) {
					outputListBox.Items.Add(failed_command);
				}
				outputListBox.ResumeLayout();
				try {
					if (batchStream!=null) {
						batchStream.Close();
						batchStream=null;
					}
				}
				catch {}//don't care
				
				this.progressBar1.Value=this.progressBar1.Maximum;
			}
			catch (Exception exn) {
				tbStatus.Text="Could not finish "+participle+" in RunBatchButtonClick: "+exn.ToString();
				Console.Error.WriteLine(exn.ToString());
			}
			
			//Run all at once (deprecated):
//			if (doRun) {
//				this.tbStatus.Text="Running batch (please wait for black console window to finish and disappear)...";
//				Application.DoEvents();
//				try {
//					if (iFilesAdded>0) {
//						Process p = new Process();
//						//p.StartInfo.UseShellExecute = false;
//						p.StartInfo.FileName = DoBatch_FullName;
//						p.Start();
//						p.WaitForExit();
//						tbStatus.Text="Running batch...OK {iFilesAdded:"+iFilesAdded.ToString()+"; iSkipped:"+iSkipped.ToString()+"; iFilesTotal:"+iFilesTotal.ToString()+"}";
//					}
//					else {
//						tbStatus.Text="Running batch...Nothing to do. {iFilesAdded:"+iFilesAdded.ToString()+"; iSkipped:"+iSkipped.ToString()+"; iFilesTotal:"+iFilesTotal.ToString()+"}";
//					}
//				}
//				catch (Exception exn) {
//					tbStatus.Text="Could not finish writing batch: "+exn.ToString();
//					Console.Error.WriteLine(exn.ToString());
//				}
//			}//end if doRun (always runs batch unless exception during write batch further above)
//			else {
//				this.tbStatus.Text="Writing batch...Failed";
//				Application.DoEvents();
//			}
			
			
			
		}//end RunBatchButtonClick
		
		void CopyToolStripMenuItemClick(object sender, EventArgs e)
		{
			editCopy();
		}
		
		void CutToolStripMenuItemClick(object sender, EventArgs e)
		{
			editCut();
		}
		
		void PasteToolStripMenuItemClick(object sender, EventArgs e)
		{
			editPaste();
		}
		
		void DeleteToolStripMenuItemClick(object sender, EventArgs e)
		{
			editDelete();
		}
		
		void PasteAfterToolStripMenuItemClick(object sender, EventArgs e)
		{
			editPasteAfter();
		}
		
		void SelectAllInputButtonClick(object sender, EventArgs e)
		{
			selectAllInput();
		}
		
		void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			string IEduSettings_Source_Destinations_File_FullName=null;
			StreamWriter destinationsListStream=null;
			try {
				IEduSettings_Source_Destinations_File_FullName=Path.Combine(settingsFolder_FullName, IEduSettings_Source_Destinations_File_Name);
				Console.Error.Write("Writing settings to \""+IEduSettings_Source_Destinations_File_FullName+"\"...");
				Console.Error.Flush();
				try {
					Directory.CreateDirectory(settingsFolder_FullName);
				}
				catch (Exception exn) {
					string msg = ("Could not finish creating settings folder \""+settingsFolder_FullName+"\"");
					Console.Error.WriteLine(msg+": "+exn.ToString());
				}
				destinationsListStream=new StreamWriter(IEduSettings_Source_Destinations_File_FullName);
				bool doWriteCurrent=true;
				string currentDestinationString=this.tbFolderOut.Text;
				if (currentDestinationString.EndsWith(char.ToString(Path.DirectorySeparatorChar))
				   && currentDestinationString.Length!=1) {
					currentDestinationString=currentDestinationString.Substring(0,currentDestinationString.Length-1);
				}
				if (string.IsNullOrEmpty(currentDestinationString)) {
					doWriteCurrent=false;
				}
				foreach (string destinationString in alDestinations) {
					destinationsListStream.WriteLine(destinationString);
					if (destinationString==currentDestinationString) {
						doWriteCurrent=false;
					}
				}
				if (doWriteCurrent) {
					destinationsListStream.WriteLine(currentDestinationString);
				}
				
				destinationsListStream.Close();
				destinationsListStream=null;
				Console.Error.WriteLine("OK");
			}
			catch (Exception exn) {
				Console.Error.WriteLine("Could not finish during MainFormFormClosing: "+exn.ToString());
				try {
					if (destinationsListStream!=null) {
						destinationsListStream.Close();
						destinationsListStream=null;
					}
				}
				catch {}//don't care
			}
		}//end MainFormFormClosing
		void MainFormShown(object sender, EventArgs e)
		{
			if (doAutoRefreshNextTime) {
				RefreshCamera();
				doAutoRefreshNextTime=false;
			}
		}
		
		void TbPrependCamTextChanged(object sender, EventArgs e)
		{
			
		}
		
		void DeselectAllButtonClick(object sender, EventArgs e)
		{
			deselectAllInput();
		}
		
		
		#endregion Events
		

		

		//formerly, tbFolderIn existed but that is now combo box
		void BtnInputBrowseClick(object sender, EventArgs e)
		{
			string deconstructedPath = this.cbFolderIn.Text;
			try {
				while (!Directory.Exists(deconstructedPath) && deconstructedPath.Length>0) {
					if (!deconstructedPath.Contains(sDirSep) && !Directory.Exists(deconstructedPath)) {
						deconstructedPath="";
					}
					else {
						int lastSlashIndex = deconstructedPath.LastIndexOf(Path.DirectorySeparatorChar);
						if (lastSlashIndex>=0) deconstructedPath = deconstructedPath.Substring(0,lastSlashIndex);
					}
				}
			}
			catch {} //don't care
			folderbrowserdlgMain.SelectedPath = deconstructedPath;
			DialogResult thisDialogResult = folderbrowserdlgMain.ShowDialog();
			if (thisDialogResult != DialogResult.Cancel) {
				if (!string.IsNullOrEmpty(folderbrowserdlgMain.SelectedPath)) {
					this.cbFolderIn.Text=folderbrowserdlgMain.SelectedPath;
				}
			}
		}//end BtnInputBrowseClick
		
		void FormUpperLeftOuterSettingsTableLayoutPanelPaint(object sender, PaintEventArgs e)
		{
			
		}
		
		void BtnOutputBrowseClick(object sender, EventArgs e)
		{
			string deconstructedPath = this.tbFolderOut.Text;
			try {
				while (!Directory.Exists(deconstructedPath) && deconstructedPath.Length>0) {
					if (!deconstructedPath.Contains(sDirSep) && !Directory.Exists(deconstructedPath)) {
						deconstructedPath="";
					}
					else {
						int lastSlashIndex = deconstructedPath.LastIndexOf(Path.DirectorySeparatorChar);
						if (lastSlashIndex>=0) deconstructedPath = deconstructedPath.Substring(0,lastSlashIndex);
					}
				}
			}
			catch {} //don't care
			folderbrowserdlgMain.SelectedPath = deconstructedPath;
			DialogResult thisDialogResult = folderbrowserdlgMain.ShowDialog();
			if (thisDialogResult != DialogResult.Cancel) {
				if (!string.IsNullOrEmpty(folderbrowserdlgMain.SelectedPath)) {
					this.tbFolderOut.Text=folderbrowserdlgMain.SelectedPath;
				}
			}
		}//end BtnOutputBrowseClick
		
		void TbAppDataFolderTextChanged(object sender, EventArgs e)
		{
			
		}
		string annotationTooltip="Annotation format: yyyy-MM-dd HH_mm_ss is Year-Month-Day 24-hr time with '_' instead of ':'; ff is 100ths of a second, fff is milliseconds, etc";
		//string statusBeforeTooltip="";
		string statusPrevNotTooltip="";
		void TbDTFormatStringMouseEnter(object sender, EventArgs e)
		{
			//statusBeforeTooltip=tbStatus.Text;
			tbStatus.Text=annotationTooltip;
		}
		
		void TbDTFormatStringMouseLeave(object sender, EventArgs e)
		{
			//tbStatus.Text = statusBeforeTooltip;
			tbStatus.Text = statusPrevNotTooltip;
		}
		
		void TbStatusTextChanged(object sender, EventArgs e)
		{
			if (tbStatus.Text!=annotationTooltip) {
				statusPrevNotTooltip=tbStatus.Text;
			}
		}
		
		void CopyStatusToolStripMenuItemClick(object sender, EventArgs e)
		{
			Clipboard.SetText(tbStatus.Text);
		}
		
		void AddCheckedFilesButtonClick(object sender, EventArgs e)
		{
			AddCheckedFilesToBatch();
		}
		
		void CbDeleteFromSourceCheckedChanged(object sender, EventArgs e)
		{
			
		}
		
		void OutputClearAllButtonClick(object sender, EventArgs e)
		{
			outputListBox.Items.Clear();
		}
		
		void InitTimerTick(object sender, EventArgs e)
		{
			ReloadDestinations();
			initTimer.Stop();
			initTimer.Enabled=false;
			if (!IsInitialized) {
				IsInitialized=true;
				ArrayList sub_paths = new ArrayList();
				if (Path.DirectorySeparatorChar=='\\') {
					sub_paths.Add("AVCHD\\BDMV\\STREAM");
					sub_paths.Add("PRIVATE\\AVCHD\\BDMV\\STREAM");
				}
				else {
					sub_paths.Add("AVCHD/BDMV/STREAM");
					sub_paths.Add("PRIVATE/AVCHD/BDMV/STREAM");
				}
				alNotAVideoFile_LowerCaseStrings_ToCompareCaseInsensitively.Add("thumbs.db");
				ArrayList alUnusablePaths = new ArrayList(); //see also https://github.com/expertmm/BackupGoNow/blob/master/MainForm.cs
				ArrayList alUnusableNames = new ArrayList(); //see also https://github.com/expertmm/BackupGoNow/blob/master/MainForm.cs
				if (Path.DirectorySeparatorChar=='\\') {
					//paths (must be LOWERCASE)
					alUnusablePaths.Add("c:");
					//labels (must be LOWERCASE)
					//keep comments below since they show the case sensitive version of the unusable labels
					alUnusableNames.Add("windows8_os");//Windows8_OS
					alUnusableNames.Add("lenovo");//LENOVO
					alUnusableNames.Add("hp_recovery");//HP_RECOVERY
					alUnusableNames.Add("recovery");//RECOVERY
					alUnusableNames.Add("os");//OS
					alUnusableNames.Add("factory_image");//FACTORY_IMAGE
					//alUnusableNames.Add("RECOVERY");//Recovery
					alUnusableNames.Add("dellutility");//DELLUTILITY
					/*
					alSources.Add(Path.Combine("D:\\",sub_path));
					alSources.Add(Path.Combine("E:\\",sub_path));
					alSources.Add(Path.Combine("F:\\",sub_path));
					alSources.Add(Path.Combine("G:\\",sub_path));
					alSources.Add(Path.Combine("H:\\",sub_path));
					alSources.Add(Path.Combine("I:\\",sub_path));
					alSources.Add(Path.Combine("J:\\",sub_path));
					alSources.Add(Path.Combine("K:\\",sub_path));
					alSources.Add(Path.Combine("L:\\",sub_path));
					alSources.Add(Path.Combine("M:\\",sub_path));
					alSources.Add(Path.Combine("N:\\",sub_path));
					alSources.Add(Path.Combine("O:\\",sub_path));
					alSources.Add(Path.Combine("P:\\",sub_path));
					alSources.Add(Path.Combine("Q:\\",sub_path));
					alSources.Add(Path.Combine("R:\\",sub_path));
					alSources.Add(Path.Combine("S:\\",sub_path));
					alSources.Add(Path.Combine("T:\\",sub_path));
					alSources.Add(Path.Combine("U:\\",sub_path));
					alSources.Add(Path.Combine("V:\\",sub_path));
					alSources.Add(Path.Combine("W:\\",sub_path));
					alSources.Add(Path.Combine("X:\\",sub_path));
					alSources.Add(Path.Combine("Y:\\",sub_path));
					alSources.Add(Path.Combine("Z:\\",sub_path));
					*/
				}
				else {
					//paths: must be lowercase
					alUnusablePaths.Add("/");
					alUnusablePaths.Add("/boot");
					alUnusablePaths.Add("/home");
					alUnusablePaths.Add("/sys");
					//alSources.Add(Path.Combine("/media/CANON",sub_path));
				}
				DriveInfo[] allDrives = DriveInfo.GetDrives();
				foreach (string sub_path in sub_paths) {
					foreach (DriveInfo di in allDrives) {
						try {
							if (!alUnusableNames.Contains(di.VolumeLabel.ToLower())) {
								if (!alUnusablePaths.Contains(di.RootDirectory.FullName.ToLower())) {
									string full_source_path = Path.Combine(di.RootDirectory.FullName, sub_path);
									try {
										if (Directory.Exists(full_source_path))
										{
											alSources.Add(full_source_path);
											Console.Error.WriteLine("success:");
											Console.Error.WriteLine("  msg: added drive");
											Console.Error.WriteLine("  name: " + di.Name);
											Console.Error.WriteLine("  label: " + di.VolumeLabel);
											Console.Error.WriteLine("  path: " + di.RootDirectory.FullName);
											Console.Error.WriteLine("  full_source_path: " + full_source_path);
										}
									}
									catch (Exception exn) {
										Console.Error.WriteLine("verbose_message: Could not finish adding source '" + full_source_path+"'");
									}
								}
								else Console.Error.WriteLine("notice: skipped drive since path is '"+di.RootDirectory.FullName+"'");
							}
							else Console.Error.WriteLine("notice: skipped drive since label is '"+di.VolumeLabel+"'");
						}
						catch (Exception exn) {
							Console.Error.WriteLine ("Could not finish checking drive: " + exn.ToString());
						}
					}
				}
				try {
					settingsFolder_FullName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), sMyName);
					Console.Error.WriteLine("settingsFolder_FullName: "+settingsFolder_FullName);
					string IEduSettings_Source_Destinations_File_FullName=Path.Combine(settingsFolder_FullName, IEduSettings_Source_Destinations_File_Name);
					Console.Error.WriteLine("IEduSettings_Source_Destinations_File_FullName: "+IEduSettings_Source_Destinations_File_FullName);
					if (File.Exists(IEduSettings_Source_Destinations_File_FullName)) {
						StreamReader destinationsListStream=new StreamReader(IEduSettings_Source_Destinations_File_FullName);
						string thisLineDestinationString="";
						tbFolderOut.Text=tbFolderOut.Text.Trim();
						DirectoryInfo documentsDirInfo=new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
						DirectoryInfo profileDirInfo=documentsDirInfo.Parent;
						while ( (thisLineDestinationString=destinationsListStream.ReadLine()) != null ) {
							thisLineDestinationString=thisLineDestinationString.Trim();
							if (!string.IsNullOrEmpty(thisLineDestinationString)) {
								alDestinations.Add(thisLineDestinationString);
								if (string.IsNullOrEmpty(tbFolderOut.Text)) {
									thisLineDestinationString=thisLineDestinationString.Replace("%USERPROFILE%",profileDirInfo.FullName);
									tbFolderOut.Text=thisLineDestinationString;
								}
							}
						}
						destinationsListStream.Close();
					}
				}
				catch (Exception exn) {
					string msg = ("Could not finish reading \""+IEduSettings_Source_Destinations_File_Name+"\".");
					Console.Error.WriteLine(msg+": "+exn.ToString());
				}
				
				try {
					StreamReader srCommands=new StreamReader(IEduSettings_Commands_File_Name);//"commands.txt"
					string commandLineString="";
					cbxCopyCommand.Text="";//cbxCopyCommand.Text.Trim();
					while ( (commandLineString=srCommands.ReadLine()) != null ) {
						commandLineString=commandLineString.Trim();
						if (!string.IsNullOrEmpty(commandLineString)) {
							cbxCopyCommand.Items.Add(commandLineString);
						}
					}
					srCommands.Close();
				}
				catch (Exception exn) {
					string msg = ("Could not finish reading \""+IEduSettings_Commands_File_Name+"\".");
					Console.Error.WriteLine(msg+": "+exn.ToString());
				}
				
				Console.Error.WriteLine("verbose_message: about to check sources...");
				CheckSources();
				Console.Error.WriteLine("verbose_message: done checking sources");
				Application.DoEvents();
				Console.Error.WriteLine("verbose_message: about to load settings...");
				bool settings_enable = LoadSettings();
				Console.Error.WriteLine("verbose_message: done loading settings");
	
				if (cbxCopyCommand.Items.Count>0) {
					//next line: if Count is not checked above, next line FAILS UNLESS IN DEBUGGER
					string found = null;
					int found_i = -1;
					Console.Error.WriteLine("verbose_message: about to examine "+cbxCopyCommand.Items.Count.ToString()+" command(s)");
					for (int c_i=0; c_i<cbxCopyCommand.Items.Count; c_i++) {
						string s = cbxCopyCommand.Items[c_i].ToString();
						Console.Error.WriteLine("verbose_message: examining '"+s+"'");
						ArrayList parts = split_respecting_quotes(s, ' ', '"',true);
						if (parts!=null) {
							for (int i=0; i<parts.Count; i++) {
								try {
									string path = parts[i].ToString();
									if (path.Length>2&&path.StartsWith("\"")&&path.EndsWith("\"")) {
										path = path.Substring(1,path.Length-2);
									}
									if (File.Exists(path)||(Path.DirectorySeparatorChar=='\\'&&File.Exists(path+".exe"))) {
										//TODO: detect when executable is in path (to allow auto-selection of command in that case)
										found = s;
										found_i = c_i;
										Console.Error.WriteLine("success: found command '"+path+"'");
									}
									else Console.Error.WriteLine("verbose_message: '"+path+"' is not an existing executable");
								}
								catch (Exception exn) {
									string msg = "Couldn't finish accessing "+parts[i];
									Console.Error.WriteLine(msg+": "+exn.ToString()); //doesn't really matter
								}
							}
						}
					}
					if (found==null) cbxCopyCommand.SelectedIndex=0; //cbxCopyCommand.Text=this.cbxCopyCommand.Items[0].ToString();
					else cbxCopyCommand.SelectedIndex=found_i;
				}
				else {
					MessageBox.Show("No copy commands! Please type in 'Copy command' area or add to \"commands.txt\" e.g. add the line:\n\"C:\\Program Files (x86)\\WinFF\\ffmpeg.exe\" -y -i \"%INFILE%\" -r 29.97 -s 1280x720 -q:v 4 -vcodec mpeg2video -acodec copy -f mpegts \"%OUTFILE%\"");
				}
				for (int i=Columns_Count; i<Columns_Max; i++) {
					inputListView.Columns.Add("",100);
					Columns_Count++;
				}
				inputListView.Columns[Column_Name].Text="Name";
				inputListView.Columns[Column_CreationTime].Text="Date created";
				inputListView.Columns[Column_Size].Text="Size";
				inputListView.Columns[Column_FullName].Text="FullName";
				inputListView.Columns[Column_FullName].Width=800;
				this.tbStatus.Text="Welcome to "+sMyName+"!";
				UpdateCheckedCount();
			}//end if not IsInitialized yet
			else Console.Error.WriteLine("WARNING: TimerTick after already initialized!");
		}//end InitTimerTick
		
		void MarkDoneButtonClick(object sender, EventArgs e)
		{
			markDoneButton.Enabled=false;
			int doneCount=0;
			try {
				for (int index=0; index<inputListView.Items.Count; index++) {
					if (inputListView.Items[index].Checked) {
						FileInfo thisFI=new FileInfo(inputListView.Items[index].SubItems[Column_FullName].Text);
						setWhetherFileWasMarkedDone(thisFI,tbDTFormatString.Text,tbPrependCam.Text,tbAppDataFolder.Text,true);
						if (!string.IsNullOrEmpty(tbProjectName.Text)) {
							setMetaDataValue(thisFI,tbDTFormatString.Text,tbPrependCam.Text,tbAppDataFolder.Text,"project",tbProjectName.Text);
						}
						doneCount++;
						inputListView.Items[index].Checked=false;
					}
				}
			}
			catch (Exception exn) {
				Console.Error.WriteLine("Could not finish MarkDoneButtonClick:"+exn.ToString());
			}
			tbStatus.Text="Marked "+doneCount.ToString()+" as done.";
			markDoneButton.Enabled=true;
		}
		
		void RefreshDestinationsToolStripMenuItemClick(object sender, EventArgs e)
		{
			ClearDestinationFileLists();
		}
		
		void MarkUnusableButtonClick(object sender, EventArgs e)
		{
			markUnusableButton.Enabled=false;
			if (!string.IsNullOrEmpty(this.unusableTextBox.Text) || allowMarkingUnusableWithoutReasonToolStripMenuItem.Checked) {
				int doneCount=0;
				try {
					for (int index=0; index<inputListView.Items.Count; index++) {
						if (inputListView.Items[index].Checked) {
							setUnusableReason(new FileInfo(inputListView.Items[index].SubItems[Column_FullName].Text),tbDTFormatString.Text,tbPrependCam.Text,tbAppDataFolder.Text,this.unusableTextBox.Text);
							doneCount++;
							inputListView.Items[index].Checked=false;
						}
					}
				}
				catch (Exception exn) {
					Console.Error.WriteLine("Could not finish MarkUnusableButtonClick:"+exn.ToString());
				}
				tbStatus.Text="Marked "+doneCount.ToString()+" as unusable.";
			}
			else {
				MessageBox.Show("Please enter a reason in the box first (nothing was marked).");
			}
			markUnusableButton.Enabled=true;
		}
		
		void InputListViewDoubleClick(object sender, EventArgs e)
		{
			string startFileName="";
			if(inputListView.SelectedItems.Count > 0) {
				try {
					startFileName="\""+inputListView.SelectedItems[0].SubItems[Column_FullName].Text+"\"";
					System.Diagnostics.Process.Start((Path.DirectorySeparatorChar!='\\')?startFileName.Replace("\\","\\\\"):startFileName);
				}
				catch (Exception exn) {
					string msg=("Could not finish starting file "+startFileName+":"+exn.ToString());
					Console.Error.WriteLine(msg);
					tbStatus.Text=msg;
					MessageBox.Show("The file is not accessible. Please make sure the device is plugged in and that there is not a problem with the file '"+startFileName+"'",sMyName);
				}
				inputListView.SelectedItems[0].Checked=!inputListView.SelectedItems[0].Checked;
			}
			double_click_tick = Environment.TickCount;
		}
		
		void InputListViewClick(object sender, EventArgs e)
		{
			if ( (Control.ModifierKeys & Keys.Shift) == Keys.Shift ) {
				
			}
		}
		
		void InputListViewItemActivate(object sender, EventArgs e)
		{
			if ( (Control.ModifierKeys & Keys.Shift) == Keys.Shift ) {
				
			}
		}
		
		void InputListViewSelectedIndexChanged(object sender, EventArgs e)
		{
			/*
			if (inputListView.SelectedIndices.Count>0) {
				Application.DoEvents(); //ensure we don't get the OLD indices
					if (inputListView.SelectedIndices.Count>1) {//if (  (Control.ModifierKeys & Keys.Shift)  ==  Keys.Shift  ) {
					//if (inputListView.SelectedIndices.Count>1) {
						for (int ii=0; ii<inputListView.SelectedIndices.Count; ii++) {
							if (inputListView.Items[inputListView.SelectedIndices[ii]].Checked) inputListView.Items[inputListView.SelectedIndices[ii]].Checked=true;
						}
					//}
					
					int index=inputListView.SelectedIndices[inputListView.SelectedIndices.Count-1];
					if (inputListView.Items[index].Checked) inputListView.Items[index].Checked=true;
					
				}
			}
			*/
		}
		
		#region unbug bad M$ ListView behavior
		// these event handlers fix automatic checkbox check/uncheck during multi-select (checks all but one!). As per Matt Nelson. <http://stackoverflow.com/questions/2017170/c-sharp-listview-with-checkboxes-automatic-checkbox-checked-when-multi-select-r> 7 Mar 2012. 29 Sep 2016.
		void InputListViewMouseLeave(object sender, EventArgs e)
		{
			is_list_view_mouse_down = false;
		}
		
		void InputListViewMouseUp(object sender, MouseEventArgs e)
		{
			is_list_view_mouse_down = false;
		}
		void InputListViewMouseDown(object sender, MouseEventArgs e)
		{
			is_list_view_mouse_down = true;
		}
		#endregion unbug bad M$ ListView behavior
		
		
		void InputListViewItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (Environment.TickCount-double_click_tick>1000) {
				if(is_list_view_mouse_down) {
					e.NewValue = e.CurrentValue;
				}
			}
			//else allow double-click event to revert it programmatically
		}
		
		void CbFolderInLeave(object sender, EventArgs e)
		{
			RefreshAll(false); //false: only refreshes if input string changed since last RefreshAll
		}
		
		
		
		void CbFolderInKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter) {
				RefreshAll(false); //false: only refreshes if input string changed since last RefreshAll
			}
		}
		
		void CbFolderInSelectedIndexChanged(object sender, EventArgs e)
		{
			RefreshAll(false); //false: only refreshes if input string changed since last RefreshAll
		}
		
		void CbFolderInMouseDown(object sender, MouseEventArgs e)
		{
			//cbFolderIn.ForeColor=DefaultTextColor;  // this is a bad idea--doesn't work until hovering over the colored one, and even if doesn't change, becomes black
		}
		
		void CbxCopyCommandSelectedIndexChanged(object sender, EventArgs e)
		{
			
		}
		
		void CbxCopyCommandTextChanged(object sender, EventArgs e)
		{
			//cbxCopyCommand.Size = new Size(cbxCopyCommand.Parent.Width, cbxCopyCommand.Size.Height); //makes it go outside of the parent in mono
		}
	}//end MainForm
}//end namespace
