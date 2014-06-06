#region User/License
// oio * 7/19/2012 * 8:47 AM

// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
#endregion
using System;
using Windows.CommonControls.Interop;
using System.IO;
using System.Windows.Forms;

using w32;
using w32.shell;
using w32.user;

namespace Windows.CommonControls
{
	class CsShellItem<T> : IDisposable
	{
		#region Constants
		internal const SHGFI _tn = SHGFI.TYPENAME;
		internal const SHGFI _sys_ico = SHGFI.SYSICONINDEX|SHGFI.ICON;
		internal const SHGFI _sys_ico_sm = _sys_ico|SHGFI.SMALLICON; // |_tn
		internal const SHGFI _sys_ico_lrg = _sys_ico|SHGFI.LARGEICON; // |_tn
		internal const SHGFI _sys_lrg = _sys_ico_lrg|_tn;
		#endregion
		
		protected T ShellItem;
		
		virtual public void Dispose()
		{
		}
	}
	class CsShellFile : CsShellItem<SHFileInfo>
	{
		public IntPtr HIcon { get { return ShellItem.hIcon; } }
		public IntPtr IIcon { get { return ShellItem.iIcon; } }
		
		public int IntIcon { get { return Convert.ToInt32(IIcon); } }
		
		public string TypeName { get { return ShellItem.szTypeName; } }
		public string DisplayName { get { return ShellItem.szDisplayName; } }
		
		string path;
		
		#region .ctor
		
		public CsShellFile(string path, SHGFI iconSize)
		{
			this.path = path;
			ShellItem = Shell32.StrIco( path, iconSize );
		}
		public CsShellFile(string path) : this(path,_sys_ico_sm)
		{
		}
		
		#endregion
		
		public void AddChild(TreeNode Parent, DirectoryInfo dir)
		{
			TreeNode node = new TreeNode(dir.Name);
			node.ImageIndex = (int)IIcon;
			node.SelectedImageIndex = (int)IIcon;
			Parent.Nodes.Add(node);
		}
		
		public CsShellFile_ListItem GetListItem()
		{
			return new CsShellFile_ListItem(DisplayName,path,TypeName);
		}
		
		void DestroyIcon() { User32.DestroyIcon(ShellItem.hIcon); }
		public override void Dispose()
		{
			this.path = null;
			DestroyIcon();
		}
	}
}
