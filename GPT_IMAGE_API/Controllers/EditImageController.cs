using GPT_IMAGE_API.Business;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GPT_IMAGE_API.Controllers
{
	[Route("api/[controller]/")]
	[ApiController]
	public class EditImageController : ControllerBase
	{
		private readonly EditCompletionDataProvider dataProvider;

		public EditImageController(
			EditCompletionDataProvider dataProvider)
		{
			this.dataProvider = dataProvider;
		}

		[HttpPost("Edit")]
		public IEnumerable<String> Post([FromBody] EditImageControllerRequestModel model)
		{
			EditImageCompletionRequestModel createImageCompletionRequestModel = new EditImageCompletionRequestModel()
			{
				FileName = model.FileName
			};
			return dataProvider.Get(createImageCompletionRequestModel).Data.Select(x => x.FileName);
		}
	}

	public class EditImageControllerRequestModel
	{
		public String FileName { get; set; }
	}
}
