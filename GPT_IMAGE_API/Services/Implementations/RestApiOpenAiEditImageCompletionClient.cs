using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace GPT_IMAGE_API.Services
{
	public class RestApiOpenAiEditImageCompletionClient : OpenAiEditImageCompletionClient
	{
		private readonly String apiKey;
		private const String url = "https://api.openai.com/v1/images/variations";//move to UrlUtils
		private String folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images/dogs");

		public RestApiOpenAiEditImageCompletionClient(String apiKey)
		{
			this.apiKey = apiKey;
		}

		public OpenAiCreateImageCompletionModel GetImageComplitionData(OpenAiEditImageCompletionRequestModel request)
		{

			HttpResponseMessage message = Send(new OpenAiEditImageCompletionRequestModel
			{
				NumberOfImages = request.NumberOfImages,
				Size = request.Size,
				FileName = request.FileName
			});

			if (!message.IsSuccessStatusCode)
			{
				throw new HttpRequestException($"HTTP request failed with status code: {message.StatusCode}");
			}

			ResponseData responseData = Deserialize(message);

			return MapResponseDatatoCompletionModel(responseData);
		}
		private HttpResponseMessage Send(OpenAiEditImageCompletionRequestModel data)
		{
			using HttpClient httpClient = new HttpClient();
			httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
			MultipartFormDataContent content = GetFormData(data).GetAwaiter().GetResult();
			Task<HttpResponseMessage> resultMessageTask = httpClient.PostAsync(url, content);
			return resultMessageTask.GetAwaiter().GetResult();
		}

		private async Task<MultipartFormDataContent> GetFormData(OpenAiEditImageCompletionRequestModel model)
		{
			MultipartFormDataContent formData = new MultipartFormDataContent();

			try 
			{
				ByteArrayContent fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(Path.Combine(folderPath, model.FileName)));
				fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/png");
				formData.Add(fileContent, "image");
			}
			catch (Exception ex)
			{
				//to do
			}

			formData.Add(new StringContent(model.NumberOfImages.ToString()), "n");
			formData.Add(new StringContent(model.Size), "size");
			return formData;
		}

		private OpenAiCreateImageCompletionModel MapResponseDatatoCompletionModel(ResponseData responseData)
		{
			List<String> data = new List<String>();
			foreach (Url url in responseData.Urls)
			{
				data.Add(url.Text);
			}
			return new OpenAiCreateImageCompletionModel { Urls = data };
		}

		private ResponseData Deserialize(HttpResponseMessage message)
		{
			if (message == null)
			{
				return null;
			}
			String responseContext = message.Content.ReadAsStringAsync().GetAwaiter().GetResult();
			ResponseData result = null;
			try
			{
				result = JsonConvert.DeserializeObject<ResponseData>(responseContext);
			}
			catch
			{
				return null;
			}
			return result;
		}

		private class ResponseData
		{
			[JsonProperty("created")]
			public String Created { get; set; }
			[JsonProperty("data")]
			public List<Url> Urls { get; set; }
		}

		private class Url
		{
			[JsonProperty("url")]
			public String Text { get; set; }
		}

	}
}
