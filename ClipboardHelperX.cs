/* oOo * 11/20/2007 : 4:41 PM */
using System;

namespace System
{
	public partial class Windows
	{
		partial class User
		{
			[Obsolete] public class ClipboardHelperX : ObjectManager<INamedObjectEditor>
			{
				void eNum(IntPtr lp, Message m)
				{
					efx.Global.cstat(ConsoleColor.Yellow,"CBHelper");
				}

				ClipboardDummyNative dummy;
				public override void AddEvents()
				{
					base.AddEvents();
					dummy = new ClipboardDummyNative(Client.Handle);
					dummy.CBMsg += eNum;
				}

				public ClipboardHelperX(INamedObjectEditor ctl) : base(ctl)
				{
					
				}
			}

		}
	}
}
