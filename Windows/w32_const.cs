﻿/* oOo * 11/20/2007 : 4:41 PM */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System
{
	partial class WindowsInterop {
		public class w32_const
		{
			///////////////////////////////////////////////////////////////////
			public const int HWND_BROADCAST = 0xffff;
			public const int HWND_TOP = 0;
			public const int HWND_BOTTOM = 1;
			public const int HWND_TOPMOST = -1;
			public const int HWND_NOTOPMOST = -2;
			///////////////////////////////////////////////////////////////////
			public const int WM_USER = 0x0400;
			///////////////////////////////////////////////////////////////////
			public const int MIIM_STATE			=0x00000001;
			public const int MIIM_ID			=0x00000002;
			public const int MIIM_SUBMENU	 	=0x00000004;
			public const int MIIM_CHECKMARKS	=0x00000008;
			public const int MIIM_TYPE			=0x00000010;
			public const int MIIM_DATA			=0x00000020;
			//		#endif /* WINVER >= 0x0400 */
			//		#if(WINVER >= 0x0500)
			public const int MIIM_STRING	  	=0x00000040;
			public const int MIIM_BITMAP	  	=0x00000080;
			public const int MIIM_FTYPE	   	=0x00000100;
			///////////////////////////////////////////////////////////////////
			/*		Menu flags for Add/Check/EnableMenuItem()		*/
			public const int MF_INSERT		   =0x00000000;
			public const int MF_CHANGE		   =0x00000080;
			public const int MF_APPEND		   =0x00000100;
			public const int MF_DELETE		   =0x00000200;
			public const int MF_REMOVE		   =0x00001000;
			
			public const int MF_BYCOMMAND		=0x00000000;
			public const int MF_BYPOSITION	   =0x00000400;
			
			public const int MF_SEPARATOR		=0x00000800;
			
			public const int MF_ENABLED		  =0x00000000;
			public const int MF_GRAYED		   =0x00000001;
			public const int MF_DISABLED		 =0x00000002;
			
			public const int MF_UNCHECKED		=0x00000000;
			public const int MF_CHECKED		  =0x00000008;
			public const int MF_USECHECKBITMAPS  =0x00000200;
			
			public const int MF_STRING		   =0x00000000;
			public const int MF_BITMAP		   =0x00000004;
			public const int MF_OWNERDRAW		=0x00000100;
			
			public const int MF_POPUP			=0x00000010;
			public const int MF_MENUBARBREAK	 =0x00000020;
			public const int MF_MENUBREAK		=0x00000040;
			
			public const int MF_UNHILITE		 =0x00000000;
			public const int MF_HILITE		   =0x00000080;
			//		#if(WINVER >= 0x0400)
			public const int MF_DEFAULT		  =0x00001000;
			//		#endif /* WINVER >= 0x0400 */
			public const int MF_SYSMENU		  =0x00002000;
			public const int MF_HELP			 =0x00004000;
			//		#if(WINVER >= 0x0400)
			public const int MF_RIGHTJUSTIFY	 =0x00004000;
			//		#endif /* WINVER >= 0x0400 */
			public const int MF_MOUSESELECT	  =0x00008000;
			//		#if(WINVER >= 0x0400)
			public const int MF_END			  =0x00000080;  /* Obsolete -- only used by old RES files */
			///////////////////////////////////////////////////////////////////
			public const int MFT_STRING		  =MF_STRING;
			public const int MFT_BITMAP		  =MF_BITMAP;
			public const int MFT_MENUBARBREAK	=MF_MENUBARBREAK;
			public const int MFT_MENUBREAK	   =MF_MENUBREAK;
			public const int MFT_OWNERDRAW	   =MF_OWNERDRAW;
			public const int MFT_RADIOCHECK	  =0x00000200;
			public const int MFT_SEPARATOR	   =MF_SEPARATOR;
			public const int MFT_RIGHTORDER	  =0x00002000;
			public const int MFT_RIGHTJUSTIFY	=MF_RIGHTJUSTIFY;
			///////////////////////////////////////////////////////////////////
			public const int TB_SAVERESTOREA         =(WM_USER + 26);
			public const int TB_SAVERESTOREW         =(WM_USER + 76);
			public const int TB_CUSTOMIZE            =(WM_USER + 27);
			public const int TB_ADDSTRINGA           =(WM_USER + 28);
			public const int TB_ADDSTRINGW           =(WM_USER + 77);
			public const int TB_GETITEMRECT          =(WM_USER + 29);
			public const int TB_BUTTONSTRUCTSIZE     =(WM_USER + 30);
			public const int TB_SETBUTTONSIZE        =(WM_USER + 31);
			public const int TB_SETBITMAPSIZE        =(WM_USER + 32);
			public const int TB_AUTOSIZE             =(WM_USER + 33);
			public const int TB_GETTOOLTIPS          =(WM_USER + 35);
			public const int TB_SETTOOLTIPS          =(WM_USER + 36);
			public const int TB_SETPARENT            =(WM_USER + 37);
			public const int TB_SETROWS              =(WM_USER + 39);
			public const int TB_GETROWS              =(WM_USER + 40);
			public const int TB_SETCMDID             =(WM_USER + 42);
			public const int TB_CHANGEBITMAP         =(WM_USER + 43);
			public const int TB_GETBITMAP            =(WM_USER + 44);
			public const int TB_GETBUTTONTEXTA       =(WM_USER + 45);
			public const int TB_GETBUTTONTEXTW       =(WM_USER + 75);
			public const int TB_REPLACEBITMAP        =(WM_USER + 46);
			//#if (_WIN32_IE >= 0x0300)=;
			public const int TB_SETINDENT            =(WM_USER + 47);
			public const int TB_SETIMAGELIST         =(WM_USER + 48);
			public const int TB_GETIMAGELIST         =(WM_USER + 49);
			public const int TB_LOADIMAGES           =(WM_USER + 50);
			public const int TB_GETRECT              =(WM_USER + 51); // wParam is the Cmd instead of index
			public const int TB_SETHOTIMAGELIST      =(WM_USER + 52);
			public const int TB_GETHOTIMAGELIST      =(WM_USER + 53);
			public const int TB_SETDISABLEDIMAGELIST =(WM_USER + 54);
			public const int TB_GETDISABLEDIMAGELIST =(WM_USER + 55);
			public const int TB_SETSTYLE             =(WM_USER + 56);
			public const int TB_GETSTYLE             =(WM_USER + 57);
			public const int TB_GETBUTTONSIZE        =(WM_USER + 58);
			public const int TB_SETBUTTONWIDTH       =(WM_USER + 59);
			public const int TB_SETMAXTEXTROWS       =(WM_USER + 60);
			public const int TB_GETTEXTROWS          =(WM_USER + 61);
		}
	}
}