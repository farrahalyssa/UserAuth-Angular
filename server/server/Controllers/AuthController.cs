using Microsoft.AspNetCore.Mvc;
using server.Models;
using MySql.Data.MySqlClient;


namespace server.Controllers { //basically used for organizing the code, saying it belongs to server/Contollers
    [Route("api/[controller]")]
    [ApiController] //handles HTTP requests

//primary constructor syntax, only works for records and struct types, not normal classes 
    public class AuthController : ControllerBase { //the apicontroller, t inherits from ControllerBase, which provides HTTP request handling (like GET, POST, etc.).

        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
       [HttpPost]
        [Route("registration")]
        public IActionResult Registration(User user)
        {
            try
            {
                string? connectionString = _configuration.GetConnectionString("Users");
                

                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO User(userName, userId, userPassword, email, isActive) " +
                                   "VALUES(@userName, @userId, @userPassword, @email, true)";

                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@userName", user.userName);
                        cmd.Parameters.AddWithValue("@userId", user.userId);
                        cmd.Parameters.AddWithValue("@userPassword", user.userPassword);
                        cmd.Parameters.AddWithValue("@email", user.userEmail);
                        cmd.Parameters.AddWithValue("@isActive", user.isActive);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            return StatusCode(201, new { message = "Data inserted successfully." });
                        }
                        else
                        {
                            return BadRequest(new { message = "Error inserting data." });
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error: {ex.Message}" });
            }
        
    

        // public string login(Registration registration){
            //sql connection
            //sql data adapter ??
            // what is a datable 


            // valid user invalid user why dt.rows >0 ?? i dont get it

        // }

    }}
}