using Kitias.Auth.API.Models;
using Kitias.Persistence.Models;

namespace Kitias.Auth.API.Interfaces
{
	public interface ITokensService
	{
		JwtTokens GenerateTokens(User user);
	}
}
