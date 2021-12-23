using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookStore.Models
{
    public class Book
    {
        public int B_Id { get; set; }
        public string Type { get; set; }

        //public string Activity { get; set; }
        //public string Story { get; set; }
        //public string Subjects { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Author { get; set; }
    }
}
