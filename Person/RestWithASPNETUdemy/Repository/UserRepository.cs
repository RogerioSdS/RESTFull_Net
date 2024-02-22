using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;
using System.Security.Cryptography;
using System.Text;

namespace RestWithASPNETUdemy.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MySQLContext _context;

        public UserRepository(MySQLContext context)
        {
            _context = context;
        }

        public User ValidateCredentials(UserVO user)
        {
            var pass = ComputerHash(user.Password, SHA256.Create());
            return _context.Users.FirstOrDefault(contains => (contains.UserName == user.UserName) && (contains.Password == pass));
        }

        private string ComputerHash(string input, HashAlgorithm hashAlgorithm)
        {
            Byte[] hashedBytes = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            var sBuilder = new StringBuilder();

            foreach (var item in hashedBytes)
            {
                sBuilder.Append(item.ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
