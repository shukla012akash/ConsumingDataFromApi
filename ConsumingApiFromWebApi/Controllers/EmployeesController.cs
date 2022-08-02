using ConsumingApiFromWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConsumingApiFromWebApi.Controllers
{
    public class EmployeesController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7211/api");
        HttpClient client;

        public EmployeesController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public ActionResult Get()
        {
            List<Employee> employeeList = new List<Employee>();

            HttpResponseMessage responseMessage = client.GetAsync(baseAddress + "/Employee/").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                employeeList = JsonConvert.DeserializeObject<List<Employee>>(data);
            }
            return View(employeeList);

        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            var postTask = client.PostAsJsonAsync<Employee>(baseAddress + "/Employee", employee);
            postTask.Wait();
            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Get");
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(employee);
        }




        


        public ActionResult Update(int id)
        {
            Employee employee = new Employee();
            HttpResponseMessage responseMessage = client.GetAsync(baseAddress + "/Employee/" + id.ToString()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                employee = JsonConvert.DeserializeObject<Employee>(data);
            }
            return View(employee);
        }


        public ActionResult UpdateEmp(Employee employee)
        {
            var putTask = client.PutAsJsonAsync<Employee>(baseAddress + "/Employee/" + employee.Id.ToString(), employee);
            putTask.Wait();

            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("Get");
            }
            return View(employee);
        }

        public ActionResult Delete(int id)
        {

            //HTTP DELETE
            var deleteTask = client.DeleteAsync(baseAddress + "/Employee/" + id.ToString());
            deleteTask.Wait();

            var result = deleteTask.Result;
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("Get");
            }

            return RedirectToAction("Delete");
        }

    }
}