using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStoreDAL.Entities
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public double  Price { get; set; }
        public string Image { get; set; }
        public int Amount { get; set; }
        //Navigation properties
        public int? GenreId { get; set; }
        public int? DeveloperId { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual Developer Developer { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public Game() 
        {
            Orders = new List<Order>();
        }
    }
}
