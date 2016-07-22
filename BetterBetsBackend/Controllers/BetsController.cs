using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BetterBetsBackend.DAL;
using BetterBetsBackend.Models;

namespace BetterBetsBackend.Controllers
{
    public class BetsController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: api/Bets
        public IQueryable<Bet> GetBets()
        {
            return db.Bets;
        }

        // GET: api/Bets/5
        [ResponseType(typeof(Bet))]
        public IHttpActionResult GetBet(int id)
        {
            Bet bet = db.Bets.Find(id);
            if (bet == null)
            {
                return NotFound();
            }

            return Ok(bet);
        }

        // PUT: api/Bets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBet(int id, Bet bet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bet.ID)
            {
                return BadRequest();
            }

            db.Entry(bet).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Bets
        [ResponseType(typeof(Bet))]
        public IHttpActionResult PostBet(Bet bet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Bets.Add(bet);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bet.ID }, bet);
        }

        // DELETE: api/Bets/5
        [ResponseType(typeof(Bet))]
        public IHttpActionResult DeleteBet(int id)
        {
            Bet bet = db.Bets.Find(id);
            if (bet == null)
            {
                return NotFound();
            }

            db.Bets.Remove(bet);
            db.SaveChanges();

            return Ok(bet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BetExists(int id)
        {
            return db.Bets.Count(e => e.ID == id) > 0;
        }
    }
}