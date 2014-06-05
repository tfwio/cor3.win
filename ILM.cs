/* oOo * 11/20/2007 : 4:41 PM */
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace System
{
	partial class User32 : WindowsInterop
	{
		public enum ILM : uint
		{
			GETIMAGELIST		= (0x1002),
			SETIMAGELIST		= (0x1003),
			GETITEMCOUNT		= (0x1004),
		}
	}
}
