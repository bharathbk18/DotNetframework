using System.ComponentModel.DataAnnotations;

namespace Assessmentapi.Models
{
    public class Userdetails
    {
        [Key]
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
