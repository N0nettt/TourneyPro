using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomskiRad.Database
{
    public static class GlobalConfig
    {
        public static IDataConnection SqlConnection = new SqlConnector();
        
        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
