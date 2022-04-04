using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagement.Data
{

    public enum Status { Incomplete, Complete }
    public class Tasks
    {
        /// <summary>
        /// Class used for migration and updating database 
        /// </summary>
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public int Progress { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
