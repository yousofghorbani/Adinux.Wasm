using Adinux.Wasm.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TL;

namespace Adinux.Wasm.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBotController : ControllerBase
    {
		private readonly WTelegramService WT;
		//public UserBotController(WTelegramService wt) => WT = wt;

		[HttpGet("status")]
		public async Task<ContentResult> Status()
		{
			var config = await WT.ConfigNeeded();
			if (config != null)
				return Content($@"Enter {config}: <form action=""config""><input name=""value"" autofocus/></form>", "text/html");
			else
				return Content($@"Connected as {WT.User}<br/><a href=""chats"">Get all chats</a>", "text/html");
		}

		[HttpGet("config")]
		public ActionResult Config(string value)
		{
			WT.ReplyConfig(value);
			return Redirect("status");
		}

		[HttpGet("chats")]
		public async Task<object> Chats()
		{
			if (WT.User == null) throw new Exception("Complete the login first");
			//var chats = await WT._client.Messages_GetAllChats(null);
			//return chats.chats;
			return "chats";
		}
	}
}
