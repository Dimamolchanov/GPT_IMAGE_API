namespace GPT_IMAGE_API.Business
{
	public interface EditCompletionDataProvider
	{
		public CompletionModel Get(EditImageCompletionRequestModel request);
	}
}
