using System.ComponentModel.DataAnnotations;

namespace ReadingEnhancer.Domain.Entities
{
    public class User : BaseEntity
    {
        [MinLength(2)]
        public string Name { get; set; }  
        [MinLength(2)]
        public string LastName { get; set; }
        [MinLength(3)]
        public string Username { get; set; }
        [MinLength(8)]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}