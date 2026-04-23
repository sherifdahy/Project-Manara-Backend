# ─────────────────────────────────────────────
# STAGE 1 — Restore
# Copy only .csproj files first so Docker can
# cache the restore layer. Nothing re-runs here
# unless a .csproj actually changes.
# ─────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS restore
WORKDIR /src

# Copy solution file
COPY App.sln .

# Copy every .csproj in its correct folder
# Paths match the FLAT structure of this repo
COPY App.Core/App.Core.csproj                   App.Core/
COPY App.Services/App.Services.csproj           App.Services/
COPY App.Infrastructure/App.Infrastructure.csproj App.Infrastructure/
COPY App.Application/App.Application.csproj    App.Application/
COPY App.API/App.API.csproj                    App.API/

# Restore the whole solution (cached unless a .csproj changed)
RUN dotnet restore App.sln


# ─────────────────────────────────────────────
# STAGE 2 — Build & Publish
# Full source is copied here, then published
# in Release mode to /app/publish
# ─────────────────────────────────────────────
FROM restore AS publish
WORKDIR /src

# Copy the rest of the source code
COPY . .

RUN dotnet publish App.API/App.API.csproj \
    --configuration Release \
    --no-restore \
    --output /app/publish


# ─────────────────────────────────────────────
# STAGE 3 — Runtime
# Lean ASP.NET runtime image — no SDK bloat.
# Only the published output is copied here.
# ─────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Create a non-root user for security
RUN addgroup --system appgroup && adduser --system --ingroup appgroup appuser

# Copy published output from the publish stage
COPY --from=publish /app/publish .

# Give ownership to the non-root user
RUN chown -R appuser:appgroup /app
USER appuser

# Port your API listens on (matches launchSettings.json)
EXPOSE 5001

# Set ASP.NET Core to listen on the correct port
ENV ASPNETCORE_URLS=http://+:5001
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "App.API.dll"]
