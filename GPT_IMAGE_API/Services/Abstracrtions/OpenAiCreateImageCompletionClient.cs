namespace GPT_IMAGE_API.Services
{
	public interface OpenAiCreateImageCompletionClient
	{
		OpenAiCreateImageCompletionModel GetImageComplitionData(OpenAiCreateImageCompletionRequestModel request);
	}
}
