using System;
using System.Runtime.Serialization;

namespace SOC.Scanning.Exceptions
{
    [Serializable]
    public class InvalidInputException : Exception
    {
        public InvalidInputException() { }

        public InvalidInputException(string message) : base(message) { }
    }
}