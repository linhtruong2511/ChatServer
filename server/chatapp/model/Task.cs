﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.model
{
    public class Task
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public string Content { get; set; }
    }
}
