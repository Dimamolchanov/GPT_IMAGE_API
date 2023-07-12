namespace GPT_IMAGE_API.Utils
{
	using System;
	using System.IO;
	using System.Net.Http;
	using System.Net.Http.Headers;
	using System.Threading.Tasks;

	public class OpenAIImageUploader
	{
		public async Task UploadImage(string imageFilePath)
		{
			using (var httpClient = new HttpClient())
			{
				try
				{
					using (MultipartFormDataContent formData = new MultipartFormDataContent())
					{
						ByteArrayContent fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(imageFilePath));
						fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/png"); // Replace with appropriate image content type if necessary

						formData.Add(fileContent, "image");

						httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "YOUR_API_KEY");
						var response = await httpClient.PostAsync("https://api.openai.com/v1/images/edits", formData);
						response.EnsureSuccessStatusCode();

						string responseBody = await response.Content.ReadAsStringAsync();
						Console.WriteLine("Image uploaded successfully.");
						Console.WriteLine("Response: " + responseBody);
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Error uploading the image: {ex.Message}");
				}
			}
		}
	}

	public class Program
	{
		public static async Task Main(string[] args)
		{
			string imageFilePath = @"C:\path\to\image.jpg"; // Replace with the actual path to your image file

			OpenAIImageUploader uploader = new OpenAIImageUploader();
			await uploader.UploadImage(imageFilePath);
		}
	}

}
