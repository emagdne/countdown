using System;

namespace CountDown.FunctionalTests.Data.Exceptions
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 5/1/14</para>
    /// </summary>
    public class MissingDependencyException : Exception
    {
        public MissingDependencyException() { }
        public MissingDependencyException(string message) : base(message) { }
    }
}
