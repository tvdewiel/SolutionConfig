using BusinessLayer;
using DataLayer;
using System;
using System.Configuration;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string connectionString = ConfigurationManager.ConnectionStrings["logDB"].ConnectionString;
            bool debug = Convert.ToBoolean(ConfigurationManager.AppSettings["debug"]);
            string kleur = ConfigurationManager.AppSettings["ColorConsole"];
            switch (kleur)
            {
                case "Yellow": Console.ForegroundColor = ConsoleColor.Yellow; break;
                case "Blue": Console.ForegroundColor = ConsoleColor.Blue; break;
                default: Console.ForegroundColor = ConsoleColor.White; break;
            }
            ADOLogDAO dao = new ADOLogDAO(connectionString);
            LogDataManager m = new LogDataManager(dao,debug);
            m.LogToDB("line 4");
            m.ShowLogdata();
        }
    }
}
