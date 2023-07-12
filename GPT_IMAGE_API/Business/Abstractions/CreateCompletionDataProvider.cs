namespace GPT_IMAGE_API.Business
{
	public interface CreateCompletionDataProvider
	{
		public CompletionModel Get(CreateImageCompletionRequestModel request);
	}
}
