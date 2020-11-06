using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StartAPI.Domain;
using StartAPI.Repository;

namespace StartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class DepartmentsController : ControllerBase
    {
        private readonly DepartamentContext _context;

        public DepartmentsController(DepartamentContext context)
        {
            _context = context;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {

            var listDepartments = await _context.Departments.Include(departments => departments.Employees).ToListAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(listDepartments);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }

        // GET: api/Departments/
        [HttpGet("{id}")]
        public ActionResult<Department> GetDepartment(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var department = _context.Departments.Include(d => d.Employees).AsNoTracking().FirstOrDefault(d => d.Id == id);

                if (department != null)
                {
                    return Ok(department);
                }

                return NotFound();

            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // PUT: api/Departments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, Department department)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
               
                if (_context.Departments.AsNoTracking().FirstOrDefault(h => h.Id == id) != null)
                {
                    _context.Update(department);
                   await _context.SaveChangesAsync();

                    return CreatedAtAction("GetDepartment", new { id = department.Id }, department);
                }
             
                return Ok("Nenhum departamento encontrado.");
                
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // POST: api/Departments
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Departments.Add(department);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetDepartment", new { id = department.Id }, department);

            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }

    }
}
