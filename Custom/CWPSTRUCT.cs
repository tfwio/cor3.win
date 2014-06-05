using System;
using System.Runtime.InteropServices;

namespace System.Shell32
{
	partial class WindowsInterop {
		[StructLayout(LayoutKind.Sequential)]
		public struct CWPSTRUCT
		{
			public IntPtr lparam;
			public IntPtr wparam;
			public int message;
			public IntPtr hwnd;
		}
	}
}
