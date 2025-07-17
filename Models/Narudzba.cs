using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Flor.Models; 

namespace Flor.Models
{
    public class Narudzba
    {
        public int Id { get; set; }

        [Display(Name = "Ime i prezime")]
        [Required]
        public string ImePrezime { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Broj mobitela")]
        [Phone]
        [Required]
        public string BrojMobitela { get; set; }

        [Display(Name = "Adresa dostave")]
        public string? Adresa { get; set; }

        [Display(Name = "Datum isporuke")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DatumIsporuke { get; set; }

        [Display(Name = "Napomena")]
        public string? Napomena { get; set; }

        [Display(Name = "Način dostave")]
        [Required]
        public string NacinDostave { get; set; }

        public decimal UkupnaCijena { get; set; }

        public List<NarudzbaStavka>? Stavke { get; set; }

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public bool Obavljeno { get; set; } = false;

    }
}
