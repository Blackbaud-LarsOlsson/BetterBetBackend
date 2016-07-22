using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterBetsBackend.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Offer> Offers { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
    }
}