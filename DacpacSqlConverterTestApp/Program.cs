using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DacpacSqlConverter;


namespace DacpacSqlConverterTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DacpacToSqlCmdlet cmd = new DacpacToSqlCmdlet();
            cmd.Test();
        }
    }
}
