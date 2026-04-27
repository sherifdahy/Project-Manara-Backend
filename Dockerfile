# ============================================================
# Stage 1: Build
# ============================================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy entire solution
COPY . .

# Build arg — resolved by CI/CD to e.g. Presentation/App.API/App.API.csproj
ARG PROJECT_PATH
RUN test -n "$PROJECT_PATH" || (echo "❌ PROJECT_PATH build-arg is required" && exit 1)

# Restore only the API project (faster than full solution restore)
RUN dotnet restore "$PROJECT_PATH"

# Publish to /app/publish
RUN dotnet publish "$PROJECT_PATH" \
    --configuration Release \
    --output /app/publish \
    --no-restore

# ============================================================
# Stage 2: Runtime
# ============================================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Non-root user for security
RUN useradd --no-create-home --shell /bin/false appuser

# Copy published output from build stage
COPY --from=build /app/publish .

# Build arg — resolved by CI/CD to e.g. App.API.dll
ARG DLL_NAME
RUN test -n "$DLL_NAME" || (echo "❌ DLL_NAME build-arg is required" && exit 1)
ENV APP_DLL="$DLL_NAME"

# Port 5001 — matches backend team's launchSettings & AWS config
EXPOSE 5001
ENV ASPNETCORE_URLS=http://+:5001
ENV ASPNETCORE_ENVIRONMENT=Production

USER appuser

ENTRYPOINT ["sh", "-c", "dotnet \"$APP_DLL\""]
