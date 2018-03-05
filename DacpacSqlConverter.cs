using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Management.Automation;
using System.Web.Http.Filters;
using Microsoft.SqlServer.Dac.Model;
using Microsoft.SqlServer.Dac;


namespace DacpacSqlConverter
{

    public class ModelFilterer
    {

        public DacQueryScopes QueryScopes
        {
            get;
            set;
        }

        private IList<IFilter> _filters;

        public ModelFilterer(params IFilter[] filters)
            : this((IList<IFilter>)filters)
        {
        }

        public ModelFilterer(IList<IFilter> filters)
        {
            if (filters == null
                || filters.Count == 0)
            {
                throw new ArgumentException("At least one filter must be specified", "filters");
            }
            _filters = new List<IFilter>(filters);
            QueryScopes = DacQueryScopes.UserDefined;
        }
    }

    [Cmdlet(VerbsCommon.Format, "DacpacAsSql")]
    public class DacpacToSqlCmdlet
    {
        // Declare the parameters for the cmdlet.
        [Parameter(Mandatory = true)]
        public string InputPath { get; set; }


        public DacQueryScopes QueryScopes
        {
            get;
            set;
        }


        // Declare the parameters for the cmdlet.
        [Parameter(Mandatory = true)]
        public string OutputPath { get; set; }

        public void Test()
        {
            ProcessRecord();

        }



        protected void ProcessRecord()
        {
            string directory = "C:/dacpac/";
            using (TSqlModel modelFromDacpac = new TSqlModel(@"C:\dacpac\staging.dacpac"))
            {
                QueryScopes = DacQueryScopes.UserDefined;
                IEnumerable<TSqlObject> allObjects = modelFromDacpac.GetObjects(QueryScopes);
                foreach (TSqlObject tsqlObject in allObjects)
                {
                    string script;

                    if (tsqlObject.TryGetScript(out script))
                    {
                        Console.WriteLine("Adding " + directory + tsqlObject.ObjectType.Name);
                        saveToDirectory(directory + tsqlObject.ObjectType.Name, tsqlObject.Name.ToString(), script);
                    }

                }

            }
        }



        private void saveToDirectory(string pathName, string objName, string script)
        {
            Directory.CreateDirectory(pathName); // If the directory already exists, this method does nothing.

            using (StreamWriter sw = new StreamWriter(string.Format("{0}/{1}.sql", pathName, objName)))
            {

                sw.Write(script);
                sw.Flush();

            }


        }



    }

}