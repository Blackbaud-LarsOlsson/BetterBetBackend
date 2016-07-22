using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterBetsBackend.Models
{
    public class SignInRequest
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }

    public class SignInReply
    {
        public int UserID { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class SignUpRequest
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
    }

    public class SignUpReply
    {
        public int UserId { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class PostChallengeRequest
    {
        public string Challenge { get; set; }
        public int UserID { get; set; }
        public decimal Amount { get; set; }
        public DateTime Expiration { get; set; }
        public int OrganizationID { get; set; }
        public string Condition { get; set; }
    }

    public class PostChallengeReply
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class AddOfferRequest
    {
        public int BetID { get; set; }
        public int UserID { get; set; }
        public decimal Amount { get; set; }
        public int OrganizationID { get; set; }
        public string Condition { get; set; }
    }

    public class AddOfferReply
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class UpdateOfferRequest
    {
        public int OfferID { get; set; }
        public string Outcome { get; set; }
    }

    public class UpdateOfferReply
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

}