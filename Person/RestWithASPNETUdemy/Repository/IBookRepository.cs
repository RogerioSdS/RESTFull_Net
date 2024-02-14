using RestWithASPNETUdemy.Model;

namespace RestWithASPNETUdemy.Repository
{
    public interface IBookRepository
    {
        Book Create(Book book);
        Book FindById(long id);
        List<Book> FindAll();
        Book Update(Book book);
        void Delete(long book);
        bool Exists(long id);

    }
}
