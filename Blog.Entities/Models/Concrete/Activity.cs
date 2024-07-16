using Blog.Entities.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entities.Models.Concrete
{
    public class Activity : BaseEntity
    {

        public int UserId { get; set; }
        public User User { get; set; } 
        public string ActivityType { get; set; }
        public DateTime ActivityDate { get; set; } = DateTime.Now;
        public string Description { get; set; }
    }
}
