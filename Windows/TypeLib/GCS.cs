﻿using System;
using System.Runtime.InteropServices;

namespace System.Shell32
{
	// Flags specifying the information to return when calling IContextMenu::GetCommandString
	[Flags]
	public enum GCS : uint
	{
	    VERBA = 0,
	    HELPTEXTA = 1,
	    VALIDATEA = 2,
	    VERBW = 4,
	    HELPTEXTW = 5,
	    VALIDATEW = 6
	}
}
