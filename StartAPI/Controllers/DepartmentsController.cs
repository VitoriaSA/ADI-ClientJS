using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
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

        public static IWebHostEnvironment _environment;

        public DepartmentsController(DepartamentContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public class FileUpload
        {
            public IFormFile files { get; set; }
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

        [HttpPost("UploadFile")]
        public async Task<string> UploadFile([FromForm] FileUpload objfile)
        {
            try
            {
                if (objfile.files.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\images\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\images\\");
                    }

                    using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\images\\" + objfile.files.FileName))
                    {
                        objfile.files.CopyTo(filestream);
                        filestream.Flush();
                        return "\\images\\" + objfile.files.FileName;
                    }
                }
                else
                {
                    return "Falhou";
                }
            }
            catch (ArgumentException e)
            {
                return e.Message.ToString();
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
