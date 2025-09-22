# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY *.sln .
COPY TodoApp.API/*.csproj TodoApp.API/
COPY TodoApp.Core/*.csproj TodoApp.Core/
COPY TodoApp.Infrastructure/*.csproj TodoApp.Infrastructure/
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 80
ENTRYPOINT ["dotnet", "TodoApp.API.dll"]