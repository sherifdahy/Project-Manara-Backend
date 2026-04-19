📁 Project Structure
text
📦 App (Solution)
├── 📂 Domain
│   ├── 📂 App.Core            → Core entities & rules
│   └── 📂 App.Services        → Domain services
├── 📂 Infrastructure
│   └── 📂 App.Infrastructure  → Database, external services
├── 📂 Presentation
│   └── 📂 App.API             → Entry point (receives requests)
└── 📂 App.Application         → Business logic & MediatR handlers
⚙️ Prerequisites
.NET SDK 10.0
Git
🏃 How to Build & Run
bash
# 1. Clone the repo
git clone https://github.com/your-username/your-project.git
cd your-project

# 2. Restore packages
dotnet restore

# 3. Build the project
dotnet build --configuration Release

# 4. Run
dotnet run --project App.API
API docs will be available at: https://localhost:5001/scalar


🆘 Troubleshooting
Problem	Solution
dotnet not found	Install .NET SDK 10.0
Build failed	Run dotnet restore first
Port already in use	Change the port in launchSettings.json