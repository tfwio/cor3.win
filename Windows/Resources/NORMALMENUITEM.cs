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
	public struct NORMALMENUITEM {
		public enum PTYP : int { MFR_END,MFR_POPUP }
		public WORD resInfo; 
		/// <summary>Specifies a null-terminated Unicode string that contains the text for this menu item. There is no fixed limit on the size of this string.</summary>
		public /*szOrOrd*/ string menuText; 
	} 
}
