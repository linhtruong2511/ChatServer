using chatapp.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.managelist
{
    internal class ManageTaskList
    {
        public List<Task> tasklist { get; set; }
        public ManageTaskList()
        {
            this.tasklist=new List<Task>();
        }
        ~ManageTaskList()
        {
            for (int i = 0; i < this.tasklist.Count; i++)
            {
                this.tasklist[i].Dispose();
            }
        }
        public void AddTask(Task task)
        {
            this.tasklist.Add(task);
        }
        public void DeleteTask(Task task)
        {
            this.tasklist.Remove(task);
        }
        public bool IsAllTaskCompleted()
        {
            foreach (Task i in this.tasklist)
            {
                if (!i.IsCompleted)
                    return false;
            }
            return true;
        }
    }
}
