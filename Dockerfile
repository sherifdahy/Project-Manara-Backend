# ============================================
# Stage 1: Build
# ============================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy the solution file and all .csproj files first
# This allows Docker to cache the "restore" layer
COPY App.sln .
COPY Domain/App.Core/App.Core.csproj                     Domain/App.Core/
COPY Domain/App.Services/App.Services.csproj              Domain/App.Services/
COPY Infrastructure/App.Infrastructure/App.Infrastructure.csproj  Infrastructure/App.Infrastructure/
COPY App.Application/App.Application.csproj               App.Application/
COPY Presentation/App.API/App.API.csproj                  Presentation/App.API/

# Restore NuGet packages (cached unless .csproj files change)
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet publish Presentation/App.API/App.API.csproj \
    --configuration Release \
    --output /app/publish \
    --no-restore

# ============================================
# Stage 2: Runtime (smaller, no SDK)
# ============================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Security: run as non-root user
RUN adduser --disabled-password --gecos "" appuser
USER appuser

COPY --from=build /app/publish .

# ASP.NET Core listens on port 8080 by default in .NET 8+
EXPOSE 8080
ENV ASPNET_URLS=http://+:8080

ENTRYPOINT ["dotnet", "App.API.dll"]
