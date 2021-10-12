using Kitias.API.Models;
using Kitias.Persistence.Models;

namespace Kitias.API.Interfaces
{
	public interface ITokensService
	{
		JwtTokens GenerateTokens(User user);
	}
}
