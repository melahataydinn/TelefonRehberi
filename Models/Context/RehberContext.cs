using System.Collections.Generic;
using System.Data.Entity;
using TelefonRehberi.Models.Entities;

namespace TelefonRehberi.Models.Context
{
    public class RehberContext:DbContext
    {
     
            public RehberContext() : base("Server=MELAHAT\\SQLEXPRESS;Database=RehberDB;Trusted_Connection=true") //Veri tabanına şifresiz bağlanmak için
        {

        }
        public DbSet<Kisi> Kisiler { get; set; }
    }
}
