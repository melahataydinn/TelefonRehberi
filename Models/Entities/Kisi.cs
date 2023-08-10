using System.ComponentModel.DataAnnotations.Schema;

namespace TelefonRehberi.Models.Entities
{
    [Table("Kisiler")]
    public class Kisi
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Numara { get; set; }
        public string Eposta { get; set; }

        public string Website { get; set; }
        public string Adres { get; set; }
 
        public string FotoYolu { get; set; }
    }
}
