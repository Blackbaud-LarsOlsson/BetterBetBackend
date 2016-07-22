using System.Collections.Generic;
using System.Web.Http;
using Blackbaud.AppFx.PaymentProcessing.PaymentServiceProxy;
using System;
using BetterBetsBackend.Models;
using BetterBetsBackend.DAL;

namespace BetterBetsBackend.Controllers
{
    public class GeneralController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        //[Route("api/transaction/{amount}/{ip}/{cardToken}/{merchantId}")]
        //[HttpPost]
        //public IHttpActionResult Process(float amount, string ip, string cardToken, string merchantId)
        //{
        //    ProcessCardNotPresentRequest request = new ProcessCardNotPresentRequest();
        //    request.Amount = decimal.Parse(amount.ToString());
        //    request.DonorIPAddress = ip;
        //    request.CardToken = cardToken;
        //    request.MerchantAccountID = Guid.Parse(merchantId);
        //    request.TransactionType = TransactionType.Sale;
        //    request.DemoMode = false;
        //    PaymentService webproxy = new PaymentService();
        //    webproxy.Credentials = new System.Net.NetworkCredential("BBPSTestUser", "Password1234");
        //    webproxy.Url = "http://payment.service.blackbaudhost.com/BBPS/additional_services/paymentprocessing.asmx";
        //    webproxy.PreAuthenticate = true;
        //    ProcessCardNotPresentReply reply = webproxy.ProcessCardNotPresent(request);
        //    return Ok(reply);
        //}

        [Route("api/SignIn")]
        [HttpPost]
        public IHttpActionResult SignIn(SignInRequest request)
        {
            foreach (User u in db.Users)
            {
                if (u.Name == request.Name)
                {
                    if (u.Password != request.Password)
                    {
                        return BadRequest(Newtonsoft.Json.JsonConvert.SerializeObject(
                            new SignUpReply { UserId = 0, Success = false, Message = "Password was incorrect."}));
                    }
                    else
                    {
                        return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(
                            new SignUpReply { UserId = u.ID, Success = true, Message = "" }));
                    }
                }
            }
            return BadRequest(Newtonsoft.Json.JsonConvert.SerializeObject(
                new SignUpReply { UserId = 0, Success = false, Message = "Name was not found." }));
            
        }

        [Route("api/SignUp")]
        [HttpPost]
        public IHttpActionResult SignUp(SignUpRequest request)
        {
            foreach (User u in db.Users)
            {
                if (u.Name == request.Name)
                {
                    return BadRequest(Newtonsoft.Json.JsonConvert.SerializeObject(
                        new SignUpReply { UserId = 0, Success = false, Message = "Name was already taken." }));
                }
                if (u.Email == request.Email)
                {
                    return BadRequest(Newtonsoft.Json.JsonConvert.SerializeObject(
                        new SignUpReply { UserId = 0, Success = false, Message = "Email was already taken." }));
                }
            }
            try
            { 
                User created = db.Users.Add(
                    new User { Name = request.Name, Email = request.Email, Password = request.Password, Image = request.Image });
                db.SaveChanges();
                return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(
                    new SignUpReply { UserId = created.ID, Success = true, Message = "" }));
            }
            catch (Exception e)
            {
                return BadRequest(Newtonsoft.Json.JsonConvert.SerializeObject(
                new SignUpReply { UserId = 0, Success = false, Message = "Unknown error.\n" + e.ToString() }));
            }
        }

        [Route("api/PostChallenge")]
        [HttpPost]
        public IHttpActionResult PostChallenge(PostChallengeRequest request)
        {
            User u = db.Users.Find(request.UserID);
            if (u == null)
            {
                return BadRequest(Newtonsoft.Json.JsonConvert.SerializeObject(
                    new PostChallengeReply { Success = false, Message = "User could not be found." }));
            }
            Organization o = db.Organizations.Find(request.OrganizationID);
            if (o == null)
            {
                return BadRequest(Newtonsoft.Json.JsonConvert.SerializeObject(
                    new PostChallengeReply { Success = false, Message = "Organization could not be found." }));
            }
            try
            {
                Bet createdBet = db.Bets.Add(new Bet { Challenge = request.Challenge, Expiration = request.Expiration});
                db.SaveChanges();
                Offer createdOffer = db.Offers.Add(
                    new Offer { Amount = request.Amount, Bet = createdBet, Condition = request.Condition, Organization = o, User = u});
                db.SaveChanges();
                createdBet.Offers.Add(createdOffer);
                db.SaveChanges();
                o.Offers.Add(createdOffer);
                db.SaveChanges();
                u.Offers.Add(createdOffer);
                db.SaveChanges();
                return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(
                    new PostChallengeReply { Success = true, Message = "" }));
            }
            catch (Exception e)
            {
                return BadRequest(Newtonsoft.Json.JsonConvert.SerializeObject(
                    new PostChallengeReply { Success = false, Message = "Unknown error.\n" + e.ToString() }));
            }
            
        }

        [Route("api/AddOffer")]
        [HttpPost]
        public IHttpActionResult AddOffer(AddOfferRequest request)
        {
            Bet b = db.Bets.Find(request.BetID);
            if (b == null)
            {
                return BadRequest(Newtonsoft.Json.JsonConvert.SerializeObject(
                    new AddOfferReply { Success = false, Message = "Bet could not be found." }));
            }
            User u = db.Users.Find(request.UserID);
            if (u == null)
            {
                return BadRequest(Newtonsoft.Json.JsonConvert.SerializeObject(
                    new AddOfferReply { Success = false, Message = "User could not be found." }));
            }
            Organization o = db.Organizations.Find(request.OrganizationID);
            if (o == null)
            { 
                return BadRequest(Newtonsoft.Json.JsonConvert.SerializeObject(
                    new AddOfferReply { Success = false, Message = "Organization could not be found." }));
            }
            try
            {
                Offer createdOffer = db.Offers.Add(
                    new Offer { Amount = request.Amount, Bet = b, Condition = request.Condition, Organization = o, User = u});
                db.SaveChanges();
                b.Offers.Add(createdOffer);
                db.SaveChanges();
                o.Offers.Add(createdOffer);
                db.SaveChanges();
                u.Offers.Add(createdOffer);
                return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(
                    new AddOfferReply { Success = true, Message = "" }));
            }
            catch (Exception e)
            {
                return BadRequest(Newtonsoft.Json.JsonConvert.SerializeObject(
                    new AddOfferReply { Success = false, Message = "Unknown error.\n" + e.ToString() }));
            }
        }

        [Route("api/UpdateOffer")]
        [HttpPost]
        public IHttpActionResult UpdateOffer(UpdateOfferRequest request)
        {
            Offer o = db.Offers.Find(request.OfferID);
            if (o == null)
            {
                return BadRequest(Newtonsoft.Json.JsonConvert.SerializeObject(
                    new UpdateOfferReply { Success = false, Message = "Offer could not be found." }));
            }
            if (o.Outcome != null)
            {
                return BadRequest(Newtonsoft.Json.JsonConvert.SerializeObject(
                    new UpdateOfferReply { Success = false, Message = "Outcome has already been determined." }));
            }
            try
            {
                o.Outcome = request.Outcome;
                db.SaveChanges();
                return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(
                    new UpdateOfferReply { Success = true, Message = "" }));
            }
            catch (Exception e)
            {
                return BadRequest(Newtonsoft.Json.JsonConvert.SerializeObject(
                    new UpdateOfferReply { Success = false, Message = "Unknown error.\n" + e.ToString() }));
            }
        }
    }
}
