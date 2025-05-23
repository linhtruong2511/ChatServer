using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.model
{
    /// <summary>
    /// lớp đại diện cho thực thể message trong cơ sở dữ liệu
    /// </summary>
    public class Message
    {
        public int Id { get; set; }
        public string Contents { get; set; }
        public int Source { get; set; }
        public int Destination { get; set; }
        public int Status { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;

        public Message(string data, int source, int destination, int status)
        {
            this.Contents = data;
            this.Source = source;
            this.Destination = destination;
            this.Status = status;
        }
        public Message() { }
    }
}
