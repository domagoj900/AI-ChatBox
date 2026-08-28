using System.Net.Http.Json;

var client = new HttpClient();

Console.CancelKeyPress += (sender, e) =>
{
	e.Cancel = false;
};

var apiUrl = "https://localhost:7265/api/Chat";

Console.WriteLine("=================================");
Console.WriteLine("          AI ChatBox");
Console.WriteLine("=================================");
Console.WriteLine("Upišite 'exit' za izlaz.");
Console.WriteLine();

while (true)
{
	Console.Write("Upit: ");

	var message = Console.ReadLine();

	if (string.IsNullOrWhiteSpace(message))
		continue;

	if (message.Equals("exit", StringComparison.OrdinalIgnoreCase))
		break;

	try
	{
		var request = new
		{
			message = message
		};

		var response = await client.PostAsJsonAsync(apiUrl, request);

		if (!response.IsSuccessStatusCode)
		{
			Console.WriteLine();
			Console.WriteLine($"Greška: {response.StatusCode}");
			Console.WriteLine();
			continue;
		}

		var result = await response.Content.ReadFromJsonAsync<ChatResponse>();

		Console.WriteLine();
		Console.WriteLine($"AI: {result?.Reply}");
		Console.WriteLine();
	}
	catch (Exception ex)
	{
		Console.WriteLine();
		Console.WriteLine($"Greška pri povezivanju: {ex.Message}");
		Console.WriteLine();
	}
}

Console.WriteLine();
Console.WriteLine("Chat završen. Doviđenja!");

public class ChatResponse
{
	public string Reply { get; set; } = string.Empty;
}