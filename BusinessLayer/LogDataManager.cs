using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class LogDataManager
    {
        private ADOLogDAO dao;
        private bool debug;

        public LogDataManager(ADOLogDAO dao,bool debug=false)
        {
            this.dao = dao;
            this.debug = debug;
        }
        public string LogToDB(string logInfo)
        {
            if (!string.IsNullOrWhiteSpace(logInfo))
            {
                try
                {
                    dao.WriteToDB(logInfo);
                    if (debug)
                        Console.WriteLine($"writing {logInfo}");
                    return "success";
                }
                catch(DAOException ex)
                {
                    if (debug)
                        Console.WriteLine(ex);
                    return "fail";
                }
            }
            throw new LogException("loginfo empty");
        }
        public void ShowLogdata(int rows = 5)
        {
            try
            {
                foreach(var x in dao.ReadFromDB(rows))
                {
                    Console.WriteLine($"{x.Item1},{x.Item2}");
                }
            }
            catch (DAOException ex)
            {
                if (debug)
                    Console.WriteLine("ex");
                Console.WriteLine("no data");
            }
            finally
            {
                Console.WriteLine("--------");
            }
        }
    }
}
