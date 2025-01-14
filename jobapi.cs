using job_api.Controllers;
using Microsoft.EntityFrameworkCore;

namespace job_api.Data
{
    public class ApplicationDbContext : DbContext
    {

        public int Id { get; set; } // Assuming you have an Id field
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string LinkedIn { get; set; }
        public string CoverLetter { get; set; }
        // Additional properties as needed
        // E.g., ResumePath if you store the file path
    }
}
