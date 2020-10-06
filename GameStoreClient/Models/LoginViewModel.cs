using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStoreClient.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Логін не може бути порожнім")]
        public string Login { get; set; }
        [Required(ErrorMessage ="Введіть пароль")]
        public string Password { get; set; }
    }
}