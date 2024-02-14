using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Repository;

namespace RestWithASPNETUdemy.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {
        private readonly IBookRepository _repository;

        public BookBusinessImplementation(IBookRepository repository)
        {
            _repository = repository;
        }

        // Method responsible for returning all people,
        public List<Book> FindAll()
        {
            return _repository.FindAll();
        }

        // Method responsible for returning one person by ID
        public Book FindById(long id)
        {
            return _repository.FindById(id);
        }

        // Method responsible to crete one new person
        public Book Create(Book book)
        {
            return _repository.Create(book);
        }

        // Method responsible for updating one person
        public Book Update(Book book)
        {
            return _repository.Update(book);
        }

        // Method responsible for deleting a person from an ID
        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}