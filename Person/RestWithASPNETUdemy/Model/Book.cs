using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNETUdemy.Model
{
    [Table("books")]//referenciando o nome da tabela que será usada no db
    public class Book
    {
        [Column("id")]//referenciando o nome da coluna que será usada no db
        public long Id { get; set; }
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
