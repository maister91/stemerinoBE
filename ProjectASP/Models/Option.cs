using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectASP.Models
{
    public class Option
    {
        public int OptionID { get; set; }
        public string choice { get; set; }
        public int Votes { get; set; }
        public int PollID { get; set; }
        public Poll poll { get; set; }

        public ICollection<Stem> stems { get; set; }

    }
}
