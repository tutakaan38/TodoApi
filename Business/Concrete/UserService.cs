using Business.Abstract;
using Business.Dtos;
using Entities;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using BCrypt.Net; // BCrypt kütüphanesini kullanabilmek için ekledik

namespace Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly TodoAppContext _context;

        public UserService(TodoAppContext context)
        {
            _context = context;
        }

        public async Task<bool> Register(UserRegisterDto userDto)
        {
            // Kullanıcı adı kontrolü
            if (await _context.Users.AnyAsync(u => u.Username == userDto.Username))
                return false;

            // Şifreyi BCrypt ile güvenli bir şekilde hashliyoruz
            // HashPassword fonksiyonu otomatik olarak şifreye 'salt' ekler
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

            var user = new User
            {
                Username = userDto.Username,
                PasswordHash = hashedPassword // Artık veritabanına hashlenmiş şifre gidiyor
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> Login(string username, string password)
        {
            // Önce kullanıcıyı adına göre buluyoruz
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                return null;

            // Verilen düz şifreyi (password), veritabanındaki hashlenmiş şifreyle (user.PasswordHash) karşılaştırıyoruz
            // BCrypt.Verify bu işlemin güvenliğini sağlar
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

            if (!isPasswordValid)
                return null;

            return user;
        }
    }
}