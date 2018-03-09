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


    [Cmdlet(VerbsCommon.Format, "DacpacAsSql")]
    public class DacpacToSqlCmdlet : Cmdlet
    {
       


        public DacQueryScopes QueryScopes
        {
            get;
            set;
        }

        // Directory for storing the output sql files
        [Parameter(Mandatory = true)]
        public string OutputDirectory { get; set; }

        // The dacpac file being used
        [Parameter(Mandatory = true)]
        public string InputPath { get; set; }

        public void Test()
        {
            ProcessRecord();

        }

        protected override void ProcessRecord()
        {
            string directory = OutputDirectory;

            // get the data frm the dacpac and iterate through each file
            using (TSqlModel modelFromDacpac = new TSqlModel(InputPath))
            {
                //list of objects not to be added to source control
                string[] objectsNotIncluded = { "RoleMembership", "Synonym", "User", "Login" };

                QueryScopes = DacQueryScopes.UserDefined;
                IEnumerable<TSqlObject> allObjects = modelFromDacpac.GetObjects(QueryScopes);
                foreach (TSqlObject tsqlObject in allObjects)
                {
                    string script;

                    // if this is a tsql script, save it to the directory according to its type
                    if (tsqlObject.TryGetScript(out script) && !objectsNotIncluded.Contains(tsqlObject.ObjectType.Name))
                    {
                        Console.WriteLine("Adding " + directory + tsqlObject.ObjectType.Name +"/" + tsqlObject.Name.ToString());
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