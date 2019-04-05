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
    [Cmdlet(VerbsCommon.Format, "SqlAsDacpac")]
    public class SqlDacpacConverter :Cmdlet
    {

        // Declare the parameters for the cmdlet.
        [Parameter(Mandatory = true)]
        public string OutputPath { get; set; }

        // Declare the parameters for the cmdlet.
        [Parameter(Mandatory = true)]
        public string FileDirectory { get; set; }

        protected override void ProcessRecord()
        {
            string script = string.Empty;
            try
            {

                // create an instance of a tsqmodel to store the sql records
                using (TSqlModel model = new TSqlModel(SqlServerVersion.Sql130, new TSqlModelOptions()))
                {
                    // iterate through each file and add the sql script to the model
                    foreach (string file in System.IO.Directory.GetFiles(FileDirectory, "*.sql", SearchOption.AllDirectories))
                    {
                        using (StreamReader sr = new StreamReader(file))
                        {
                            script = sr.ReadToEnd();
                            model.AddObjects(script);
                        }
                    }

                    // build the sql directory and add to the output path
                    DacPackageExtensions.BuildPackage(
                      OutputPath,
                      model,
                      new PackageMetadata(), // Describes the dacpac. 
                      new PackageOptions());  // Use this to specify the deployment contributors, refactor log to include in package

                }


            }
            catch(Exception ex)
            {
                WriteWarning(ex.ToString());
                WriteWarning(script);
                throw (ex);
            }

        }
    }
}
