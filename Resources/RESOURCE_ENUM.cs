﻿using System;

namespace System
{
	partial class Kernel : WindowsInterop
	{
		//./////////////////////////////////////////////////////////////////////////
		public class ResourceEnum
		{
			public const int RESOURCE_ENUM_LN = 0x0001;
			public const int RESOURCE_ENUM_MUI = 0x0002;
			public const int RESOURCE_ENUM_MUI_SYSTEM = 0x0004;
			public const int RESOURCE_ENUM_VALIDATE = 0x0008;
			public const int RESOURCE_UPDATE_LN = 0x0010;
			public const int RESOURCE_UPDATE_MUI = 0x0020;
		}
		[Flags] public enum RESOURCE_ENUM : int
		{
			LN = ResourceEnum.RESOURCE_ENUM_LN,
			MUI = ResourceEnum.RESOURCE_ENUM_LN,
			MUI_SYSTEM = ResourceEnum.RESOURCE_ENUM_LN,
			VALIDATE = ResourceEnum.RESOURCE_ENUM_LN,
			UPDATE_LN = ResourceEnum.RESOURCE_ENUM_LN,
			UPDATE_MUI = ResourceEnum.RESOURCE_ENUM_LN,
		}

	}
}
