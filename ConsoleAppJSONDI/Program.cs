using BusinessLayer;
using DataLayer;
using Microsoft.Extensions.Configuration;
using System;

namespace ConsoleAppJSONDI
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            var config = builder.Build();
            string connectionString = config.GetConnectionString("logDB");
            bool debug = bool.Parse(config.GetSection("app").GetSection("debug").Value);

            string kleur = config.GetSection("app").GetSection("ColorConsole").Value;
            switch (kleur)
            {
                case "Yellow": Console.ForegroundColor = ConsoleColor.Yellow; break;
                case "Blue": Console.ForegroundColor = ConsoleColor.Blue; break;
                default: Console.ForegroundColor = ConsoleColor.White; break;
            }

            ADOLogDAO dao = new ADOLogDAO(connectionString);
            LogDataManager m = new LogDataManager(dao, debug);
            m.LogToDB("json DI line 7");
            m.ShowLogdata();
        }
    }
}
