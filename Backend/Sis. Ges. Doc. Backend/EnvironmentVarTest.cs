using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetEnv;

namespace Sis.Ges.Doc.Backend
{
    public class EnvironmentVarTest
    {
        static void Main()
        {
            Env.Load();

            string db_host = Environment.GetEnvironmentVariable("DB_HOST");
            string db_user = Environment.GetEnvironmentVariable("DB_USER");
            string db_pass = Environment.GetEnvironmentVariable("DB_PASS");

            Console.WriteLine($"Host: {db_host}");
            Console.WriteLine($"User: {db_user}");
            Console.WriteLine($"Pass: {db_pass}");

        }
    }
}