using Microsoft.Extensions.Configuration;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Verification.Tracker
{
    public class ConfigureApi
    {
        private readonly IConfiguration configuration;
        //private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public ConfigureApi(IConfiguration configuration)
        {
            this.configuration = configuration;
            
            var Configure = configuration.GetSection("ConfigureApi").Value;
            if (File.Exists(Configure))
            {
                ConfigApi = new XmlTextReader(Configure);
                while (ConfigApi.Read())
                { 

                }
            }
        }

        public XmlTextReader ConfigApi { get; private set; }

        public static void WriteExceptionLog(Logger logger,Exception exp, string Prj, string cls, string fun, int line = 0 )
        {
            logger.Error( $"{DateTime.Now.ToLocalTime().ToString()} # Project: {Prj} -> Class: {cls} -> Function {fun} ~ LineNo:{line} ##Exception:-{exp.Message}");
        }

    }
}
