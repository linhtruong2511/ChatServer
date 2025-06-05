using System;

namespace chatapp.model
{
    public abstract class ChatObject
    {
        public int Source { get; set; }
        public int Destination { get; set; }
        public DateTime CreateAt { get; set;} 
        
    }
}
