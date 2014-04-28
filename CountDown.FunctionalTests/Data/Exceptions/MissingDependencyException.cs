using System;

namespace CountDown.FunctionalTests.Data.Exceptions
{
    public class MissingDependencyException : Exception
    {
        public MissingDependencyException() { }
        public MissingDependencyException(string message) : base(message) { }
    }
}
