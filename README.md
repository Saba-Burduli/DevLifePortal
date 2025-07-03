


# 📄 Code Roast Project – DevLife Portal

The **Code Roast** mini-project is part of the DevLife Portal. It's a humorous AI-powered tool where developers submit code snippets and get brutally honest feedback from a sarcastic AI mentor. Ideal for fun, motivation, and creative development learning.

---


## 🧱 Architecture Overview
This project follows **Clean Architecture** and is structured as a **modular monolith** with four layers:

- **API Layer**: Endpoint routing, SignalR Hubs, Filters
- **Application Layer**: Commands, Queries, Handlers (CQRS, MediatR)
- **Domain Layer**: Entities, Interfaces, Custom Exceptions
- **Infrastructure Layer**: External services (OpenAI, MongoDB, SignalR Hubs)

---

## 🚀 Features

- 🔥 **AI Integration**: Roasts code snippets using OpenAI's GPT-4
- 🧠 **Caching**: Uses MongoDB to avoid repeated API calls for same code
- 📡 **SignalR**: Sends live roast messages to connected WebSocket clients
- 🧼 **Clean Layering**: Fully decoupled and extensible architecture

---

## ⚙️ Tech Stack

| Component       | Technology              |
|----------------|--------------------------|
| Language        | C# / .NET 9             |
| API Framework   | ASP.NET Core Minimal API|
| AI Engine       | OpenAI GPT-4            |
| Caching DB      | MongoDB                 |
| Real-time Layer | SignalR (WebSocket)     |
| Messaging       | MediatR (CQRS)          |
| Serialization   | System.Text.Json        |

---

## 📂 Project Structure

```bash
DevLife.Api
└── CodeRoast
    ├── CodeRoastHub.cs
    ├── CodeRoastEndpointDefinition.cs
    └── CodeRoastExceptionFilter.cs

DevLife.Application
└── CodeRoast
    ├── CodeRoastCommand.cs
    └── RoastCodeHandler.cs

DevLife.Domain
└── CodeRoastEntities
    ├── CodeRoastRequest.cs
    ├── CodeRoastResponse.cs
    ├── RoastLog.cs
└── CodeRoastRepositories
    └── ICodeRoastRepository.cs
└── CodeRoastExceptions
    └── CodeRoastException.cs

DevLife.Infrastructure
└── CodeRoastRepository
    └── CodeRoastRepository.cs
└── CodeRoastConfiguration
    └── OpenAiSettings.cs
```

---

## 📡 SignalR Events
- `ReceiveRoast`: Broadcasts the roast message to all connected WebSocket clients.

```js
connection.on("ReceiveRoast", (message) => {
    console.log("🔥 Roast Received:", message);
});
```

Endpoint: `/ws/coderoast`

---

## 🧪 Example Request
```http
POST /api/roast
Content-Type: application/json

{
  "codeSnippet": "for(int i=0;i<10;i++){System.Console.WriteLine(i);}"
}
```

Response:
```json
{
  "verdict": "Brutal honesty incoming",
  "roastMessage": "Wow, using a for loop? What is this, 1998?"
}
```

---

## 🛠 Configuration
**appsettings.json**:
```json
{
  "OpenAi": {
    "ApiKey": "your-api-key",
    "Endpoint": "https://api.openai.com/v1/chat/completions"
  },
  "ConnectionStrings": {
    "MongoDb": "mongodb://localhost:27017"
  }
}
```

---

## 🧠 Future Enhancements
- Roast Levels (nice, sarcastic, aggressive)
- Roast history view
- User profile tracking
- Multiplayer roast battles 💥

---

##  Credits
Built with ❤️ for DevLife Portal – Developer Life Simulator. Enjoy the roast and embrace the pain .
