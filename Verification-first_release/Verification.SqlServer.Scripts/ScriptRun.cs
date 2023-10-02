using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verification.Data.AdoDotNet;

namespace Verification.SqlServer.Scripts
{
    class ScriptRun
    {
        private readonly VerificationQueryHandler handler;

        public ScriptRun(VerificationQueryHandler handler)
        {
            this.handler = handler;
        }

        public Tuple<bool, dynamic> Read(string Query, Dictionary<string, dynamic> param = null) => handler.Reader(Query, param);
        public Tuple<bool, dynamic> Run(string Query,Dictionary<string, dynamic> param=null) => handler.NonQuery(Query, param);



    }
}
