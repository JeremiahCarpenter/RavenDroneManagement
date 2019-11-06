using System;
using System.Collections.Generic;
using RavenDAL;
using System.Data.SqlClient;


namespace Raven
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ContextDAL ctx = new ContextDAL())
            {
                ctx.ConnectionString = $@"Data Source =.\SQLEXPRESS; Initial Catalog = Raven; Integrated Security = True";

                Console.WriteLine(ctx.ObtainRoleCount());
                Console.WriteLine(ctx.FindRoleByID(5));
                Console.WriteLine(ctx.FindRoleByID(1));

                List<UsersDAL> answer = ctx.GetUsers(0, 100);
                Console.WriteLine("**********");
                foreach(var x in answer)
                {
                    Console.WriteLine(x);
                }
            }
        }
    }
}
