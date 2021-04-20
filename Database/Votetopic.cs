using System;
using System.Collections.Generic;

#nullable disable

namespace Bazadanych
{
    public partial class Votetopic
    {
        public decimal Votetopicid { get; set; }
        public string Maininformation { get; set; }
        public decimal Votetimeid { get; set; }
        public decimal Optiongroupid { get; set; }

        public virtual Votetime Optiongroup { get; set; }
    }
}
