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
using Angularwebapi.Models;
using System.Web.Http.Cors;

namespace Angularwebapi.Controllers
{
    //[EnableCors(origins: "http://localhost:52623", headers: "*", methods: "*")]
    public class EmployeesController : ApiController
    {
        private angularwebapidbEntities db = new angularwebapidbEntities();

        // GET: api/Employees
        public IQueryable<tblEmployee> GettblEmployees()
        {
            return db.tblEmployees;
        }

        // GET: api/Employees/5
        [ResponseType(typeof(tblEmployee))]
        public async Task<IHttpActionResult> GettblEmployee(int id)
        {
            tblEmployee tblEmployee = await db.tblEmployees.FindAsync(id);
            if (tblEmployee == null)
            {
                return NotFound();
            }

            return Ok(tblEmployee);
        }

        // PUT: api/Employees/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblEmployee(int id, tblEmployee tblEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblEmployee.EmployeeId)
            {
                return BadRequest();
            }

            db.Entry(tblEmployee).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblEmployeeExists(id))
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

        // POST: api/Employees
        [ResponseType(typeof(tblEmployee))]
        public async Task<IHttpActionResult> PosttblEmployee(tblEmployee tblEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblEmployees.Add(tblEmployee);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (tblEmployeeExists(tblEmployee.EmployeeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tblEmployee.EmployeeId }, tblEmployee);
        }

        // DELETE: api/Employees/5
        [ResponseType(typeof(tblEmployee))]
        public async Task<IHttpActionResult> DeletetblEmployee(int id)
        {
            tblEmployee tblEmployee = await db.tblEmployees.FindAsync(id);
            if (tblEmployee == null)
            {
                return NotFound();
            }

            db.tblEmployees.Remove(tblEmployee);
            await db.SaveChangesAsync();

            return Ok(tblEmployee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblEmployeeExists(int id)
        {
            return db.tblEmployees.Count(e => e.EmployeeId == id) > 0;
        }
    }
}