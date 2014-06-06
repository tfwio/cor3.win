/*
TFOOO - With SharpDevelop2
 */
#if FALSE
using System;
using System.Runtime.InteropServices;

/**
 * 
 * The structures defined here are perhaps useful with a depreceated
 * grouping method.
 * 
 * ‘useful’ in that it might be used in an old app of mine.
 * 
 **/

namespace System.CommonControls.Interop
{
	[StructLayout(LayoutKind.Sequential)]
	public struct sifter
	{
		public string Type;
		public string Comment;
	}
	public struct typex
	{
		public string Name;
		public Dictionary<string,sifter> Types;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct KntGroup
	{
		public string Title;
		public string Path;
		public string Type;
		public string Filter;
		public bool IncSubDirs;
		public string FileTemplate;
		public string FileOut;
	}
}

#endif