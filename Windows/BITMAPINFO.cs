/* oOo * 11/20/2007 : 4:41 PM */
using System;
using System.Runtime.InteropServices;

namespace System.Gdi
{

	public struct BITMAPINFO
	{
		public BITMAPINFOHEADER bmpheader;
		RGBQUAD[] bmiColors;
//		public BITMAPINFO() {  }
//		public BITMAPINFO(int numquads)
//		{
//		}
	}
}
