using System;
using System.ComponentModel.DataAnnotations;

namespace Flor.Models
{
    public class Recenzija
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Ime može imati najviše 20 znakova.")]
        public string Ime { get; set; }

        [StringLength(100, ErrorMessage = "Recenzija može imati najviše 100 znakova.")]
        public string? Sadrzaj { get; set; }

        [Range(1, 5, ErrorMessage = "Ocjena mora biti između 1 i 5.")]
        public int Ocjena { get; set; }

        public DateTime Datum { get; set; } = DateTime.Now;

        public bool Odobrena { get; set; } = false;
    }
}
