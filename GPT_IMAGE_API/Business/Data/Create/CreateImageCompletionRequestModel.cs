namespace GPT_IMAGE_API.Business
{
	public class CreateImageCompletionRequestModel
	{
		public String Prompt { get; set; }
		public Int32 NumberOfImages { get; set; }
		public String Style { get; set; }//return with enum
	}
}
