﻿/*
 * Created by SharpDevelop.
 * User: Poikilos
 * Date: 11/30/2017
 * Time: 11:26 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Xml.Serialization;
using System.IO; //StringWriter etc
//namespace ThisBreaksEverything
//NOTE: the SerializeObject method breaks loading things from resx from MainForm.Designer.cs if the method is in MainForm.cs for some reason (even in different namespace and object!)--I've heard order of compiling matters in some cases with resx usage
namespace ExpertMultimedia
{
	
	/// <summary>
	/// Description of ManualSerialization.
	/// </summary>
	public static class ManualSerialization
	{
		//public ManualSerialization()
		//{
		//}
//namespace ThisBreaksEverything {
	//public static class ManualSerialization {
		public static string SerializeObject<T>(this T toSerialize)
		{
			Console.WriteLine(typeof(T).Name);			 // PRINTS: "Super", the base/superclass -- Expected output is "Sub" instead
			Console.WriteLine(toSerialize.GetType().Name); // PRINTS: "Sub", the derived/subclass
	
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
			StringWriter textWriter = new StringWriter();
	
			// And now...this will throw and Exception!
			// Changing new XmlSerializer(typeof(T)) to new XmlSerializer(subInstance.GetType()); 
			// solves the problem
			xmlSerializer.Serialize(textWriter, toSerialize);
			return textWriter.ToString();
		}
	//}
//}
	}
}
