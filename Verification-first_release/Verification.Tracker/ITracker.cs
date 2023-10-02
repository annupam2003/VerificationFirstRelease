using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verification.Tracker
{
    public interface ITracker
    {
        void Trace(Stopwatch stopwatch, string message, string arg = null);
        void Debug(Stopwatch stopwatch, string message, string arg = null);
        void Info(Stopwatch stopwatch, string message, string arg = null);
        void Warning(string message, string arg = null);
        void Error(string message, string arg = null);
    }
}
