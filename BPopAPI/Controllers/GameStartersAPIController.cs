using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BPopAPI.Models;

namespace BPopAPI.Controllers
{
    public class GameStartersAPIController : ApiController
    {
        private BPopAPIContext db = new BPopAPIContext();

        // GET: api/GameStartersAPI
        public IQueryable<GameStarter> GetGameStarters()
        {
            return db.GameStarters;
        }

        // GET: api/GameStartersAPI/5
        [ResponseType(typeof(GameStarter))]
        public async Task<IHttpActionResult> GetGameStarter(int id)
        {
            GameStarter gameStarter = await db.GameStarters.FindAsync(id);
            if (gameStarter == null)
            {
                return NotFound();
            }

            return Ok(gameStarter);
        }

        // PUT: api/GameStartersAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGameStarter(int id, GameStarter gameStarter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gameStarter.Id)
            {
                return BadRequest();
            }

            db.Entry(gameStarter).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameStarterExists(id))
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

        // POST: api/GameStartersAPI
        [ResponseType(typeof(GameStarter))]
        public async Task<IHttpActionResult> PostGameStarter(GameStarter gameStarter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GameStarters.Add(gameStarter);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = gameStarter.Id }, gameStarter);
        }

        // DELETE: api/GameStartersAPI/5
        [ResponseType(typeof(GameStarter))]
        public async Task<IHttpActionResult> DeleteGameStarter(int id)
        {
            GameStarter gameStarter = await db.GameStarters.FindAsync(id);
            if (gameStarter == null)
            {
                return NotFound();
            }

            db.GameStarters.Remove(gameStarter);
            await db.SaveChangesAsync();

            return Ok(gameStarter);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GameStarterExists(int id)
        {
            return db.GameStarters.Count(e => e.Id == id) > 0;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/UpdateGameState/Id={id}/Begin={begin}")]
        public async Task<IHttpActionResult> UpdateGame(int id, string begin)
        {
            GameStarter game =
                         await db.GameStarters.Where(x => x.Id == id).SingleOrDefaultAsync();
            game.begin = begin;

            db.Entry(game).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return Ok(game);
        }
    }
}