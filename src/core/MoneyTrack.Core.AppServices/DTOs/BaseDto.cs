using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Core.AppServices.DTOs
{
    public abstract class BaseDto
    {
        public abstract string GetErrorString();
        public virtual string GetErrorIncludeInner()
        {
            return GetErrorString();
        }
    }
}
