using Kitias.Providers.Models;
using System.Threading.Tasks;

namespace Kitias.Providers.Interfaces
{
	public interface IAuthProvider
	{
		Task<Result<string>> SignUpAsync(SignUpRequestModel model);
	}
}
