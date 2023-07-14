using GPT_IMAGE_API.Business;
using Microsoft.AspNetCore.Mvc;

namespace GPT_IMAGE_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CreateImageController : ControllerBase
	{
		private readonly CreateCompletionDataProvider dataProvider;

		public CreateImageController(
			CreateCompletionDataProvider dataProvider)
		{
			this.dataProvider = dataProvider;
		}

		[HttpPost("Create")]
		public IEnumerable<String> Post([FromBody] CreateImageControllerRequestModel model)
		{
			CreateImageCompletionRequestModel createImageCompletionRequestModel = new CreateImageCompletionRequestModel() 
			{
				Prompt = model.Text,
				Style = model.Style
			};
			return dataProvider.Get(createImageCompletionRequestModel).Data.Select(x => x.Url);
		}
	}

	public class CreateImageControllerRequestModel 
	{
		public String Text { get; set; }
		public String Style { get; set; }
	}
}
