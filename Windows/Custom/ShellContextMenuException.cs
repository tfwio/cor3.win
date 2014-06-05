using System;

namespace System.Shell32
{
	public class ShellContextMenuException : Exception
	{
	    /// <summary>Default contructor</summary>
	    public ShellContextMenuException()
	    {
	    }
	
	    /// <summary>Constructor with message</summary>
	    /// <param name="message">Message</param>
	    public ShellContextMenuException(string message)
	        : base(message)
	    {
	    }
	}
}
