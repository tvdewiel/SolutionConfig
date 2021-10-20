using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DAOException : Exception
    {
        public DAOException(string message) : base(message)
        {
        }

        public DAOException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
