using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Management.Automation;
using System.Web.Http.Filters;


using Microsoft.SqlServer.Dac.Model;
using System.IO;

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

        [Cmdlet(VerbsCommon.Format, "DacpacAsSql")]
    public class DacpacToSqlCmdlet : Cmdlet
    {
        // Declare the parameters for the cmdlet.
        [Parameter(Mandatory = true)]
        public string InputPath { get; set; }


        // Declare the parameters for the cmdlet.
        [Parameter(Mandatory = true)]
        public string OutputPath { get; set; }




        protected override void ProcessRecord()
        {


                using (StreamWriter sw = new StreamWriter(OutputPath))
            using (TSqlModel modelFromDacpac = new TSqlModel(InputPath))
            {
                IEnumerable<TSqlObject> allObjects = model.GetObjects(QueryScopes);
                foreach (TSqlObject tsqlObject allObjects)
                {
                    string script;
                    if (tsqlObject.TryGetScript(out script))
                    {
                        sw.WriteLine(modelFromDacpac)
    
                        }

                }
            }
        }
    }
}