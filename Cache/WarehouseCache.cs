using CubeReportingModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CubeReportingModule.Cache
{
    public class WarehouseCache
    {
        public static void createWarehouseCache()
        {
            Warehouse warehouseCache = new Warehouse(1, "defaultWarehouse", "Cube");
            HttpContext.Current.Application["warehouse"] = warehouseCache;
        }
    }
}