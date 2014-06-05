/* oOo * 11/20/2007 : 4:41 PM */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System
{
	partial class WindowsInterop
	{
		////////////////////////////////////////////////////////////////////////////
		[StructLayout(LayoutKind.Sequential)]
		public struct DLLVERSIONINFO {
			public int cbSize;
			public int dwMajorVersion;
			public int dwMinorVersion;
			public int dwBuildNumber;
			public int dwPlatformID;
		}
	}
}