namespace Flor.Models
{
    public class CartItem
    {
        public int BuketId { get; set; }
        public string Naziv { get; set; } 
        public int Kolicina { get; set; }
        public string Velicina { get; set; } 
        public decimal Cijena { get; set; } 
    }
}
