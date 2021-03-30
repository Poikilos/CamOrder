/*
 * Created by SharpDevelop.
 * User: jgustafson
 * Date: 10/1/2015
 * Time: 4:25 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace ExpertMultimedia
{
	/// <summary>
	/// Description of CachedDir.
	/// </summary>
	public class CachedDir
	{
		public FileInfo[] files=null;
		public string DirectoryFullName=null;
		public DirectoryInfo directoryInfo=null;
		public CachedDir()
		{
		}
	}
}
