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
	public struct NEWHEADER {
		public enum RESTYPE : int { RES_ICON,RES_CURSOR }
		public WORD Reserved;
		public WORD ResType; 
		public WORD ResCount; 
	} 
}
