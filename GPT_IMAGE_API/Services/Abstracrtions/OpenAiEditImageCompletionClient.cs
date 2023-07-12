namespace GPT_IMAGE_API.Services
{
	public interface OpenAiEditImageCompletionClient
	{
		OpenAiCreateImageCompletionModel GetImageComplitionData(OpenAiEditImageCompletionRequestModel request);
	}
}
