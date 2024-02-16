using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNETUdemy.Model.Base
{
    public class BaseEntity
    {
        [Column("id")]//referenciando o nome da coluna que será usada no db
        public long Id { get; set; }
       
    }
}
