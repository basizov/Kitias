using Kitias.Repository.Interfaces.Base;
using Microsoft.AspNetCore.Identity;
using System;

namespace Kitias.Repository.Interfaces
{
	public interface IUserRoleRepository : IRepository<IdentityUserRole<Guid>> { }
}
