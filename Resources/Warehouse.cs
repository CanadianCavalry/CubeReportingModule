using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CubeReportingModule.Resources
{
    public class Warehouse
    {
        public readonly uint warehouseId { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public List<Box> allBoxes { get; set; }

        public Warehouse(uint inId, string inName, string inLocation)
        {
            warehouseId = inId;
            name = inName;
            location = inLocation;
        }

        public void refreshAllBoxes()
        {
        }

        public void refreshBoxes(List<Box> toRefresh)
        {
        }

        public void addBox(Box toAdd)
        {
            allBoxes.Add(toAdd);
        }

        public Box getBox(uint boxId)
        {
            Box toFind = allBoxes.Find(toCheck => toCheck.boxId == boxId);
            return toFind;
        }
    }
}