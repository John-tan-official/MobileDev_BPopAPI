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
    public class UsersAPIController : ApiController
    {
        private BPopAPIContext db = new BPopAPIContext();

        // GET: api/UsersAPI
        public IQueryable<Users> GetUsers()
        {
            return db.Users;
        }

        // GET: api/UsersAPI/5
        [ResponseType(typeof(Users))]
        public async Task<IHttpActionResult> GetUsers(int id)
        {
            Users users = await db.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        // PUT: api/UsersAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUsers(int id, Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != users.Id)
            {
                return BadRequest();
            }

            db.Entry(users).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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

        // POST: api/UsersAPI
        [ResponseType(typeof(Users))]
        public async Task<IHttpActionResult> PostUsers(Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(users);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = users.Id }, users);
        }

        // DELETE: api/UsersAPI/5
        [ResponseType(typeof(Users))]
        public async Task<IHttpActionResult> DeleteUsers(int id)
        {
            Users users = await db.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            db.Users.Remove(users);
            await db.SaveChangesAsync();

            return Ok(users);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsersExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/UsersLogin/studentID={studentID}/password={password}")]
        public async Task<IHttpActionResult> UserDetailsLogin(string studentID, string password)
        {
            Users login =
                         await db.Users.Where(x => x.StudentId == studentID && x.Password == password).SingleOrDefaultAsync();
            if (login == null)
            {
                return NotFound();
            }
            return Ok(login);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/UsersRegister/studentID={studentID}/password={password}/name={name}")]
        public async Task<IHttpActionResult> Create(string studentID, string password, string name)
        {
            Users register = new Users
            {
                StudentId = studentID,
                Password = password,
                Name = name,
                Team = "",
                Points = "0"
            };
            db.Users.Add(register);
            await db.SaveChangesAsync();

            return Ok(register);
        }
        
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/UsersUpdate/Id={id}/Team={team}/Points={points}")]
        public async Task<IHttpActionResult> UpdateUsers(int id, string team, string points)
        {
            Users login =
                         await db.Users.Where(x => x.Id == id).SingleOrDefaultAsync();
            
            login.Points = points;
            login.Team = team;

            db.Entry(login).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return Ok(login);
        }
    }
}