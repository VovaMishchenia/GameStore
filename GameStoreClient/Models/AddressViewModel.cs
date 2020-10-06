using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStoreClient.Models
{
    public class AddressViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введіть назву країни, де ви проживаєте")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Введіть назву міста, де ви проживаєте")]
        public string City { get; set; }
        [Required(ErrorMessage = "Введіть назву вулиці, де ви проживаєте")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Введіть номер будинку, де ви проживаєте")]
        public int Building { get; set; }
    }
}