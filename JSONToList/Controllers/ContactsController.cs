using JSONToList.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace JSONToList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public ContactsController(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<Contact> GetContacts()
        {
            List<Contact> contacts = new List<Contact>();

            using (StreamReader reader = new StreamReader($"{_webHostEnvironment.ContentRootPath}/json/contacts.json"))
            {
                contacts = JsonConvert.DeserializeObject<List<Contact>>(reader.ReadToEnd());
            }

            InsertContacts(contacts);
            return contacts;
        }

        private void InsertContacts(List<Contact> contacts)
        {
            var cs = _configuration.GetConnectionString("TestConnection");

            using (SqlConnection cn = new SqlConnection(cs))
            {
                cn.Open();
                string sql = "insert_contact";

                foreach (Contact contact in contacts)
                {
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@name", contact.Name);
                        cmd.Parameters.AddWithValue("@phone", contact.Phone);
                        cmd.Parameters.AddWithValue("@email", contact.Email);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

        }
    }
}
