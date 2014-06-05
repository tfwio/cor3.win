/* oOo * 11/20/2007 : 4:41 PM */
using System;
using System.Windows.Forms;
using Windows.Shell32;

namespace System.User
{
	public class ListViewPath : User32
	{
		static public void LV_SetSmallImageList(ListView lv, string path, SHGFI icon_size_mode)
		{
			SendMessage(
				lv.Handle,
				(uint)ILM.SETIMAGELIST,
				(uint)LVSIL.SMALL,
				GetImageListPointer( path , icon_size_mode )
			);
		}
	}
}
