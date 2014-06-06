/*
TFOOO - With SharpDevelop2
 */
using System;
using System.Diagnostics;
using System.IO;

namespace Windows.CommonControls
{
	#region doProcess
	/// <summary>
	/// Simplifies 'Process' functions or some System.IO routines.
	/// </summary>
	class doProcess
		{
			private string output;
			public string Output
			{
				get { return output; }
				set { output = value; }
			}
			public doProcess(string args, string pth, string wrkPth, bool useShell, bool noWin)
			{
				string outs;
			//	MessageBox.Show(Path.GetFullPath(pth),Path.GetFullPath(max));
				if (Path.HasExtension(pth))
				{
					
					Process proc = new Process();
					proc.StartInfo.UseShellExecute = useShell;
					proc.StartInfo.CreateNoWindow = noWin;
					try
					{
					proc.StartInfo.FileName = Path.GetFullPath(wrkPth+"\\"+pth);
					proc.StartInfo.RedirectStandardOutput = true;
					}
					catch(System.ArgumentException)
					{
				//		this.Text = bar + ":" + max;
						output =  "it didn't work";
					}
					proc.StartInfo.WorkingDirectory = Path.GetFullPath(wrkPth);
				//	MessageBox.Show(proc.StartInfo.WorkingDirectory);
					proc.StartInfo.Arguments = args;
				//	proc.StartInfo.LoadUserProfile = true;
					try 
					{
					proc.Start();
					}
					catch (System.ComponentModel.Win32Exception)
					{
						output =  "dumb-ass";
					}
					outs = "";
					outs += (proc.StandardOutput.ReadToEnd());
					proc.WaitForExit();
					output = outs;
					return;
				}
				output = "problems?  "+pth;
			}
			
			private Process proc = new Process();

			public doProcess(string app, string pth, bool useShell)
			{
				if (Path.HasExtension(pth))
				{
					
					proc = new Process();
					proc.StartInfo.UseShellExecute = useShell;
					proc.StartInfo.FileName = Path.GetFullPath(app);
					proc.StartInfo.Arguments = Path.GetFullPath(pth);
					proc.StartInfo.WorkingDirectory = Path.GetFullPath(pth);
					try 
					{
					proc.Start();
					}
					catch (System.ComponentModel.Win32Exception)
					{
						
					}
				}
			}

			public doProcess(string pth, bool useShell)
			{
			//	if (Path.HasExtension(pth))
			//	{
					
					proc = new Process();
					proc.StartInfo.UseShellExecute = useShell;
					proc.StartInfo.FileName = Path.GetFullPath(pth);
					proc.StartInfo.WorkingDirectory = "";
					try 
					{
					proc.Start();
					}
					catch (System.ComponentModel.Win32Exception)
					{
						
					}
			//	}
			}

			public doProcess(string pth, bool useShell, string workPath)
			{
			//	if (Path.HasExtension(pth))
			//	{
					
					proc = new Process();
					proc.StartInfo.UseShellExecute = useShell;
					proc.StartInfo.FileName = Path.GetFullPath(pth);
					proc.StartInfo.WorkingDirectory = workPath;
					try 
					{
					proc.Start();
					}
					catch (System.ComponentModel.Win32Exception)
					{
						
					}
			//	}
			}

	}
	#endregion
}
