using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterBetsBackend.Models
{
    public class Card
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public virtual User User { get; set; }
        public int OrganizationID { get; set; }
        public virtual Organization Organization { get; set; }
        public string Token { get; set; }
    }
}