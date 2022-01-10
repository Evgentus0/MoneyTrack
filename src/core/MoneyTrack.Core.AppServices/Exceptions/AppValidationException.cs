using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyTrack.Core.AppServices.Exceptions
{
    public class AppValidationException: Exception
    {

        public AppValidationException(string message): base(message)
        { }
    }
}
