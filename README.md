# AI ChatBox

AI ChatBox is a simple console-based AI chatbot developed in C# using ASP.NET Core and the Google Gemini API.

The project was created as a learning project to practice REST API communication, HTTP/HTTPS requests, JSON handling, and integration with an external AI service.

## Features

- Console-based chat interface
- ASP.NET Core Web API backend
- Communication using HTTP/HTTPS
- JSON request and response handling
- Integration with Google Gemini API
- Croatian-language AI responses
- Secure API key storage using .NET User Secrets
- Simple `exit` command for closing the application

## Technologies

- C#
- .NET 8
- ASP.NET Core Web API
- REST API
- HTTP/HTTPS
- JSON
- Google Gemini API
- Visual Studio 2022
- Git
- GitHub

## Project Structure

The solution consists of two projects:

```text
AI ChatBox
│
├── AI ChatBox
│   └── ASP.NET Core Web API
│       └── Handles communication with the Gemini API
│
└── AIChatBox.Console
    └── Console application
        └── Sends user messages to the Web API
```

## Communication Flow

```text
User
  ↓
Console Application
  ↓
HTTP POST /api/Chat
  ↓
ASP.NET Core Web API
  ↓
Gemini API
  ↓
ASP.NET Core Web API
  ↓
Console Application
  ↓
AI Response
```

## Getting Started

### Requirements

Before running the application, make sure you have:

- .NET 8 SDK
- Visual Studio 2022 or another compatible IDE
- Google Gemini API key

### 1. Clone the Repository

Clone the GitHub repository:

```bash
git clone https://github.com/domagoj900/AI-ChatBox.git
```

Navigate to the project folder.

### 2. Configure the Gemini API Key

The Gemini API key is not stored directly inside the source code.

Navigate to the ASP.NET Core Web API project directory and run:

```bash
dotnet user-secrets set "Gemini:ApiKey" "YOUR_API_KEY"
```

Replace:

```text
YOUR_API_KEY
```

with your own Gemini API key.

You can verify that the API key was successfully stored by running:

```bash
dotnet user-secrets list
```

You should see:

```text
Gemini:ApiKey = YOUR_API_KEY
```

> Never store or commit API keys, passwords, or other credentials directly inside a Git repository.

### 3. Run the Application

Both projects need to be running at the same time:

- `AI ChatBox` — ASP.NET Core Web API
- `AIChatBox.Console` — Console client

In Visual Studio:

1. Right-click the solution.
2. Select `Configure Startup Projects`.
3. Select `Multiple startup projects`.
4. Set both projects to `Start`.
5. Run the solution.

The Web API runs in the background while the console application provides the user interface.

The console application communicates with the local API endpoint:

```text
https://localhost:7265/api/Chat
```

The port may be different depending on the local development environment.

If necessary, update the API URL inside the console application's `Program.cs`.

## Example

Example conversation:

```text
=================================
          AI ChatBox
=================================
Upišite 'exit' za izlaz.

Upit: Koliko je 7 * 8?

AI: 7 * 8 = 56.

Upit: Objasni što je REST API.

AI: REST API je način komunikacije između aplikacija putem HTTP zahtjeva.
Omogućuje klijentu slanje zahtjeva prema serveru i primanje odgovora.

Upit: exit

Chat završen. Doviđenja!
```

## API Communication

The console application sends a POST request to:

```text
POST /api/Chat
```

Example JSON request:

```json
{
  "message": "Objasni što je REST API."
}
```

Example JSON response:

```json
{
  "reply": "REST API je način komunikacije između aplikacija putem HTTP zahtjeva."
}
```

The ASP.NET Core Web API then forwards the user's message to the Gemini API and returns the generated response to the console application.

## Security

The Gemini API key is stored using .NET User Secrets.

This prevents the API key from being stored directly inside the source code or committed to GitHub.

The project accesses the key using the ASP.NET Core configuration system.

Example:

```csharp
var apiKey = _configuration["Gemini:ApiKey"];
```

## What I Learned

Through this project I practiced:

- Creating a C# console application
- Creating REST API endpoints with ASP.NET Core
- Working with controllers
- Sending asynchronous HTTP requests
- Working with HTTP POST requests
- Sending and receiving JSON data
- Serializing and deserializing JSON
- Using `HttpClient`
- Integrating an external AI API
- Working with the Google Gemini API
- Separating client and backend responsibilities
- Securely storing API credentials
- Using .NET User Secrets
- Handling API responses and errors
- Using Git and GitHub for version control

## Future Improvements

Possible future improvements include:

- Conversation history and context
- Graphical web interface
- Better error handling
- Loading indicators
- Configurable AI models
- Logging
- Automated tests
- Database integration
- User authentication
- Cloud deployment
- Docker support

## Author

Developed as a personal learning project to practice C#, ASP.NET Core, REST APIs, HTTP/HTTPS communication, JSON handling, Git/GitHub, and AI API integration.
