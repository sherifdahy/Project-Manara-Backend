# ============================================
# Stage 1: Build
# ============================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files using ACTUAL repo paths (flat structure, no Domain/ or Presentation/)
COPY App.Core/App.Core.csproj                         App.Core/
COPY App.Services/App.Services.csproj                 App.Services/
COPY App.Infrastructure/App.Infrastructure.csproj     App.Infrastructure/
COPY App.Application/App.Application.csproj           App.Application/
COPY App.API/App.API.csproj                           App.API/

# Restore NuGet packages (cached unless .csproj files change)
RUN dotnet restore App.API/App.API.csproj

# Copy everything else and publish
COPY . .
RUN dotnet publish App.API/App.API.csproj \
    --configuration Release \
    --output /app/publish \
    --no-restore

# ============================================
# Stage 2: Runtime
# ============================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

RUN adduser --disabled-password --gecos "" appuser
USER appuser

COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "App.API.dll"]
