using System;
using System.Windows.Forms;
using System.Xml;
//by By circumpunct, 9 Sep 2006
//http://www.codeproject.com/Articles/15530/Quick-and-Dirty-IEduSettings-Persistence-with-XML
//Public Domain
//modified by ProtoArmor
//-new constructor
//-separated sFileFullNameDefault from sFileFullName
//-separated Load&Save from Get* & Put*
//-changed ToInt16 to ToInt32

namespace ExpertMultimedia {
	public class IEduSettings {
		public bool AutoSaveOnPut=false;
		XmlDocument xmlDocument = new XmlDocument();
		private static string sFileFullNameDefault=Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+"//Iedu-UNKNOWNPROGRAM-settings.xml";
		string sFileFullName=null;//Application.StartupPath+"//settings.xml"
		#region constructors
		public IEduSettings() {
			Console.Error.WriteLine("Warning: default path was used for IEduSettings (\""+sFileFullNameDefault+"\")");
			Init(null,false);
		}
		//~IEduSettings() {
		//}
		public IEduSettings(string set_DocumentPath) {
			Init(set_DocumentPath,true);
		}
		public void Init(string set_DocumentPath, bool bLoad) {
			if (!string.IsNullOrEmpty(set_DocumentPath)) sFileFullName=set_DocumentPath;
			else sFileFullName=sFileFullNameDefault;
			xmlDocument.LoadXml("<settings></settings>");//this creates the settings node by loading this literal string as xml
			if (bLoad) {
				Load(sFileFullName);
			}
			//try {xmlDocument.Load(sFileFullName);}
			//catch {xmlDocument.LoadXml("<settings></settings>");}
		}
		#endregion constructors
		
		public bool Load(string set_DocumentPath) {
			bool result = false;
			try {
				xmlDocument.Load(sFileFullName); //crashes with Invalid URI: A Dos path must be rooted, for example, 'c:\'. --occurs when different camera with different sub_path is plugged in then Reload is pressed
				//TODO: fix crash caused by backslash in path such as @"/media/FBM\jgustafson/CANON/AVCHD/BDMV/STREAM/integratoreduimport.xml" when using a domain user
				result = true;
			}
			catch (Exception exn) {
				Console.WriteLine("Could not finish IEduSettings Load '"+sFileFullName+"': "+exn.ToString());
			}
			return result;
		}
		
		public int GetSetting(string xPath, int defaultValue) {
			return Convert.ToInt32(GetSetting(xPath, Convert.ToString(defaultValue)));
		}
		
		public void PutSetting(string xPath, int value) {
			PutSetting(xPath, Convert.ToString(value));
		}
		
		public string GetSetting(string xPath,  string defaultValue) {
			XmlNode xmlNode=xmlDocument.SelectSingleNode("settings/"+xPath);
			if (xmlNode!=null) return xmlNode.InnerText;
			else return defaultValue;
		}
		
		public void PutSetting(string xPath,  string value) {
			XmlNode xmlNode=xmlDocument.SelectSingleNode("settings/"+xPath);
			if (xmlNode==null) xmlNode=createMissingNode("settings/"+xPath);
			xmlNode.InnerText=value;
			if (AutoSaveOnPut) xmlDocument.Save(sFileFullName);
		}
		
		public void Save(string set_sFileFullName) {
			xmlDocument.Save(set_sFileFullName);
			sFileFullName=set_sFileFullName;
		}
		public void Save() {
			Save(sFileFullName);
		}
		
		private XmlNode createMissingNode(string xPath) {
			string[] xPathSections=xPath.Split('/');
			string currentXPath="";
			XmlNode testNode=null;
			
			XmlNode currentNode=null;//=xmlDocument.SelectSingleNode("settings");
			currentNode = xmlDocument.SelectSingleNode("settings");
			foreach (string xPathSection in xPathSections) {
				//Console.Error.WriteLine("ERROR: xPathSection (a segment of xml path) is null");
				currentXPath += xPathSection;
				testNode=xmlDocument.SelectSingleNode(currentXPath);
				if (testNode==null) {
					currentNode.InnerXml+="<"+xPathSection+"></"+xPathSection+">";
				}
				currentNode=xmlDocument.SelectSingleNode(currentXPath);
				currentXPath+="/";
			}
			return currentNode;
		}//end createMissingNode
	}//end class IEduSettings
}//end namespace
