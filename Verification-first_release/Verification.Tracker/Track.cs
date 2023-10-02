using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verification.Tracker
{
    public class Track : ITracker
    {

        private static Track instance;
        private static Logger logger;

        public Track()
        {

        }

        public static Track GetInstance()
        {
            if (instance == null)
                instance = new Track();
            return instance;
        }

        private Logger GetLogger(string thelogger)
        {
            if (Track.logger == null)
                Track.logger = LogManager.GetLogger(thelogger);
            return Track.logger;
        }
        public void Trace(Stopwatch stopwatch, string message, string arg = null)
        {
            if (arg == null)
                GetLogger("logRule").Trace(stopwatch.ElapsedMilliseconds.ToString().PadLeft(4, '0') + "ms|" + message);
            else
                GetLogger("logRule").Trace(stopwatch.ElapsedMilliseconds.ToString().PadLeft(4, '0') + "ms|" + message, arg);
        }
        public void Info(Stopwatch stopwatch, string message, string arg = null)
        {
            if (arg == null)
                GetLogger("logRule").Info(stopwatch.ElapsedMilliseconds.ToString().PadLeft(4, '0') + "ms|" + message);
            else
                GetLogger("logRule").Info(stopwatch.ElapsedMilliseconds.ToString().PadLeft(4, '0') + "ms|" + message, arg);
        }

        public void Debug(Stopwatch stopwatch,string message, string arg = null)
        {
            stopwatch.Stop();
            
            if (arg == null)
                GetLogger("logRule").Debug(stopwatch.ElapsedMilliseconds.ToString().PadLeft(4,'0')+"ms|"+message);
            else
                GetLogger("logRule").Debug(stopwatch.ElapsedMilliseconds.ToString().PadLeft(4, '0') + "ms|" + message, arg);
        }
        public void Warning(string message, string arg = null)
        {
            if (arg == null)
                GetLogger("logRule").Warn(message);
            else
                GetLogger("logRule").Warn(message, arg);
        }
        public void Error(string message, string arg = null)
        {
            if (arg == null)
                GetLogger("logRule").Error(message);
            else
                GetLogger("logRule").Error(message, arg);
        }



        //public string AdvanceTrack(string Prj, string cls, string fun, int line = 0)
        //{
        //    return $"{DateTime.Now.ToLocalTime().ToString()} # Project: {Prj} -> Class: {cls} -> Function {fun} -> LineNo:{line} ";
        //}

        //public string AppTrack(string Prj, string cls, string fun, int line = 0)
        //{
        //    return $"{DateTime.Now.ToLocalTime().ToString()} # Project: {Prj} -> Class: {cls} -> Function {fun} -> LineNo:{line} ";
        //}

        //public string ErrorTrack(Exception exp, string Prj, string cls, string fun, int line = 0)
        //{
        //    return $"{DateTime.Now.ToLocalTime().ToString()} # Project: {Prj} -> Class: {cls} -> Function {fun} ~ LineNo:{line} ##Exception:-{exp.Message}";
        //}

    }
}
