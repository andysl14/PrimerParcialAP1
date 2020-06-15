using Microsoft.EntityFrameworkCore;
using PrimerParcial.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimerParcial.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Articulos> Articulos { get; set; }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source= DATA\PrimerParcial.db");
        }
    }
}
    
