﻿/* oOo * 11/20/2007 : 4:41 PM */
using System;
using System.Runtime.InteropServices;
// these types are from:
//http://support.microsoft.com/default.aspx?scid=kb;en-us;812425
namespace System
{
	partial class WindowsInterop
	{
		[StructLayout(LayoutKind.Sequential)]
		public sealed class POINTL
		{
			public int x;
			public int y;
		}
	}
}
