using RestWithASPNETUdemy.Model;

namespace RestWithASPNETUdemy.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private volatile int count;

        public Person Create(Person person)
        {
            //Logica do Acesso ao DB
            return person;
        }

        public void Delete(long person)
        {
            //Logica de negocio para Delete
        }


            public List<Person> FindAll()
        {
            List<Person> persons = new List<Person>();
            for (int i = 0; i < 8; i++)
            {
                Person person = MockPerson(i);
                persons.Add(person);
            }
            return persons;
        }


        public Person FindById(long id)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "Deus",
                LastName = "Seja louvado",
                Address = "Ceus",
                Gender = "o Todo Poderoso"
            };
        }

        public Person Update(Person person)
        {
            return person;
        }

        private Person MockPerson(int i)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "Deus",
                LastName = "Seja louvado",
                Address = "Ceus",
                Gender = "o Todo Poderoso"
            };
        }

        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }
        
    }
}
