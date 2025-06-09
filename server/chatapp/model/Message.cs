using System;

namespace chatapp.model
{
    public class Message:ChatObject
    {
        public int Id { get; set; }
        public string Contents { get; set; }
        public int Status { get; set; }
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
