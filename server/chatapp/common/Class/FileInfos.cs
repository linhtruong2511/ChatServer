using chatapp.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.common.Class
{
    public class FileInfos : ChatObject
    {
        public int id { get; set; }
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public bool Status { get; set; }
        public FileInfos() { }
    }
}
