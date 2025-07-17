using System.ComponentModel.DataAnnotations;

namespace Flor.Models
{
    public class Buket
    {
        public int Id { get; set; }

        [Required]
        public string Naziv { get; set; }

        [Required]
        public string Opis { get; set; }

        public string SlikaUrl { get; set; }

        [Display(Name = "Cijena za mali buket")]
        public decimal CijenaMali { get; set; }

        [Display(Name = "Cijena za srednji buket")]
        public decimal CijenaSrednji { get; set; }

        [Display(Name = "Cijena za veliki buket")]
        public decimal CijenaVeliki { get; set; }
    }
}
