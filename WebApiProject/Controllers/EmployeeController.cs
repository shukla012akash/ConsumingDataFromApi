using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApiProject.Controllers.Model;
using WebApiProject.Data;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class EmployeeController : Controller
    {
       
        
        
        
            private ApiDemoDbContext _context;

            public EmployeeController(ApiDemoDbContext employeeApiContext)
            {
                _context = employeeApiContext;
            }
            // GET: EmployeesController
            [HttpGet]
            public ActionResult Index()
            {
                var employees = _context.Employee.ToList();
                _context.SaveChanges();
                if (employees != null)
                {
                    string jsondata = JsonConvert.SerializeObject(employees);
                    return Ok(jsondata);
                }
                else
                {
                    return NotFound();
                }

            }
            [HttpGet("id")]
            public ActionResult GetEmployee(int Id)
            {
                try
                {
                    var emp = _context.Employee.Find(Id);
                    _context.SaveChanges();
                    var data = JsonConvert.SerializeObject(emp);
                    return Ok(data);

                }
                catch
                {
                    return NotFound();

                }

            }

            // GET: EmployeesController/Details/5
            [HttpGet("search")]
            public ActionResult Details(string search)
            {
                var employee = _context.Employee.Where(e => e.City == search || e.Country == search || e.FirstName == search || e.LastName == search).ToList();
                if (employee != null)
                {
                    var json = JsonConvert.SerializeObject(employee);
                    return Ok(json);
                }
                else
                {
                    return NotFound(search);
                }

            }

            // GET: EmployeesController/Create
            [HttpPost]
            public ActionResult Create(Employee employee)
            {
                if (employee == null)
                {
                    return BadRequest();
                }
                else
                {
                    _context.Employee.Add(employee);
                    _context.SaveChanges();
                    return Ok("Created Successfully");
                }
            }



        // GET: EmployeesController/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_context.Employee == null)
            {
                return NotFound();
            }
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult Edit(Employee employee)
            {

                // var emp= _context.Employee.Find(employee.Id);   
                if (employee != null)
                {
                    _context.Employee.Update(employee);
                    _context.SaveChanges();
                    return Ok("Updated Successfully");
                }
                else
                {
                    return NotFound();
                }


            }


        
    }
}
