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
    public class DocumentoController : ApiController
    {
        private DBArquivoEntities db = new DBArquivoEntities();

        // GET: api/Documento
        public IQueryable<tblDocumento> GettblDocumento()
        {
            return db.tblDocumento;
        }

        // GET: api/Documento/5
        [ResponseType(typeof(tblDocumento))]
        public IHttpActionResult GettblDocumento(int id)
        {
            tblDocumento tblDocumento = db.tblDocumento.Find(id);
            if (tblDocumento == null)
            {
                return NotFound();
            }

            return Ok(tblDocumento);
        }

        // PUT: api/Documento/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblDocumento(int id, tblDocumento tblDocumento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblDocumento.id_Documento)
            {
                return BadRequest();
            }

            db.Entry(tblDocumento).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblDocumentoExists(id))
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

        // POST: api/Documento
        [ResponseType(typeof(tblDocumento))]
        public IHttpActionResult PosttblDocumento(tblDocumento tblDocumento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblDocumento.Add(tblDocumento);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tblDocumentoExists(tblDocumento.id_Documento))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tblDocumento.id_Documento }, tblDocumento);
        }

        // DELETE: api/Documento/5
        [ResponseType(typeof(tblDocumento))]
        public IHttpActionResult DeletetblDocumento(int id)
        {
            tblDocumento tblDocumento = db.tblDocumento.Find(id);
            if (tblDocumento == null)
            {
                return NotFound();
            }

            db.tblDocumento.Remove(tblDocumento);
            db.SaveChanges();

            return Ok(tblDocumento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblDocumentoExists(int id)
        {
            return db.tblDocumento.Count(e => e.id_Documento == id) > 0;
        }
    }
}