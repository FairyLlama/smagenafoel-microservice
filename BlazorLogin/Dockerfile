# .NET 10 image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Kopier alt til /src
COPY . .

RUN dotnet restore

# Byg applikationen til release
RUN dotnet publish -c Release -o /app

# Runtime stage
FROM base AS runtime
WORKDIR /app

COPY --from=build /app .
ENTRYPOINT [ "dotnet", "BlazorLogin.dll" ]
