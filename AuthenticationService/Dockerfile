# Træk image fra ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Kopier alt fra i dette directory
COPY . .

RUN dotnet restore

# Installer EntityFramework cli
RUN dotnet tool install --global dotnet-ef --version 8.0.22
ENV PATH="$PATH:/root/.dotnet/tools"

# Opdater database med migrations
CMD dotnet ef database update

# Byg AuthenticationService
RUN dotnet publish -c Release -o /app

# Final stage
FROM base AS runtime
WORKDIR /app

# Kopier færdige filer fra build stage og sæt DLL til entrypoint
COPY --from=build /app .
ENTRYPOINT ["dotnet", "AuthenticationService.dll"]