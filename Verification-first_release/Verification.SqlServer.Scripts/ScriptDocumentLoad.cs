using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Verification.Data.AdoDotNet;

namespace Verification.SqlServer.Scripts
{
    public class ScriptDocumentLoad
    {
        VerificationQueryHandler handler;
        string DatabaseScript;
        ScriptRun scriptRun;
        List<ScriptDetails> script;

        public ScriptDocumentLoad(IConfiguration configuration)
        {
            handler = new VerificationQueryHandler(configuration.GetConnectionString("EfCore"));
            DatabaseScript = configuration.GetSection("DatabaseScript").Value.ToString().ToUpper();
            scriptRun = new ScriptRun(handler);
            script = new List<ScriptDetails>();
        }

        public void LoadXmlDoc(string FilePath)
        {
            if (File.Exists(FilePath))
            {
                LoadedScript();
                string ReadAllText = File.ReadAllText(FilePath);
                XmlDocument Xdoc = new XmlDocument();
                Xdoc.LoadXml(ReadAllText);
                XmlNodeList XNlist = Xdoc.GetElementsByTagName("dataBase");
                foreach (XmlNode RootNode in XNlist)
                {
                    foreach (XmlNode ParentNode in RootNode.ChildNodes)
                    {
                        if (ParentNode.Name.ToUpper().Equals(DatabaseScript))
                        {
                            foreach (XmlNode ChildNode in ParentNode.ChildNodes)
                            {
                                foreach (XmlNode SubChildNode in ChildNode.ChildNodes)
                                {
                                    
                                    if (script.Where(x=>x.Ver== ChildNode.Attributes["ver"].Value.ToString() && x.Syn== ChildNode.Attributes["sync"].Value.ToString() && x.Qry== SubChildNode.Attributes["ver"].Value.ToString()).Count()==0 )
                                    {
                                        ReadAllText = SubChildNode.InnerText.Trim();
                                        if (ReadAllText.Length > 0)
                                        {
                                            var Result = scriptRun.Run(ReadAllText);
                                            if (Result.Item1)
                                            {
                                                Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
                                                param.Add("@VerActive", ChildNode.Attributes["ver"].Value.ToString());
                                                param.Add("@SyncScript", ChildNode.Attributes["sync"].Value.ToString());
                                                param.Add("@QryScript", SubChildNode.Attributes["ver"].Value.ToString());
                                                scriptRun.Run("INSERT INTO  SqlVer (VerActive ,SyncScript,QryScript) VALUES(@VerActive,@SyncScript,@QryScript)", param);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        void LoadedScript()
        {
            var result = scriptRun.Read("Select * from SqlVer;");
            if (result.Item1 && result.Item2 != null)
            {
                foreach (DataRow row in ((DataTable)result.Item2).Rows)
                {
                    script.Add(new ScriptDetails { Ver=row["VerActive"].ToString(),Syn= row["SyncScript"].ToString(), Qry= row["QryScript"].ToString() });
                }
            }
        }

        
    }
}
