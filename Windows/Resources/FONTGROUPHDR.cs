﻿/**
 * 
 * User: tfw
 * Date: 1/2/2009
 * Time: 7:20 AM
 * 
**/
using System;

namespace System.Kernel
{
	public struct FONTGROUPHDR { 
		public WORD NumberOfFonts; 
		public DIRENTRY[/* 1 */]  DE;
	}
}
