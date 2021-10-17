using Kitias.Persistence;
using Kitias.Repository.Implementations.Base;
using Kitias.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;

namespace Kitias.Repository.Implementations
{
	public class UserRoleRepository : Repository<IdentityUserRole<Guid>>, IUserRoleRepository
	{
		public UserRoleRepository(DataContext context) : base(context) { }
	}
}
