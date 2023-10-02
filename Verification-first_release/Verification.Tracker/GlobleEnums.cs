using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verification.Tracker
{
    public class GlobleEnums
    {
        public enum ResponseStatus
        {
            Error = -2,
            Fail = -1,
            NotFound = 0,
            Success = 1,
            Exists=2,
        }
    }
}
