using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CubeReportingModule.Resources
{
    public class Warehouse
    {
        private readonly uint warehouseId;
        public string name { get; set; }
        public string location { get; set; }
        public List<Box> allBoxes { get; set; }

        public Warehouse(uint inId, string inName, string inLocation)
        {
            warehouseId = inId;
            name = inName;
            location = inLocation;
        }

        public uint WarehouseId
        {
            get { return warehouseId; }
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
            Box toFind = allBoxes.Find(toCheck => toCheck.BoxId == boxId);
            return toFind;
        }

        public void removeBox(uint boxId)
        {
            Box toRemove = allBoxes.Find(toCheck => toCheck.BoxId == boxId);
            allBoxes.Remove(toRemove);
        }
    }
}