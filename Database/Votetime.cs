using System;
using System.Collections.Generic;

#nullable disable

namespace Bazadanych
{
    public partial class Votetime
    {
        public Votetime()
        {
            Votetopics = new HashSet<Votetopic>();
        }

        public decimal Votetimeid { get; set; }
        public DateTime Votestarttime { get; set; }
        public DateTime Votestoptime { get; set; }

        public virtual ICollection<Votetopic> Votetopics { get; set; }
    }
}
