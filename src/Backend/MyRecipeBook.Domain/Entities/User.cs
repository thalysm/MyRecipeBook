using System.ComponentModel.DataAnnotations.Schema;

namespace MyRecipeBook.Domain.Entities
{
    [Table("User")]
    public class User : EntityBase
    {

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;


    }
}
