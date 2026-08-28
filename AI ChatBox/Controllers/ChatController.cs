using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Text.Json;

namespace AI_ChatBox.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ChatController : ControllerBase
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IConfiguration _configuration;

		public ChatController(
			IHttpClientFactory httpClientFactory,
			IConfiguration configuration)
		{
			_httpClientFactory = httpClientFactory;
			_configuration = configuration;
		}

		[HttpPost]
		public async Task<IActionResult> SendMessage(ChatRequest request)
		{
			if (string.IsNullOrWhiteSpace(request.Message))
			{
				return BadRequest("Poruka ne smije biti prazna.");
			}

			var apiKey = _configuration["Gemini:ApiKey"];

			if (string.IsNullOrWhiteSpace(apiKey))
			{
				return StatusCode(500, "Gemini API key nije pronađen.");
			}

			var client = _httpClientFactory.CreateClient();

			var url =
				"https://generativelanguage.googleapis.com/v1beta/models/gemini-3.6-flash:generateContent";

			var payload = new
			{
				system_instruction = new
				{
					parts = new[]
					{
			new
			{
				text = """
                Odgovaraj isključivo na hrvatskom standardnom jeziku.

                Koristi standardni hrvatski jezik i hrvatske izraze.
                Nemoj koristiti srpske ili bosanske jezične varijante ako postoji
                standardni hrvatski izraz.

                Primjeri:
                rješavati, ne rešavati
                točno, ne tačno
                računalo, ne računar
                uputa, ne instrukcija kada je prirodniji hrvatski izraz

                Odgovori trebaju biti jasni, prirodni i gramatički ispravni.
                Nemoj koristiti LaTeX niti znakove $ oko matematičkih izraza.
                Za matematičke izraze koristi običan tekst, primjerice:
                8 - 1 = 7
                7 × 7 = 49
                """
			}
		}
				},

				contents = new[]
				{
		new
		{
			parts = new[]
			{
				new
				{
					text = request.Message
				}
			}
		}
	}
			};

			using var httpRequest =
				new HttpRequestMessage(HttpMethod.Post, url);

			httpRequest.Headers.Add("x-goog-api-key", apiKey);
			httpRequest.Content = JsonContent.Create(payload);

			var response = await client.SendAsync(httpRequest);

			var responseBody =
				await response.Content.ReadAsStringAsync();

			if (!response.IsSuccessStatusCode)
			{
				return StatusCode(
					(int)response.StatusCode,
					responseBody);
			}

			using var json =
				JsonDocument.Parse(responseBody);

			var reply = json.RootElement
				.GetProperty("candidates")[0]
				.GetProperty("content")
				.GetProperty("parts")[0]
				.GetProperty("text")
				.GetString();

			return Ok(new
			{
				reply = reply
			});
		}
	}

	public class ChatRequest
	{
		public string Message { get; set; } = string.Empty;
	}
}