using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CubeReportingModule.Models
{
    public class Box
    {
        private readonly uint boxId;
        public string location { get; set; }
        private readonly Client owner;
        public List<File> allFiles { get; set; }

        public Box(uint inBoxId, string inLocation, Client inOwner)
        {
            boxId = inBoxId;
            location = inLocation;
            owner = inOwner;
        }

        public uint BoxId
        {
            get { return boxId; }
        }

        public Client Ownder
        {
            get { return owner; }
        }

        public void addFile(File toAdd)
        {
            allFiles.Add(toAdd);
        }

        public File getFile(uint fileId)
        {
            File toFind = allFiles.Find(toCheck => toCheck.FileId == fileId);
            return toFind;
        }
    }
}