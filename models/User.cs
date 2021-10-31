using System.ComponentModel.DataAnnotations;

namespace Shop.models
{
    
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        
        [Required(ErrorMessage = "Este campo é Obrigatorio")]
        [MaxLength(20,ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3,ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        public string Username { get; set; }
        
        
        [Required(ErrorMessage = "Este campo é Obrigatorio")]
        [MaxLength(60,ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3,ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        public string Password { get; set; } 

        public string Role { get; set; }
    }
}