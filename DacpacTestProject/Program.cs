using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DacpacSqlConverter;
using System.Management.Automation;

namespace DacpacTestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            DacpacToSqlCmdlet cmd = new DacpacToSqlCmdlet();

           cmd.OutputDirectory = "C:/Users/smith/Documents/GitHub/LifespeakSqlServer";
           cmd.InputPath = "C:/Users/smith/Documents/GitHub/staging.dacpac";
            cmd.Test();
           // SqlDacpacConverter cmd = new SqlDacpacConverter();
           // cmd.OutputPath = "C:/dacpac/t1.dacpac";
           // cmd.FileDirectory = "C:/Users/smith/Documents/GitHub/LifespeakSqlServer/Redesign/";
           //cmd.ProcessRecord();
        }
    }
}
