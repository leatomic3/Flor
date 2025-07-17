using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flor.Models
{
    public class NarudzbaStavka
    {
        public int Id { get; set; }

        [Required]
        public int NarudzbaId { get; set; }

        [Required]
        public int BuketId { get; set; }

        [Required]
        public int Kolicina { get; set; }

        [Required]
        public string Velicina { get; set; } 

        public decimal Cijena { get; set; } 

        [ForeignKey("NarudzbaId")]
        public Narudzba Narudzba { get; set; }

        [ForeignKey("BuketId")]
        public Buket Buket { get; set; }
    }
}
