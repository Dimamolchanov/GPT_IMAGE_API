using GPT_IMAGE_API.Services;
using GPT_IMAGE_API.Utils;

namespace GPT_IMAGE_API.Business
{
	public class EditImageCompletionDataProvider : EditCompletionDataProvider
	{
		private OpenAiEditImageCompletionClient client;
		private FileSaver fileSaver;

		public EditImageCompletionDataProvider(OpenAiEditImageCompletionClient client, FileSaver fileSaver)
		{
			this.client = client;
			this.fileSaver = fileSaver;
		}

		public CompletionModel Get(EditImageCompletionRequestModel request)
		{

			OpenAiEditImageCompletionRequestModel model = new OpenAiEditImageCompletionRequestModel()
			{
				NumberOfImages = 2,
				Size = "1024x1024",
				FileName = request.FileName
			};
			OpenAiCreateImageCompletionModel aiModel = client.GetImageComplitionData(model);
			Int32 count = 67;
			List<Completion> data = new List<Completion>();
			foreach (String url in aiModel.Urls)
			{
				String fileName = $"dog{count}.png";
				SaveFile(url, fileName).GetAwaiter().GetResult();
				data.Add(new Completion { FileName = fileName });
				count++;
			}
			return new CompletionModel { Data = data };
		}

		private async Task SaveFile(String url, String fileName)
		{
			await fileSaver.Save(url, fileName);
		}
	}
}
