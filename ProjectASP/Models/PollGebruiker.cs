using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectASP.Models
{
    public class PollGebruiker
    {
        public long PollGebruikerID { get; set; }
        public long PollID { get; set; }
        public long GebruikerID { get; set; }

        public Poll poll { get; set; }
   
        public Gebruiker gebruiker { get; set; }

        
    }
}
