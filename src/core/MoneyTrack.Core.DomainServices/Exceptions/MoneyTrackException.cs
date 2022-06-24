using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyTrack.Core.DomainServices.Exceptions
{

    [Serializable]
    public class MoneyTrackException : Exception
    {
        public MoneyTrackException() { }
        public MoneyTrackException(string message) : base(message) { }
        public MoneyTrackException(string message, Exception inner) : base(message, inner) { }
        protected MoneyTrackException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
