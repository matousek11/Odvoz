using SQLite;
using System;

namespace Odvoz.Models
{
    public class Predmet
    {
        [PrimaryKey]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Jmeno { get; set; }
        public string Zul { get; set; }
        public string Polotovar { get; set; }
        public string Vykres { get; set; }
        public int Ks { get; set; }
        public string SearchId { get; set; }
    }
}
