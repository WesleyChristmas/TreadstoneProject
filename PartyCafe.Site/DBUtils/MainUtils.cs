using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace PartyCafe.Site.DBUtils
{
    public static class MainUtils
    {
        public static PartyCafeClassesDataContext GetDBContext()
        {
            ConnectionStringSettings connSettings = ConfigurationManager.ConnectionStrings["PartyCafeDbContext"];
            if (connSettings == null || String.IsNullOrEmpty(connSettings.ConnectionString))
            {
                throw new Exception("Incorrect connection string, please check it in Web.config file!");
            }

            return new PartyCafeClassesDataContext(connSettings.ConnectionString);
        } 
    }
}