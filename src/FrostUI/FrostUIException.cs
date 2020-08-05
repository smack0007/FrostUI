using System;

namespace FrostUI
{
    public class FrostUIException : Exception
    {
        public FrostUIException(string message)
            : base(message)
        {
        }
    }
}
