/*
TFOOO - With SharpDevelop2
 */
using System;
using System.Collections.Generic;
using Windows.CommonControls.Interop;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

using w32;
using w32.shell;
using w32.user;

namespace Windows.CommonControls
{
	public class CsShellFileView : System.Windows.Forms.UserControl
	{
		protected CsShellManager ShellManager;
		
		#region Obsolete Dictionary Elements
		#if FALSE
		Dictionary<string,KntGroup>	KontentCFG = new Dictionary<string,KntGroup>();
		Dictionary<string,typex>	Types = new Dictionary<string,typex>();
		#endif
		#endregion
		
		#region Actions
		void FolderBrowserAction()
		{
			if (fb.ShowDialog() == DialogResult.OK)
			{
				if (tv.ImageList!=null) tv.ImageList.Dispose();
				tv.Nodes.Clear(); lv.Items.Clear();
				curpath = fb.SelectedPath;
				tstb.Text = curpath;
				DirectoryInfo dii = new DirectoryInfo(curpath);
				TreeNode tn1 = new TreeNode(dii.FullName);
				
				// Get ShellFileInfo
				using (CsShellFile shellFile = new CsShellFile(dii.FullName, SHGFI.ICON|SHGFI.LARGEICON|SHGFI.SYSICONINDEX))
				{
					tn1.ImageIndex = (int)shellFile.IIcon;
					tn1.SelectedImageIndex = (int)shellFile.IIcon;
				}
//				SHFileInfo bs = Shell32.StrIco( dii.FullName, SHGFI.ICON|SHGFI.LARGEICON|SHGFI.SYSICONINDEX );
//				User32.DestroyIcon(bs.hIcon);
				
				foreach (string s in Directory.GetDirectories(curpath))
				{
					DirectoryInfo di  = new DirectoryInfo(s);
					tn1.Nodes.Add(GetDirectoryRootNode(di.FullName));
				}
				tv.Nodes.Add(tn1);
				tv.Sorted=true;
				tv.Sort();
			}
		}
		#endregion
		
		#region Constants
		const SHGFI _tn = SHGFI.TYPENAME;
		const SHGFI _sys_ico = SHGFI.SYSICONINDEX|SHGFI.ICON;
		const SHGFI _sys_ico_sm = _sys_ico|SHGFI.SMALLICON; // |_tn
		const SHGFI _sys_ico_lrg = _sys_ico|SHGFI.LARGEICON; // |_tn
		const SHGFI _sys_lrg = _sys_ico_lrg|_tn;
		#endregion
		
		#region Fields/Properties
		
		FolderBrowserDialog fb = new FolderBrowserDialog();
		
		/// <summary>
		/// Set the Icon size for both the treeview and listview.
		/// </summary>
		public SHGFI ModelIconSize {
			get { return modelIconSize; } set { modelIconSize = value; }
		} protected SHGFI modelIconSize = SHGFI.LARGEICON;
		
		/// <summary>
		/// Monetizes with 'tstb' TextBox.Text with curpath variable.
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string CurPath {
			get { return curpath; } set { curpath = tstb.Text = value; }
		} string curpath;
		
		
		/// <summary>
		/// If set to true, execute windows shell operation on ListView.ItemDoubleClick.
		/// </summary>
		public bool DOPROC {
			get { return doprocedure; } set { doprocedure = value; }
		} bool doprocedure;
		
		public string[] Filter {
			get { return filter; } set { filter = value; }
		} string[] filter;
		
		#endregion

		#region Dictionaries
		
		public Dictionary<string,string> appPointer = new Dictionary<string,string>();
		public Dictionary<string,string> extPointer = new Dictionary<string,string>();
		public Dictionary<string,string> exePointer = new Dictionary<string,string>();
		
		#endregion
		
		#region Public Control Access
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ListView LV { get {return lv;} }
		
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TreeView TV { get {return tv;} }
		
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SplitContainer SCo { get {return sc;} set {sc = value;} }
		#endregion

		public CsShellFileView()
		{
			InitializeComponent();
			WindowsInterop.WindowsTheme.HandleTheme(this.lv);
			WindowsInterop.WindowsTheme.HandleTheme(this.tv);
			fb.Description = "Browse for path";
		}
		
		#region Methods
		
		private DirectoryInfo DParent(string argPath)
		{
			DirectoryInfo DIE = new DirectoryInfo(argPath);
			if (DIE.Parent==null) return null;
			else return DIE.Parent;
		}
		
		#region Cleanup
		void KillImageLists()
		{
			Debug.Print("> Kill ImageLists…");
			try {
				lv.SmallImageList.Dispose();
				lv.LargeImageList.Dispose();
			} catch {
				Debug.Print("Error Disposing ImageList\n");
			}
		}
		void ClearItemsAndGroups(bool clearImageLists)
		{
			lv.Items.Clear();
			lv.Groups.Clear();
			if (clearImageLists) KillImageLists();
		}
		#endregion
		
		public void LoadDirectoryContent(string SelectionPath)
		{
			ClearItemsAndGroups(false);
			ShellManager = new CsShellManager(SelectionPath);
			ShellManager.Initialize(lv, ModelIconSize, _sys_ico_sm);
			// now sort em
			lv.Sort();
			Debug.Print("Populated ListView\n");
		}
		#endregion
		
		#region TreeView Rendering
		void AddTVPath(string path)
		{
			tstb.Text = curpath;
			DirectoryInfo dii = new DirectoryInfo(path);
			TreeNode tn1 = new TreeNode(dii.FullName);
			SHFileInfo bs = Shell32.StrIco(dii.FullName,_sys_ico_lrg);
			tn1.ImageIndex = tn1.SelectedImageIndex = (int)bs.iIcon;
			User32.DestroyIcon(bs.hIcon);
			string [] mantis;
			try { mantis = Directory.GetDirectories(path); foreach (string jj in mantis) { tn1.Nodes.Add(GetDirectoryNode(jj)); } }
			catch (Exception exception) {
				System.Diagnostics.Debug.Print("Error: {0}\n{1}",path,exception.Message);
			}
			tv.Nodes.Add(tn1);
			return;
		}
		
		/* Level One: Calls TDode to add new nodes		*/
		TreeNode GetDirectoryRootNode(string Path)
		{
			TreeNode tn = new TreeNode(new DirectoryInfo(Path).Name);
			SHFileInfo bx = Shell32.StrIco(Path,_sys_lrg);
			tn.ImageIndex = tn.SelectedImageIndex = (int)bx.iIcon;
			User32.DestroyIcon(bx.hIcon);
			bx = new SHFileInfo();
			try
			{
				foreach (string s in Directory.GetDirectories(Path))
				{
					DirectoryInfo di  = new DirectoryInfo(s);
					tn.Nodes.Add(GetDirectoryNode(s));
				}
				
			} catch (Exception exception) {
				Debug.Print("{0}: {1}, File: ‘{2}’",exception, Path, exception.Message);
			}
			return tn;
		}
		/* Handles the majority of nodes into the treeview. */
		TreeNode GetDirectoryNode(string Path)
		{
			TreeNode tn = new TreeNode(new DirectoryInfo(Path).Name);
			SHFileInfo tx = Shell32.StrIco(Path,_sys_ico_lrg);
			tn.ImageIndex =  tn.SelectedImageIndex = (int)tx.iIcon;
			User32.DestroyIcon(tx.hIcon);
			foreach (string s in Directory.GetDirectories(Path))
			{
				DirectoryInfo di  = new DirectoryInfo(s);
				TreeNode ba= new TreeNode(di.Name);
				SHFileInfo ux = Shell32.StrIco(s,_sys_ico_lrg);
				ba.ImageIndex = (int)ux.iIcon;
				ba.SelectedImageIndex = (int)ux.iIcon;
				tn.Nodes.Add(ba);
				User32.DestroyIcon(ux.hIcon);
			}
			return tn;
		}
		#endregion
		
		#region ToolStrip Event Handlers
		void MenuFileExit(object sender, EventArgs e) { Dispose(true); }
		
		void Event_Load_Directory(object sender, EventArgs e) { FolderBrowserAction(); }
		void Event_TreeView_Select_Parent(object sender, EventArgs e)
		{
			try
			{
				if (tv.SelectedNode.Parent != null)
					tv.SelectedNode = (tv.SelectedNode.Parent);
			}
			catch
			{
				System.Diagnostics.Debug.Print("e_ts_up_dir\n");
			}
		}
		#endregion
		
		#region Event Handlers: ListView
		
		void e_lv_dbl_click(object sender, EventArgs e)
		{
			Debug.Print("ListView Item DoubleClick\n");
			try
			{
				CurPath = (tv.SelectedNode.FullPath+"\\"+lv.SelectedItems[0].Text).Replace("\\\\","\\");
				ListViewItem lvi = lv.SelectedItems[0];
				string selt = lvi.Text;
				TreeNode tmp = new TreeNode();
				foreach (TreeNode tj in tv.SelectedNode.Nodes)
				{
					if (tj.Text == selt)
					{
						tmp = tj;
						System.Diagnostics.Debug.Print("felt");
					}
				}
				if(Directory.Exists(CurPath)) { tv.SelectedNode = tmp; Application.DoEvents(); }
				else { if (doprocedure) { doProcess dp = new doProcess(CurPath,true); } }
				foreach (TreeNode tj in tv.SelectedNode.Nodes) { if (tj.Text == selt) { tv.SelectedNode = tj; } }
			} catch (Exception exception) {
				System.Diagnostics.Debug.Print(
					"ListView Error: {0}", exception.Message
				);
			}
		}
		void Event_ListView_Click(object sender, EventArgs e)
		{
			try { CurPath = (tv.SelectedNode.FullPath+"\\"+lv.SelectedItems[0].Text).Replace("\\\\","\\"); }
			catch {
				System.Diagnostics.Debug.Print("Error: e_lv_click\n");
			}
		}
		void Event_ListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			try {
				CurPath =  ((TV.SelectedNode.FullPath+@"\"+LV.SelectedItems[0].Text).Replace(@"\\",@"\"));
			}
			catch
			{
				
			}
		}
		
		#endregion
		
		#region Event Handlers: Tree
		
		// assigned: after-select
		void Event_TreeView_AfterSelect(object s, TreeViewEventArgs e)
		{
			tv.SelectedNode = e.Node;
			CurPath = (e.Node.FullPath).Replace("\\\\","\\"); // 
			Debug.Print("Node Selected: {0}\n",e.Node.FullPath);
			
			// Now Do the ListView Operation.
			LoadDirectoryContent(tstb.Text);
		}
		
		// assigned after-expand
		void Event_TreeView_AfterExpand(object sender, TreeViewEventArgs e)
		{
			Debug.Print("Event: TreeView.AfterExpand");
			foreach (TreeNode tx in e.Node.Nodes)
			{
				if (tx.Nodes.Count <= 0)
				{
					string txt = (tx.FullPath).Replace("\\\\","\\");
					tx.Nodes.Clear();
					DirectoryInfo dn = new DirectoryInfo(txt);
					foreach (DirectoryInfo ss in dn.GetDirectories())
					{
						TreeNode sm = new TreeNode(ss.Name);
						
						SHFileInfo Jah = Shell32.StrIco(ss.FullName,_sys_ico_lrg);
						sm.ImageIndex = (int)Jah.iIcon;
						//sm.key = ;;
						sm.SelectedImageIndex = (int)Jah.iIcon;
						tx.Nodes.Add(sm);
						User32.DestroyIcon(Jah.hIcon);
					}
				}
			}
			
			System.Diagnostics.Debug.Print("Expanded TreeNode");
			
		}
		
		/// <summary>
		/// We remove all child elements from a given tree node,
		/// and rebuild one level of children.
		/// </summary>
		/// <remarks>Assigned: after-collapse</remarks>
		void TreeView_Event_AfterCollapse(object sender, TreeViewEventArgs e)
		{
			// update the current path
			CurPath = (e.Node.FullPath).Replace("\\\\","\\"); // text is updated
			e.Node.Nodes.Clear();
			DirectoryInfo selected_directory = new DirectoryInfo(CurPath);
			
			foreach (DirectoryInfo dir in selected_directory.GetDirectories())
				using (CsShellFile Ja = new CsShellFile(dir.FullName,_sys_ico_lrg))
					Ja.AddChild(e.Node,dir);
		}
		#endregion

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (tv.ImageList!=null) tv.ImageList.Dispose();
			lv.Items.Clear();
			tv.Nodes.Clear();
			foreach (string js in Directory.GetLogicalDrives()) { AddTVPath(js); }
			
			tv.Sorted=true;
			tv.Sort();
			
			User32.SendMessage(
				tv.Handle,
				(uint)TVM.TVM_SETIMAGELIST,
				(uint)TVSIL.NORMAL,
				Shell32.ImgListPtr(curpath,SHGFI.SYSICONINDEX|SHGFI.ICON|ModelIconSize)
			);
		}
		
		#region Design
		
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CsShellFileView));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.colName = new System.Windows.Forms.ColumnHeader();
			this.colType = new System.Windows.Forms.ColumnHeader();
			this.colMisc = new System.Windows.Forms.ColumnHeader();
			this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
			this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
			this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
			this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
			this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
			this.sc = new System.Windows.Forms.SplitContainer();
			this.lv = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.tstb = new System.Windows.Forms.TextBox();
			this.toolStrip2 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.tsFileMenu = new System.Windows.Forms.ToolStripDropDownButton();
			this.loadDrivesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			this.reportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tv = new System.Windows.Forms.TreeView();
			this.sc.Panel1.SuspendLayout();
			this.sc.Panel2.SuspendLayout();
			this.sc.SuspendLayout();
			this.toolStrip2.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(200, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// colName
			// 
			this.colName.Text = "Name";
			this.colName.Width = 179;
			// 
			// colType
			// 
			this.colType.Text = "Type";
			this.colType.Width = 122;
			// 
			// colMisc
			// 
			this.colMisc.Text = "Misc.";
			this.colMisc.Width = 63;
			// 
			// BottomToolStripPanel
			// 
			this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
			this.BottomToolStripPanel.Name = "BottomToolStripPanel";
			this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(0);
			this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
			// 
			// TopToolStripPanel
			// 
			this.TopToolStripPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
			this.TopToolStripPanel.Name = "TopToolStripPanel";
			this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
			// 
			// RightToolStripPanel
			// 
			this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
			this.RightToolStripPanel.Name = "RightToolStripPanel";
			this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
			// 
			// LeftToolStripPanel
			// 
			this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
			this.LeftToolStripPanel.Name = "LeftToolStripPanel";
			this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
			// 
			// ContentPanel
			// 
			this.ContentPanel.Size = new System.Drawing.Size(322, 263);
			// 
			// sc
			// 
			this.sc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sc.Location = new System.Drawing.Point(0, 0);
			this.sc.Name = "sc";
			this.sc.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// sc.Panel1
			// 
			this.sc.Panel1.Controls.Add(this.lv);
			this.sc.Panel1.Controls.Add(this.tstb);
			this.sc.Panel1.Controls.Add(this.toolStrip2);
			// 
			// sc.Panel2
			// 
			this.sc.Panel2.Controls.Add(this.tv);
			this.sc.Size = new System.Drawing.Size(304, 300);
			this.sc.SplitterDistance = 157;
			this.sc.TabIndex = 6;
			// 
			// lv
			// 
			this.lv.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			                         	this.columnHeader1,
			                         	this.columnHeader2});
			this.lv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lv.Location = new System.Drawing.Point(0, 45);
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(304, 112);
			this.lv.TabIndex = 9;
			this.lv.UseCompatibleStateImageBehavior = false;
			this.lv.View = System.Windows.Forms.View.Details;
			this.lv.SelectedIndexChanged += new System.EventHandler(this.Event_ListView_SelectedIndexChanged);
			this.lv.Click += new System.EventHandler(this.Event_ListView_Click);
			this.lv.DoubleClick += new System.EventHandler(this.e_lv_dbl_click);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Name";
			this.columnHeader1.Width = 189;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Type";
			this.columnHeader2.Width = 205;
			// 
			// tstb
			// 
			this.tstb.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.tstb.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
			this.tstb.Dock = System.Windows.Forms.DockStyle.Top;
			this.tstb.Location = new System.Drawing.Point(0, 25);
			this.tstb.Name = "tstb";
			this.tstb.Size = new System.Drawing.Size(304, 20);
			this.tstb.TabIndex = 11;
			// 
			// toolStrip2
			// 
			this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			                               	this.toolStripButton1,
			                               	this.tsFileMenu,
			                               	this.toolStripDropDownButton1});
			this.toolStrip2.Location = new System.Drawing.Point(0, 0);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip2.Size = new System.Drawing.Size(304, 25);
			this.toolStrip2.Stretch = true;
			this.toolStrip2.TabIndex = 10;
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton1.Text = "toolStripButton1";
			this.toolStripButton1.Click += new System.EventHandler(this.Event_TreeView_Select_Parent);
			// 
			// tsFileMenu
			// 
			this.tsFileMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsFileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			                                       	this.loadDrivesToolStripMenuItem,
			                                       	this.loadDirectoryToolStripMenuItem,
			                                       	this.toolStripMenuItem1,
			                                       	this.exitToolStripMenuItem});
			this.tsFileMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsFileMenu.Name = "tsFileMenu";
			this.tsFileMenu.ShowDropDownArrow = false;
			this.tsFileMenu.Size = new System.Drawing.Size(29, 22);
			this.tsFileMenu.Text = "&File";
			// 
			// loadDrivesToolStripMenuItem
			// 
			this.loadDrivesToolStripMenuItem.Enabled = false;
			this.loadDrivesToolStripMenuItem.Name = "loadDrivesToolStripMenuItem";
			this.loadDrivesToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.loadDrivesToolStripMenuItem.Text = "Load Drives";
			this.loadDrivesToolStripMenuItem.Click += new System.EventHandler(this.Event_Load_Directory);
			// 
			// loadDirectoryToolStripMenuItem
			// 
			this.loadDirectoryToolStripMenuItem.Name = "loadDirectoryToolStripMenuItem";
			this.loadDirectoryToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.loadDirectoryToolStripMenuItem.Text = "Load Directory";
			this.loadDirectoryToolStripMenuItem.Click += new System.EventHandler(this.Event_Load_Directory);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(148, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Enabled = false;
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.MenuFileExit);
			// 
			// toolStripDropDownButton1
			// 
			this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			                                                     	this.reportToolStripMenuItem});
			this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			this.toolStripDropDownButton1.ShowDropDownArrow = false;
			this.toolStripDropDownButton1.Size = new System.Drawing.Size(36, 22);
			this.toolStripDropDownButton1.Text = "&View";
			this.toolStripDropDownButton1.Visible = false;
			// 
			// reportToolStripMenuItem
			// 
			this.reportToolStripMenuItem.Name = "reportToolStripMenuItem";
			this.reportToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
			this.reportToolStripMenuItem.Text = "Report";
			// 
			// tv
			// 
			this.tv.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tv.FullRowSelect = true;
			this.tv.HideSelection = false;
			this.tv.Indent = 15;
			this.tv.Location = new System.Drawing.Point(0, 0);
			this.tv.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tv.Name = "tv";
			this.tv.ShowLines = false;
			this.tv.ShowNodeToolTips = true;
			this.tv.Size = new System.Drawing.Size(304, 139);
			this.tv.TabIndex = 2;
			this.tv.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_Event_AfterCollapse);
			this.tv.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.Event_TreeView_AfterExpand);
			this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.Event_TreeView_AfterSelect);
			// 
			// xplocontrol
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.Controls.Add(this.sc);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "xplocontrol";
			this.Size = new System.Drawing.Size(304, 300);
			this.sc.Panel1.ResumeLayout(false);
			this.sc.Panel1.PerformLayout();
			this.sc.Panel2.ResumeLayout(false);
			this.sc.ResumeLayout(false);
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ListView lv;
		private System.Windows.Forms.TreeView tv;
		private System.Windows.Forms.SplitContainer sc;
		private System.Windows.Forms.ToolStripContentPanel ContentPanel;
		private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
		private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
		private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
		private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.ToolStripMenuItem reportToolStripMenuItem;
		private System.Windows.Forms.ToolStripDropDownButton tsFileMenu;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadDirectoryToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem loadDrivesToolStripMenuItem;
		private System.Windows.Forms.ToolStrip toolStrip2;
		private System.Windows.Forms.ColumnHeader colName;
		private System.Windows.Forms.ColumnHeader colType;
		private System.Windows.Forms.ColumnHeader colMisc;
		private System.Windows.Forms.TextBox tstb;
		#endregion
		
	}
}
