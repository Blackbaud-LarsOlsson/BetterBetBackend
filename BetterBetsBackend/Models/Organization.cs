using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterBetsBackend.Models
{
    public class Organization
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Merchant { get; set; }
        public virtual ICollection<Offer> Offers { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
    }
}