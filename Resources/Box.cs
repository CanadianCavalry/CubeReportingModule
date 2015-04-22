using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CubeReportingModule.Resources
{
    public class Box
    {
        public readonly uint boxId { get; set; }
        public string location { get; set; }
        public readonly Client owner { get; set; }
        public List<File> allFiles { get; set; }

        public Box(uint inBoxId, string inLocation, Client inOwner)
        {
            boxId = inBoxId;
            location = inLocation;
            owner = inOwner;
        }

        public void addFile(File toAdd)
        {
            allFiles.Add(toAdd);
        }

        public File getFile(uint fileId)
        {
            File toFind = allFiles.Find(toCheck => toCheck.fileId == fileId);
            return toFind;
        }
    }
}