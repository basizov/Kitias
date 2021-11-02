using Kitias.Providers.Models;
using Kitias.Providers.Models.Request;
using System.Threading.Tasks;

namespace Kitias.Providers.Interfaces
{
	public interface IAuthProvider
	{
		Task<Result<string>> SignUpAsync(SignUpRequestModel model);
	}
}
