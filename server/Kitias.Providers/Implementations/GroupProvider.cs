using AutoMapper;
using Kitias.Persistence.DTOs;
using Kitias.Persistence.Entities;
using Kitias.Providers.Interfaces;
using Kitias.Providers.Models;
using Kitias.Providers.Models.Group;
using Kitias.Repository.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kitias.Providers.Implementations
{
	/// <summary>
	/// Provider to work with group entity
	/// </summary>
	public class GroupProvider : Provider, IGroupProvider
	{
		/// <summary>
		/// Configure all necessary services
		/// </summary>
		/// <param name="mapper">Mapper</param>
		/// <param name="logger">Logging</param>
		/// <param name="unitOfWork">Working with different dbs</param>
		public GroupProvider(IMapper mapper, ILogger<GroupProvider> logger, IUnitOfWork unitOfWork) : base(mapper, logger, unitOfWork) { }

		public Result<IEnumerable<GroupDto>> TakeGroups()
		{
			try
			{
				var groups = _unitOfWork.Group.GetAll();
				var result = _mapper.Map<IEnumerable<GroupDto>>(groups);

				
				return ResultHandler.OnSuccess(result);
			}
			catch (Exception ex)
			{
				return ResultHandler.OnFailure<IEnumerable<GroupDto>>(ex.Message);
			}
		}

		public async Task<Result<GroupDto>> TakeGroupByIdAsync(Guid id)
		{
			try
			{
				var group = await _unitOfWork.Group
					.FindBy(g => g.Id == id)
					.SingleOrDefaultAsync();

				if (group == null)
					return ReturnFailureResult<GroupDto>($"Group with id ${id} doesn't existed", "Couldn't find this group");
				var result = _mapper.Map<GroupDto>(group);

				return ResultHandler.OnSuccess(result);
			}
			catch (Exception ex)
			{
				return ReturnFailureResult<GroupDto>(ex.Message);
			}
		}

		public async Task<Result<GroupDto>> CreateGroupAsync(CreateGroupModel group)
		{
			if (await _unitOfWork.Group.AnyAsync(g => g.Number == group.Number))
				return ReturnFailureResult<GroupDto>("Group with same number is existed");
			try
			{
				var groupEntity = _mapper.Map<Group>(group);
				var newGroup = _unitOfWork.Group.Create(groupEntity);
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					return ReturnFailureResult<GroupDto>("Couldn't save new group");
				var result = _mapper.Map<GroupDto>(newGroup);

				_logger.LogInformation($"Group with id {newGroup.Id} was successfully created");
				return ResultHandler.OnSuccess(result);
			}
			catch (Exception ex)
			{
				return ReturnFailureResult<GroupDto>(ex.Message, "Error group data");
			}
		}

		public async Task<Result<GroupDto>> UpdateGroupAsync(Guid id, UpdateGroupModel group)
		{
			var findGroup = await _unitOfWork.Group
				.FindBy(g => g.Id == id)
				.SingleOrDefaultAsync();

			if (findGroup == null)
				return ReturnFailureResult<GroupDto>($"Group with id ${id} doesn't existed", "Couldn't find this group");
			try
			{
				findGroup.Course = group.Course ?? findGroup.Course;
				findGroup.Number = group.Number ?? findGroup.Number;
				var updateGroup = _unitOfWork.Group.Update(findGroup);

				if (group.StudentsIds != null)
				{
					foreach (var studentId in group.StudentsIds)
					{
						var findStudent = await _unitOfWork.Student
							.FindBy(s => s.Id == studentId)
							.SingleOrDefaultAsync();

						if (findStudent == null)
							return ReturnFailureResult<GroupDto>($"Student with id ${id} doesn't existed", "Couldn't find student");
						findStudent.GroupId = updateGroup.Id;
					}
				}
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					return ReturnFailureResult<GroupDto>($"Couldn;t update group with id ${id}", "Couldn't update group");
				var result = _mapper.Map<GroupDto>(updateGroup);

				_logger.LogInformation($"Group with id {id} was successfully updated");
				return ResultHandler.OnSuccess(result);
			}
			catch (Exception ex)
			{
				return ReturnFailureResult<GroupDto>(ex.Message, "Error group data");
			}
		}

		public async Task<Result<string>> DeleteGroupAsync(Guid id)
		{
			var findGroup = await _unitOfWork.Group
				.FindBy(g => g.Id == id)
				.SingleOrDefaultAsync();

			if (findGroup == null)
				return ReturnFailureResult<string>($"Group with id ${id} doesn't existed", "Couldn't find this group");
			try
			{
				 _unitOfWork.Group.Delete(findGroup);
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					return ReturnFailureResult<string>($"Couldn't deletes group with id ${id}", "Couldn't delete group");
				_logger.LogInformation($"Group with id {id} was successfully deleted");
				return ResultHandler.OnSuccess("Group was successfully deleted");
			}
			catch (Exception ex)
			{
				return ReturnFailureResult<string>(ex.Message, "Error group data");
			}
		}

		private Result<T> ReturnFailureResult<T>(string loggerMessage, string errorMessage = null)
			where T : class
		{
			_logger.LogError(loggerMessage);
			return ResultHandler.OnFailure<T>(errorMessage ?? loggerMessage);
		}
	}
}
