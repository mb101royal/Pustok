using nov30task.Hellpers;

namespace nov30task.Helpers
{
	public static class FileExtension
	{
		public static bool IsValidSize(this IFormFile file, float kb = 1500)
			=> file.Length <= kb * 1024;

		public static bool IsCorrectType(this IFormFile file, string contentType = "image")
			=> file.ContentType.Contains(contentType);

		public static async Task<string> SaveAsync(this IFormFile file, string path)
		{
			string extension = Path.GetExtension(file.FileName);
			
			string fileName = Path.GetFileNameWithoutExtension(file.FileName).Length > 129 ?
				file.FileName.Substring(0,129) :
				Path.GetFileNameWithoutExtension(file.FileName);
			
			fileName = Path.Combine(path, Path.GetRandomFileName() + fileName + extension);

			using FileStream fileStream = File.Create(Path.Combine(PathConstants.RootPath, fileName));
			
			await file.CopyToAsync(fileStream);

			return fileName;
		}
	}
}
