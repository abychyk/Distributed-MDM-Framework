using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DMDMS.Data
{
    public static class XmlHelper
    {
        public static string ParseSourceConnectionString(string path)
        {
            var doc = XDocument.Load(path);
            var q = (from b in doc.Descendants("SourceDB")
                    select b).FirstOrDefault();

            var sourceIP = q.Element("SourceIp")?.Value.ToString();
            var sourceDBName = q.Element("SourceDBName")?.Value.ToString();
            var sourceUser = q.Element("SourceUser")?.Value.ToString();
            var sourcePassword = q.Element("SourcePassword")?.Value.ToString();
            var sourceIS = q.Element("SourceIS")?.Value.ToString();
            string connectionString = default(string);
            if (!String.IsNullOrEmpty(sourceUser) && !String.IsNullOrEmpty(sourcePassword))
            {
                connectionString = String.Format("Data Source={0};Initial Catalog={1};User id={2};Password={3};",
                                                     sourceIP,
                                                     sourceDBName,
                                                     sourceUser,
                                                     sourcePassword);
            }
            else if (!String.IsNullOrEmpty(sourceIS))
            {
                connectionString = String.Format("Data Source={0};Initial Catalog={1};Integrated Security={2};",
                                                     sourceIP,
                                                     sourceDBName,
                                                     sourceIS);
            }
            return connectionString;
        }

        public static List<string> ParseTargetConnectionString(string path)
        {
            var doc = XDocument.Load(path);
            var q = (from d in doc.Descendants("TargetDB")
                    select d).ToList();

            var targetConnectionStringList = new List<string>();
            foreach (var targetDb in q.Elements("TargetNode"))
            {
                var targetIP = targetDb.Element("TargetIp")?.Value.ToString();
                var targetDBName = targetDb.Element("TargetDBName")?.Value.ToString();
                var targetUser = targetDb.Element("TargetUser")?.Value.ToString();
                var targetPassword = targetDb.Element("TargetPassword")?.Value.ToString();
                var targetIS = targetDb.Element("TargetIS")?.Value.ToString();

                var connectionString = default(string);

                if (!String.IsNullOrEmpty(targetUser) && !String.IsNullOrEmpty(targetPassword))
                {
                    connectionString = String.Format("Data Source={0};Initial Catalog={1};User id={2};Password={3};",
                                                    targetIP,
                                                    targetDBName,
                                                    targetUser,
                                                    targetPassword);
                }
                else if (!String.IsNullOrEmpty(targetIS))
                {
                    connectionString = String.Format("Data Source={0};Initial Catalog={1};Integrated Security={2};",
                                                    targetIP,
                                                    targetDBName,
                                                    targetIS);
                }
                targetConnectionStringList.Add(connectionString);
            }
            return targetConnectionStringList;
        }
    }
}
