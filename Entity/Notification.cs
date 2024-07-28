using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamHub.Entity
{
    public class Notification : Base
    {
        public string Message { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
         public bool IsRead { get; set; }
        public DateTime DateCreated { get; internal set; }
    }
}