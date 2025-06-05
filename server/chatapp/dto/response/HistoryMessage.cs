using chatapp.model;
using System;

namespace chatapp.dto.response
{
    public class HistoryMessage : ChatObject 
    {
        public string Contents { get; set; }
        public HistoryMessage(Message message) 
        {
            this.Contents = message.Contents;
            this.Source = message.Source;
            this.CreateAt = message.CreateAt;
        }
        public HistoryMessage() { }
    }
}
