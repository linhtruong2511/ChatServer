using System;

namespace chatapp.model
{
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
