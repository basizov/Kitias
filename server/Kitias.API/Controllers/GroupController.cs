using Kitias.Persistence.DTOs;
using Kitias.Providers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Kitias.API.Controllers
{
	public class GroupController : BaseController
	{
		private readonly IGroupProvider _groupProvider;

		public GroupController(IGroupProvider groupProvider) => _groupProvider = groupProvider;

		[HttpGet]
		public ActionResult<IEnumerable<GroupDto>> TakeGroups() =>
			HandleResult(_groupProvider.TakeGroups());
	}
}
