using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApp_CRUD_.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo client = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnPost()
        {
            client.name = Request.Form["name"];
            client.email = Request.Form["email"];
            client.phone = Request.Form["phone"];
            client.address = Request.Form["address"];

            if (client.name.Length == 0 || client.email.Length == 0 ||
                client.phone.Length == 0 || client.address.Length == 0) 
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                string connectionString = "Data Source=DESKTOP-3PP87AK;Initial Catalog=MyStore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "INSERT INTO clients " +
                                 "(name, email, phone, address) VALUES " +
                                 "(@name, @email, @phone, @address)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", client.name);
                        command.Parameters.AddWithValue("@email", client.email);
                        command.Parameters.AddWithValue("@phone", client.phone);
                        command.Parameters.AddWithValue("@address", client.address);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            client.name = ""; client.email = ""; client.phone = ""; client.address = "";
            successMessage = "New Client Added Correctly";

            Response.Redirect("/Clients/Index");
        }
    }
}
