﻿/**
 * 
 * User: tfw
 * Date: 1/2/2009
 * Time: 7:20 AM
 * 
**/
using System;
using Cor3;

namespace System.Kernel
{

	public struct RESDIR
	{
		public RESINFO ResInfo;
		public ushort Planes; 
		public ushort BitCount; 
		public uint BytesInRes; 
		public ushort IconCursorId; 
	} 
}
