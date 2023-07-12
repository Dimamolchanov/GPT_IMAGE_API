using System.Text;
using Newtonsoft.Json;

namespace GPT_IMAGE_API.Services
{
	public class RestApiOpenAiCreateImageCompletionClient : OpenAiCreateImageCompletionClient
	{
		private readonly String apiKey;
		private const String url = "https://api.openai.com/v1/images/generations";//move to UrlUtils

		public RestApiOpenAiCreateImageCompletionClient(String apiKey)
		{
			this.apiKey = apiKey;
		}

		public OpenAiCreateImageCompletionModel GetImageComplitionData(OpenAiCreateImageCompletionRequestModel request)
		{
			HttpResponseMessage message = Send(new RequestData
			{
				Prompt = request.Prompt,
				NumberOfcompletions = request.NumberOfImages,
				Size = request.Size,
			});

			if (!message.IsSuccessStatusCode)
			{
				throw new HttpRequestException($"HTTP request failed with status code: {message.StatusCode}");
			}

			ResponseData responseData = Deserialize(message);
			
			return MapResponseDatatoCompletionModel(responseData);
		}
		private HttpResponseMessage Send(RequestData data)
		{
			using HttpClient httpClient = new HttpClient();
			httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
			Task<HttpResponseMessage> resultMessageTask = httpClient.PostAsync(url, Serialize(data));
			return resultMessageTask.GetAwaiter().GetResult();
		}

		private OpenAiCreateImageCompletionModel MapResponseDatatoCompletionModel(ResponseData responseData)
		{
			List<String> data = new List<String>();
			foreach (Url url in responseData.Urls)
			{
				data.Add( url.Text );
			}
			return new OpenAiCreateImageCompletionModel { Urls = data };
		}

		private HttpContent Serialize(RequestData data)
		{
			if (data != null)
			{
				return new StringContent(content: JsonConvert.SerializeObject(data), encoding: Encoding.UTF8, mediaType: "application/json");
			}
			throw new ArgumentException("No parameters to create a request body");
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

		private class RequestData
		{
			[JsonProperty("prompt")]
			public String Prompt { get; set; }

			[JsonProperty("n")]
			public Int32 NumberOfcompletions { get; set; }
			[JsonProperty("size")]
			public String Size { get; set; }
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
