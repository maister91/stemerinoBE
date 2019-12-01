using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectASP.Models
{
    public class Stem
    {
        public long StemID { get; set; }
        public long GebruikerID { get; set; }
        public long OptionID { get; set; }
        public long aantalStemmen { get; set; }

        public Gebruiker gebruiker { get; set; }
        public Option option { get; set; }
    }
}
