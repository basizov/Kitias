﻿using Kitias.Persistence;
using Kitias.Persistence.Models;
using Kitias.Repository.Implementations.Base;
using Kitias.Repository.Interfaces;

namespace Kitias.Repository.Implementations
{
	public class TeacherRepository : Repository<Teacher>, ITeacherRepository
	{
		public TeacherRepository(DataContext context) : base(context) { }
	}
}