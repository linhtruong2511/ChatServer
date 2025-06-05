using chatapp.model;
using System;

namespace chatapp.dto.response
{
    public class HistoryMessage
    {
        public string Contents { get; set; }
        public int Source { get; set; }
        public DateTime CreateAt { get; set; }
        public HistoryMessage(Message message) 
        {
            this.Contents = message.Contents;
            this.Source = message.Source;
            this.CreateAt = message.CreateAt;
        }
        public HistoryMessage() { }
    }
}
