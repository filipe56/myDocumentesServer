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
using ArchiveService.Models;

namespace ArchiveService.Controllers
{
    public class UsuarioController : ApiController
    {
        private DBArquivoEntities db = new DBArquivoEntities();

        // GET: api/Usuario
        public IQueryable<tblUsuario> GettblUsuario()
        {
            return db.tblUsuario;
        }

        // GET: api/Usuario/5
        [ResponseType(typeof(tblUsuario))]
        public IHttpActionResult GettblUsuario(int id)
        {
            tblUsuario tblUsuario = db.tblUsuario.Find(id);
            if (tblUsuario == null)
            {
                return NotFound();
            }

            return Ok(tblUsuario);
        }

        // PUT: api/Usuario/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblUsuario([FromBody]int id, [FromBody] tblUsuario tblUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblUsuario.id_Usuario)
            {
                return BadRequest();
            }

            db.Entry(tblUsuario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblUsuarioExists(id))
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

        // POST: api/Usuario
        //[ResponseType(typeof(tblUsuario))]
        public IHttpActionResult PosttblUsuario([FromBody]tblUsuario tblUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblUsuario.Add(tblUsuario);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblUsuario.id_Usuario }, tblUsuario);
        }

        // DELETE: api/Usuario/5
        [ResponseType(typeof(tblUsuario))]
        public IHttpActionResult DeletetblUsuario(int id)
        {
            tblUsuario tblUsuario = db.tblUsuario.Find(id);
            if (tblUsuario == null)
            {
                return NotFound();
            }

            db.tblUsuario.Remove(tblUsuario);
            db.SaveChanges();

            return Ok(tblUsuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblUsuarioExists(int id)
        {
            return db.tblUsuario.Count(e => e.id_Usuario == id) > 0;
        }
    }
}