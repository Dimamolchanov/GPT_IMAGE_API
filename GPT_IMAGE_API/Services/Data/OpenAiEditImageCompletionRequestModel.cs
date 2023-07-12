using System.ComponentModel.DataAnnotations;

namespace GPT_IMAGE_API.Services
{
	public class OpenAiEditImageCompletionRequestModel
	{
		[Required]
		public String FileName { get; set; }
		public Int32 NumberOfImages { get; set; }//1-10
		public String Size { get; set; }//Must be one of 256x256, 512x512, or 1024x1024
	}
}
