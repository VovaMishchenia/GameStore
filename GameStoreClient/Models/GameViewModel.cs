using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStoreClient.Models
{
    public class GameViewModel
    {
      
        public int Id { get; set; }
        [Required(ErrorMessage ="Введіть назву гри")]
        [MinLength(2,ErrorMessage ="Назва не може бути коротшою ніж два символи")]
        [MaxLength(100)]
        //[RegularExpression("\w")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage ="Введіть рік")]

        public int Year { get; set; }
       [Range(1,1000,ErrorMessage ="Ціна має бути в діапазоні 1-1000")]
        public double Price { get; set; }
        [Required(ErrorMessage ="Вкажіть кількість продукції")]
        public int Amount { get; set; }
        public string Image { get; set; }
        //Navigation properties
       [Required(ErrorMessage ="Виберіть жанр зі списку")]
        public string Genre { get; set; }
        [Required(ErrorMessage = "Виберіть розробника зі списку")]
        public string Developer { get; set; }



    }
}