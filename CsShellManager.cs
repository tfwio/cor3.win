/*
TFOOO - With SharpDevelop2
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

using w32;
using w32.shell;
using w32.user;

namespace Windows.CommonControls.Interop
{
	public class CsShellManager
	{
		Dictionary<string,List<string>> ActiveStuff = new Dictionary<string,List<string>>();
		
		
		public bool ShowListViewDirectories {
			get { return showListViewDirectories; }
			set { showListViewDirectories = value; }
		} bool showListViewDirectories = false;
		
		public string CurrentPath {
			get { return currentPath; }
			set { currentPath = value; }
		} string currentPath;
		
		public CsShellDirectory CurrentShellPath {
			get { return currentShellPath; }
			set { currentShellPath = value; }
		} CsShellDirectory currentShellPath = new CsShellDirectory();
		
		protected CsShellDirectory ActiveShellDirectory;
		
		public CsShellManager(string Path) {
			currentPath = Path;
			currentShellPath = PopulateDictionaries();
		}

		public string checkType(string path)
		{
			foreach (KeyValuePair<string,List<string>> item in ActiveStuff)
				foreach (string st in item.Value)
			{
				if ( path == st.ToLower() ) return item.Key;
			}
			return string.Empty;
		}
		public void Initialize(ListView listView, SHGFI iconReadStyle, SHGFI iconWriteStyle)
		{
			Debug.Print("Initialize Directories\n");
			foreach (
				KeyValuePair<string,CsShellFile_ListItem> mojo
				in CurrentShellPath.Directories.Paths)
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(mojo.Value.Path);
				using (CsShellFile shellFile = new CsShellFile(mojo.Value.Path,iconWriteStyle))
				{
					ListViewItem listViewItem = new ListViewItem(directoryInfo.Name);
					listViewItem.ImageIndex=(int)shellFile.IIcon;
					if (listView.Groups[shellFile.TypeName]==null)
						listView.Groups.Add(shellFile.TypeName,shellFile.TypeName);
					listViewItem.Group = listView.Groups[shellFile.TypeName];
					listViewItem.SubItems.Add(shellFile.TypeName);
					listView.Items.Add(listViewItem);
				}
				
			}
			Debug.Print("Initialize Files\n");
			foreach (
				KeyValuePair<string,CsShellFile_ListItem> mojo
				in CurrentShellPath.Files.Paths)
			{
				FileInfo FI = new FileInfo(mojo.Value.Path);
				ListViewItem listItem = new ListViewItem(FI.Name);
				
				SHFileInfo shFile = SHFileInfo.Create(mojo.Value.Path,iconWriteStyle);
				
				listItem.ImageIndex=(int)shFile.iIcon;
				
				if (listView.Groups[shFile.szTypeName]==null)
					listView.Groups.Add(shFile.szTypeName,shFile.szTypeName);
				
				listItem.Group = listView.Groups[shFile.szTypeName];
				listItem.SubItems.Add(mojo.Value.TypeName);
				listItem.SubItems.Add(checkType(Path.GetExtension(mojo.Value.Path).Trim('.')));
				listView.Items.Add(listItem);
				User32.DestroyIcon(shFile.hIcon);
			}
			
			User32.SendMessage(listView.Handle,(uint)wm_message.LVM_SETIMAGELIST,(uint)LVSIL.NORMAL,Shell32.ImgListPtr(CurrentPath, iconReadStyle|SHGFI.SYSICONINDEX));
			User32.SendMessage(listView.Handle,(uint)wm_message.LVM_SETIMAGELIST,(uint)LVSIL.SMALL, Shell32.ImgListPtr(CurrentPath, iconReadStyle|SHGFI.SYSICONINDEX));
		}
		
		protected CsShellDirectory PopulateDictionaries()
		{
			Debug.Print("Populating Dictionaries");
			ActiveShellDirectory = new CsShellDirectory().Create();
			try
			{
				Debug.Print("> Enumerating Files");
				foreach (string ss in Directory.GetFiles(currentPath))
				{
					FileInfo f = new FileInfo(ss);
					using (CsShellFile sh = new CsShellFile(ss, SHGFI.SMALLICON |SHGFI.ICON|SHGFI.TYPENAME))
					{
						if (!ActiveShellDirectory.Files.Paths.ContainsKey(f.FullName))
							ActiveShellDirectory.Files.Paths.Add(f.FullName,sh.GetListItem());
						if (!ActiveShellDirectory.Files.IcoPtr.ContainsKey(ss))
							ActiveShellDirectory.Files.IcoPtr.Add(f.FullName,sh.IIcon);
					}
				}
				
				Debug.Print("> File Count: {0}",ActiveShellDirectory.Files.Paths.Count);
				Debug.Print("> Enumerating Directories");
				foreach (string ss in Directory.GetDirectories(currentPath))
				{
					DirectoryInfo d = new DirectoryInfo(ss);
					CsShellFile_ListItem sx = new CsShellFile_ListItem();
					sx.Path = ss;
					sx.Name = Path.GetFileName(ss);
					SHFileInfo sh = Shell32.StrIco(ss, SHGFI.SMALLICON |SHGFI.ICON|SHGFI.TYPENAME);
					sx.TypeName = sh.szTypeName;
					if (!ActiveShellDirectory.Directories.Paths.ContainsKey(d.Name))
						ActiveShellDirectory.Directories.Paths.Add(d.Name,sx);
					if (!ActiveShellDirectory.Directories.IcoPtr.ContainsKey(ss))
						ActiveShellDirectory.Directories.IcoPtr.Add(ss,sh.hIcon);
					User32.DestroyIcon(sh.hIcon);
				}
				Debug.Print("> Directory Count: {0}",ActiveShellDirectory.Directories.Paths.Count);
			}
			catch (IOException e){
				MessageBox.Show(e.Message,e.Source);
			}
			catch (Exception e){
				Debug.Print("{0}",e);
			}
			return ActiveShellDirectory;
		}
	}
}
