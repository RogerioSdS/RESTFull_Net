using RestWithASPNETUdemy.Hypermedia;
using RestWithASPNETUdemy.Hypermedia.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNETUdemy.Data.VO
{
    public class BookVO : ISupportsHyperMedia
    {
        public long Id { get; set; }
        public DateTime Launch_Date { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}

