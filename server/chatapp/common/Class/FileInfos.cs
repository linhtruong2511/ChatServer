using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.common.Class
{
    public class FileInfos
    {
        public int id { get; set; }
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public int source { get; set; }
        public int destination { get; set; }
        public DateTime createAt { get; set; }
        public bool Status { get; set; }
        public FileInfos() { }
    }
}
