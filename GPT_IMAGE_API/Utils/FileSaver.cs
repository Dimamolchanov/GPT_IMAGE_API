using System.Net;

namespace GPT_IMAGE_API.Utils
{
	public class FileSaver
	{
		private String folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images/dogs");
		public async Task Save(String url, String fileName) 
		{
			using (HttpClient httpClient = new HttpClient())
			{
				try
				{
					Byte[] fileBytes = await httpClient.GetByteArrayAsync(url);
					EnsureDirectory();
					await File.WriteAllBytesAsync(Path.Combine(folderPath, fileName), fileBytes);
				}
				catch (Exception ex)
				{
					throw new IOException($"Error saving the file: {ex.Message}");
				}
			}
		}

		private void EnsureDirectory() 
		{
			
			if (!Directory.Exists(folderPath)) 
			{
				Directory.CreateDirectory(folderPath);
			}
		}
	}
}
