using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Database.Models
{
    public class Developer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
    }
}
