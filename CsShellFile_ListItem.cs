/*
TFOOO - With SharpDevelop2
 */
using System;
using System.Runtime.InteropServices;

namespace Windows.CommonControls.Interop
{
	/// <summary>
	/// not supported<br />
	/// I don't know if the program still uses this
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct CsShellFile_ListItem
	{
		public string Name, Path, TypeName;
		public CsShellFile_ListItem(string n, string p, string t)
		{
			this.Name=n;
			this.Path = p;
			this.TypeName = t;
		}
	}
}
