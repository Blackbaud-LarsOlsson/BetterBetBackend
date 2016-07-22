using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterBetsBackend.Models
{
    public class Bet
    {
        public int ID { get; set; }
        public string Challenge { get; set; }
        public DateTime Expiration { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Offer> Offers { get; set; }
    }
}