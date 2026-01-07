# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy only the AuthAPI project file
COPY ["Mango.Services.AuthAPI/Mango.Services.AuthAPI.csproj", "Mango.Services.AuthAPI/"]

# Restore dependencies
RUN dotnet restore "Mango.Services.AuthAPI/Mango.Services.AuthAPI.csproj"

# Copy remaining source
COPY . .

# Build
WORKDIR "/src/Mango.Services.AuthAPI"
RUN dotnet build "Mango.Services.AuthAPI.csproj" -c Release -o /app/Build

# Publish Stage
FROM build AS publish
RUN dotnet publish "Mango.Services.AuthAPI.csproj" -c Release -o /app/publish

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 5085
ENV ASPNETCORE_URLS=http://+:5085
ENTRYPOINT ["dotnet", "Mango.Services.AuthAPI.dll"]