/**
 * 
 * User: tfw
 * Date: 1/2/2009
 * Time: 7:20 AM
 * 
**/
using System;

namespace System.Kernel
{
	partial class Kernel : WindowsInterop
	{
		
	/**
	 * SString was re-named from String as to not conflict
	 * with the default String type.
	**/
	public struct SString
	{
		public short	wLength; 
		public short	wValueLength; 
		public short	wType; 
		public char[]	szKey; //WCHAR
		public short[]	Padding;
		public short[]	Value;
	}
	}
}
