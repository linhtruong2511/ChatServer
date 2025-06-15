using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.model
{
    public class Salary
    {
        public int UserID { get; set; } 
        public int Id { get; set; } 
        public decimal Base_Salary { get; set; }    
        public decimal Bonus { get; set; }
        public DateTime Date { get; set; } // Assuming you have a date field to track when the salary was issued
}
}
