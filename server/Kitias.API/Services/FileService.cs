using Kitias.Providers.Models.File;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Kitias.API.Services
{
	/// <summary>
	/// Service to work with files
	/// </summary>
	public class FileService
	{
		private readonly IWebHostEnvironment _hostEnviroment;

		/// <summary>
		/// Constructor to get services from di
		/// </summary>
		/// <param name="hostEnviroment">Enviroment to get assembly info</param>
		public FileService(IWebHostEnvironment hostEnviroment) => _hostEnviroment = hostEnviroment;

		/// <summary>
		/// Save files in project
		/// </summary>
		/// <param name="files">Files to save</param>
		/// <param name="userEmail">Email to create user directoryinfo</param>
		/// <returns>File Names</returns>
		public async Task<IEnumerable<CreateFileModel>> SaveFilesAsync(IEnumerable<IFormFile> files, string userEmail)
		{
			var result = new List<CreateFileModel>();
			var currentDate = DateTime.UtcNow.ToString("dd.MM.yyyy");
			var fileRootPath = Path.Combine(
				_hostEnviroment.ContentRootPath, "Files", userEmail, currentDate
			);

			if (!Directory.Exists(fileRootPath))
				Directory.CreateDirectory(fileRootPath);
			foreach (var file in files)
			{
				var fileName = Path.GetFileNameWithoutExtension(file.FileName)
					.Replace(' ', '-');
				var fileExtension = Path.GetExtension(file.FileName);
				var fullFileName = Path.Combine(fileName, fileExtension);
				var savePath = Path.Combine(fileRootPath, fullFileName);
				var fileLenght = file.Length;
				using var fileStream = new FileStream(savePath, FileMode.Create);

				await file.CopyToAsync(fileStream);
				result.Add(new()
				{
					Date = currentDate,
					Extension = fileExtension,
					Name = fileName,
					OwnerEmail = userEmail,
					Path = savePath,
					Size = fileLenght
				});
			}
			return result;
		}
	}
}
