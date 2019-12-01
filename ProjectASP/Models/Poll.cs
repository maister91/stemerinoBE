using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectASP.Models
{
    public class Poll
    {
        public int PollID { get; set; }
        public string Topic { get; set; }
        public int Count { get; set; }

        public ICollection<Option> options { get; set; }
        public ICollection<PollGebruiker> pollGebruikers { get; set; }
       

    }

}
