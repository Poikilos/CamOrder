/*
 * Created by SharpDevelop.
 * User: jgustafson
 * Date: 11/13/2015
 * Time: 10:27 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace ExpertMultimedia
{
	/// <summary>
	/// DEPRECATED. Use YAMLObject instead.
	/// </summary>
	public class IEduYaml
	{
		public IEduYaml()
		{
			
			
			
		}
		
		/// <summary>
		/// Splits a YAML assignment (does not take arrays into account).
		/// </summary>
		/// <param name="nameString"></param>
		/// <param name="valueString">Returns the value from the YAML line, only if after a colon (double quote is only escape sequence processed) or null if no value (but instead returns empty string if the empty string was quoted)</param>
		/// <param name="line"></param>
		/// <returns>whether variable has a name and a colon after it</returns>
		public static bool getNameAndValueFromYAMLLine(out string nameString, out string valueString, string line) {
			bool IsOK=false;
			valueString=null;//string valueString=null;
			nameString=null;//string nameString=null;
			try {
				if (line!=null) {
					int signIndex=line.IndexOf(":");
					if (signIndex>=0) {
						nameString=line.Substring(0,signIndex);
						if (nameString!=null) {
							nameString=nameString.Trim();
							if (!string.IsNullOrEmpty(nameString)) IsOK=true;
							valueString=line.Substring(signIndex+1);
							if (valueString!=null) {
								valueString=valueString.Trim();
								if (valueString.Length==0) valueString=null;
								else if (valueString.Length>=2&&valueString.StartsWith("\"")&&valueString.EndsWith("\"")) {
									valueString=valueString.Substring(1,valueString.Length-2);
									valueString=valueString.Replace("\\\"","\"");
								}
								else if (valueString.Length>=2&&valueString.StartsWith("'")&&valueString.EndsWith("'")) {
									valueString=valueString.Substring(1,valueString.Length-2);
								}
							}
						}
					}
				}
			}
			catch (Exception exn) {
				IsOK=false;
				Console.Error.WriteLine("Could not finish getting NameAndValueFromYMLLine "+( (line!=null)?("\""+line+"\""):"null" )+": "+exn.ToString());
			}
			return IsOK;
		}		
	}
}
