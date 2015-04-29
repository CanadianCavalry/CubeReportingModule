using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CubeReportingModule.Resources
{
    public class File
    {
        private readonly uint fileId;
        public List<FileItem> allFileItems { get; set; }

        public File(uint inFileId)
        {
            fileId = inFileId;
        }

        public uint FileId
        {
            get { return fileId; }
        }

        public void addFileItem(FileItem toAdd)
        {
            allFileItems.Add(toAdd);
        }

        public FileItem getFileItem(string fileItemName)
        {
            FileItem toFind = allFileItems.Find(toCheck => toCheck.Name == fileItemName);
            return toFind;
        }
    }
}