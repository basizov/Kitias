using Kitias.Auth.API.Models;
using Kitias.Persistence.Models;
using System.Threading.Tasks;

namespace Kitias.Auth.API.Interfaces
{
	public interface IUserService
	{
		Task<User> AuthenticateAsync(LoginModel loginModel);
		Task<User> CreateAsync(RegisterModel registerModel);
	}
}
