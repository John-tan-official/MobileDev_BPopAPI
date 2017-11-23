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
    public class QandAsController : ApiController
    {
        private BPopAPIContext db = new BPopAPIContext();

        // GET: api/QandAs
        public IQueryable<QandA> GetQandAs()
        {
            return db.QandAs;
        }

        // GET: api/QandAs/5
        [ResponseType(typeof(QandA))]
        public async Task<IHttpActionResult> GetQandA(int id)
        {
            QandA qandA = await db.QandAs.FindAsync(id);
            if (qandA == null)
            {
                return NotFound();
            }

            return Ok(qandA);
        }

        // PUT: api/QandAs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutQandA(int id, QandA qandA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != qandA.Id)
            {
                return BadRequest();
            }

            db.Entry(qandA).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QandAExists(id))
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

        // POST: api/QandAs
        [ResponseType(typeof(QandA))]
        public async Task<IHttpActionResult> PostQandA(QandA qandA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.QandAs.Add(qandA);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = qandA.Id }, qandA);
        }

        // DELETE: api/QandAs/5
        [ResponseType(typeof(QandA))]
        public async Task<IHttpActionResult> DeleteQandA(int id)
        {
            QandA qandA = await db.QandAs.FindAsync(id);
            if (qandA == null)
            {
                return NotFound();
            }

            db.QandAs.Remove(qandA);
            await db.SaveChangesAsync();

            return Ok(qandA);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QandAExists(int id)
        {
            return db.QandAs.Count(e => e.Id == id) > 0;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/QandAGetQuestions/id={id}")]
        public async Task<IHttpActionResult> UserDetailsUpdate(int id)
        {
            QandA questions = await db.QandAs.Where(x => x.Id == id).SingleOrDefaultAsync();

            if (questions == null)
            {
                return NotFound();
            }
            return Ok(questions);
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/QandAInsert/Question={question}/ChoiceA={choiceA}/ChoiceB={choiceB}/ChoiceC={choiceC}/ChoiceD={choiceD}/Answer={answer}")]
        public async Task<IHttpActionResult> Create(string question, string choiceA, string choiceB, string choiceC, string choiceD, string answer)
        {
            QandA register = new QandA
            {
                Question = question,
                ChoiceA = choiceA,
                ChoiceB = choiceB,
                ChoiceC = choiceC,
                ChoiceD = choiceD,
                Answer = answer
            };
            db.QandAs.Add(register);
            await db.SaveChangesAsync();

            return Ok(register);
        }
    }
}