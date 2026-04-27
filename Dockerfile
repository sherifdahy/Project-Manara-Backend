# ============================================
# Stage 1: Build
# ============================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY . .

ARG PROJECT_PATH
RUN test -n "$PROJECT_PATH" || (echo "PROJECT_PATH build-arg is required" && exit 1)

RUN dotnet restore "$PROJECT_PATH"

RUN dotnet publish "$PROJECT_PATH" \
    --configuration Release \
    --output /app/publish \
    --no-restore

# ============================================
# Stage 2: Runtime
# ============================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

RUN useradd --no-create-home --shell /bin/false appuser
USER appuser

COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ARG DLL_NAME
ENV APP_DLL="$DLL_NAME"
RUN test -n "$APP_DLL" || (echo "DLL_NAME build-arg is required" && exit 1)

ENTRYPOINT ["sh", "-c", "dotnet \"$APP_DLL\""]
