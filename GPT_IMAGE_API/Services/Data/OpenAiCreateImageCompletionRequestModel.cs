using System.ComponentModel.DataAnnotations;

namespace GPT_IMAGE_API.Services
{
	public class OpenAiCreateImageCompletionRequestModel
	{
		
		[Required]
		public String Prompt { get; set; }
		public Int32 NumberOfImages { get; set; }//1-10
		public String Size { get; set; }//Must be one of 256x256, 512x512, or 1024x1024
	}
}
