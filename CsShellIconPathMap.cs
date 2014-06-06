/*
TFOOO - With SharpDevelop2
 */
using System;
using System.Collections.Generic;

namespace Windows.CommonControls.Interop
{
	/// <summary>
	/// Contains the files in the given fDir structure.<br/>
	/// equivelants...
	/// </summary><seealso cref="fDir">fDir</seealso>
	/// <code>fDir.Files</code>
	public struct CsShellIconPathMap
	{
		public Dictionary<string,CsShellFile_ListItem> Paths;
		public Dictionary<string,IntPtr> IcoPtr;
	}
}
