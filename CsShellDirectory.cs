/*
TFOOO - With SharpDevelop2
 */
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Windows.CommonControls.Interop
{
	/// <summary>
	/// the structure used to store path information to memory<br />
	/// this structure is criticle
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct CsShellDirectory
	{
		public string Name;
		public CsShellIconPathMap Files;
		public CsShellIconPathMap Directories;
		
		/// <summary>
		/// Initializes (Empty) Dictionaries.
		/// </summary>
		/// <returns></returns>
		public CsShellDirectory Create()
		{
			Directories.Paths = new Dictionary<string,CsShellFile_ListItem>();
			Directories.IcoPtr = new Dictionary<string,IntPtr>();
			Files.Paths = new Dictionary<string,CsShellFile_ListItem>();
			Files.IcoPtr = new Dictionary<string,IntPtr>();
			return this;
		}
	}
}
