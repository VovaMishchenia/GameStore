using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStoreClient.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Логін не може бути пустим")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Eamil не може бути пустим")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Номер телефону не може бути пустим")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Пароль не може бути пустим")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        public string Confirm { get; set; }
    }
}