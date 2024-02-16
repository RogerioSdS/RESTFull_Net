using RestWithASPNETUdemy.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNETUdemy.Model
{
    [Table("books")]//referenciando o nome da tabela que será usada no db
    public class Book : BaseEntity
    {        
        [Column("launch_date")]
        public DateTime Launch_Date { get; set; }
        [Column("author")]
        public string Author { get; set; }
        [Column("price")]
        public decimal Price { get; set; }
        [Column("title")]
        public string Title { get; set; }

        public Book()
        {
            Launch_Date = DateTime.Now;
        }
    }
}
