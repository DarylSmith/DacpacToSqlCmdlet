using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DacpacSqlConverter
{
    public class Utilities
    {
        public string GetDatabaseNameFromEnvironment(string env, string db)
        {
            switch (env.ToLower())
            {
                case "dev":
                    return string.Format("Lifespeak_{0}_Dev", db);

                case "staging":
                    return string.Format("Lifespeak_{0}_Staging", db);

                default:
                    return string.Format("Lifespeak_{0}", db);




            }
        }

    }
}
