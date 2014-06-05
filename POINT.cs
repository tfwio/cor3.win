using System;
using System.Runtime.InteropServices;

namespace System.Shell32
{
	// Defines the x- and y-coordinates of a point
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public struct POINT
	{
	    public POINT(int x, int y)
	    {
	        this.x = x;
	        this.y = y;
	    }
	
	    public int x;
	    public int y;
	}
}
