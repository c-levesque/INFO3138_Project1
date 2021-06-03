/**
	 * Class Name:  Error.cs
	 * Purpose:		Static class for writing error messages.
	 * Coder:		Chris Levesque
	 * Date:		2021-05-30	
*/

using System;

namespace GradeTracker.HelperClasses
{
    public static class Error
    {
        public static void Message(string msg)
        {
            /*Method Name:  Message
            *Purpose:       Wrapper for Console.Write, adds ERROR: before the message
            *Accepts:       string (error message)
            *Returns:       void
            */
            Console.Write($"ERROR: {msg}");
        }
    }
}
