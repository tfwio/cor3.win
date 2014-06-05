using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace System
{
	partial class WindowsInterop {
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public sealed class FILEGROUPDESCRIPTORW
		{
			public uint cItems;
			public FILEDESCRIPTORW[] fgd;
		}
	}
}
