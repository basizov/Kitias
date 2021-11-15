using Kitias.Persistence.Contexts;
using Kitias.Persistence.Entities.Scheduler.Attendence;
using Kitias.Persistence.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitias.Persistence.Seed
{
	/// <summary>
	/// Seed postgres context
	/// </summary>
	public static class SeedDataContext
	{
		public static async Task SeedAttendances(DataContext context)
		{
			if (context.Attendances.Any())
				return;
			var listAttendances = new List<Attendance>();

			for (var i = 0; i < 32; ++i)
			{
				var subjectId = context.Subjects.First().Id;

				if (i > 23)
					subjectId = context.Subjects.Skip(1).First().Id;
				else if (i > 15)
					subjectId = context.Subjects.Skip(1).First().Id;
				var newAttendanceFst = new Attendance
				{
					Attended = i % 2 == 0 ? i % 4 == 0 ? AttendaceVariants.WasNotByReason : AttendaceVariants.Was : i % 3 == 0 ? AttendaceVariants.WasIll : AttendaceVariants.WasNot,
					Date = DateTime.Now.AddDays(i),
					Score = (byte)(i / 30 * 100),
					StudentId = context.Students.First().Id,
					SubjectId = subjectId,
					Theme = $"Test {i + 1}"
				};
				var newAttendanceSec = new Attendance
				{
					Attended = i % 2 == 1 ? i % 4 == 1 ? AttendaceVariants.WasNotByReason : AttendaceVariants.Was : i % 3 == 1 ? AttendaceVariants.WasIll : AttendaceVariants.WasNot,
					Date = DateTime.Now.AddDays(i),
					Score = (byte)(i / 30 * 100),
					StudentId = context.Students.Skip(1).First().Id,
					SubjectId = subjectId,
					Theme = $"Test {i + 1}"
				};

				context.Attendances.AddRange(newAttendanceFst, newAttendanceSec);
				listAttendances.Add(newAttendanceFst);
				listAttendances.Add(newAttendanceSec);
			}
			var listStudentAttendances = new List<StudentAttendance>();

			for (var i = 0; i < 5; ++i)
			{
				var newStudentAttendance = new StudentAttendance()
				{
					Attendances = listAttendances,
					Grade = i < 3 ? Grade.Good : Grade.Excellent,
					Raiting = (byte)(i / 5 * 100),
					StudentId = context.Students.First().Id,
					TeacherId = context.Teachers.First().Id
				};

				context.StudentAttendances.Add(newStudentAttendance);
				listStudentAttendances.Add(newStudentAttendance);
			}

			var sheduler = new AttendanceSheduler()
			{
				StudentAttendances = listStudentAttendances,
				TeacherId = context.Teachers.First().Id,
				GroupId = context.Groups.First().Id
			};

			context.AttendanceShedulers.Add(sheduler);
			await context.SaveChangesAsync();
		}
	}
}
