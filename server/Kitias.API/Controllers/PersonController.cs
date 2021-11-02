using Microsoft.Extensions.Logging;

namespace Kitias.API.Controllers
{
	/// <summary>
	/// Controller to work with persons
	/// </summary>
	public class PersonController : BaseController
	{
		/// <summary>
		/// Add neccessary services
		/// </summary>
		/// <param name="logger">Logging</param>
		public PersonController(ILogger<PersonController> logger) : base(logger) { }
	}
}
