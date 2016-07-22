using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterBetsBackend.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public int BetID { get; set; }
        public virtual Bet Bet { get; set; }
        public int UserID { get; set; }
        public virtual User User { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
    }
}