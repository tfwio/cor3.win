/* oOo * 11/20/2007 : 4:41 PM */
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.User;
using System.Windows.Forms;

//using System.Cor3.man;

namespace System
{
	partial class User32 : WindowsInterop {
		
		public interface IClipboardDummy
		{
			event ThrowMessage CBMsg;
			void eBtnActivate(object sender, EventArgs e);
			void ThrowMsg(IntPtr lp, Message msg);
			IntPtr AddListener(IntPtr hWnd);
			void DestroyHandle();
			int NumFormats { get; }
			CF_Enu[] CFEnum { get; }
		}
	}
}
