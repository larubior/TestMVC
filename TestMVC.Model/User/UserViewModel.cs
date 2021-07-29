using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TestMVC.Model.User
{
    public class UserViewModel
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El valor del campo Email supera el valor permitido 100 caracteres")]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Usuario")]
        [Required]
        [StringLength(50, ErrorMessage = "El valor del campo Usuario no se encuentra en el rango de valores permitidos, máximo 50, mínimo 7 caracteres", MinimumLength = 7)]
        public string Username { get; set; }

        [DisplayName("Contraseña")]
        [Required]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "El valor del campo Password no se encuentra en el rango de valores permitidos, máximo 50, mínimo 8 caracteres")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Las contraseñas deben tener al menos 8 caracteres y contener 3 de 4 de los siguientes: mayúsculas (A-Z), minúsculas (a-z), números (0-9) y caracteres especiales (e.j. !@#$%^&*)")]
        public string Password { get; set; }

        [DisplayName("Confirmar Contraseña")]
        [Required]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "El valor del campo Password no se encuentra en el rango de valores permitidos, máximo 50, mínimo 8 caracteres")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Las contraseñas deben tener al menos 8 caracteres y contener 3 de 4 de los siguientes: mayúsculas (A-Z), minúsculas (a-z), números (0-9) y caracteres especiales (e.j. !@#$%^&*)")]
        public string ConfirmPassword { get; set; }

        public string Status { get; set; }

        [DisplayName("Sexo")]
        [Required]
        public string Genre { get; set; }

        public DateTime? DateCreated { get; set; }
    }
}
