# Use the .NET 8.0 runtime as the base image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
# Explicitly expose port 5000 to match your Docker Compose configuration
EXPOSE 5000

# Use the .NET 8.0 SDK for building the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
# Copy the project file and restore dependencies
COPY ["API/API.csproj", "API/"]
RUN dotnet restore "API/API.csproj"

# Copy the remaining files and build the application
COPY . .
WORKDIR "/src/API"
RUN dotnet build "API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application to a folder for deployment
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage: Run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Set the environment variable to listen on port 5195
ENV ASPNETCORE_URLS=http://+:5195
ENTRYPOINT ["dotnet", "API.dll"]
