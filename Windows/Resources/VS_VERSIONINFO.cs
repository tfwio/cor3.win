/**
 * 
 * User: tfw
 * Date: 1/2/2009
 * Time: 7:20 AM
 * 
**/
using System;
using System.Runtime.InteropServices;

namespace System.Kernel
{
	public struct VS_VERSIONINFO
	{
		public short	wLength; 
		public short	wValueLength; 
		public short	wType; 
		public char[]	szKey; //wchar 
		public short[]	Padding1; 
		public VS_FIXEDFILEINFO	Value; 
		public short[]	Padding2;
		public short[]	Children;
	}
}
