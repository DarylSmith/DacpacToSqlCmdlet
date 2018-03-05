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
    public class SqlDacpacConverter
    {

        public void BuildDacpacFromFiles()
        {
            using (TSqlModel model = new TSqlModel(SqlServerVersion.Sql140, new TSqlModelOptions()))
            {
                foreach (string file in System.IO.Directory.GetFiles("C:/dacpac", "*.sql", SearchOption.AllDirectories))
                {
                    using (StreamReader sr = new StreamReader(file))
                    {
                        string script = sr.ReadToEnd();
                        model.AddObjects(script);
                    }
                }

                DacPackageExtensions.BuildPackage(
                  "C:/dacpac/test.dacpac",
                  model,
                  new PackageMetadata(), // Describes the dacpac. 
                  new PackageOptions());  // Use this to specify the deployment contributors, refactor log to include in package

            }


        }
    }
}
