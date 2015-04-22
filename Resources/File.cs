using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CubeReportingModule.Resources
{
    public class File
    {
        public readonly uint fileId { get; set; }
        public List<FileItem> allFileItems { get; set; }

        public File(uint inFileId)
        {
            fileId = inFileId;
        }

        public void addFileItem(FileItem toAdd)
        {
            allFileItems.Add(toAdd);
        }

        public FileItem getFileItem(string fileItemName)
        {
            FileItem toFind = allFileItems.Find(toCheck => toCheck.name == fileItemName);
            return toFind;
        }
    }
}