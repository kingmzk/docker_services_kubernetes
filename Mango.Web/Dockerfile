# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy only the Web project file
COPY ["Mango.Web/Mango.Web.csproj", "Mango.Web/"]

# Restore dependencies
RUN dotnet restore "Mango.Web/Mango.Web.csproj"

# Copy remaining source
COPY . .

# Build
WORKDIR "/src/Mango.Web"
RUN dotnet build "Mango.Web.csproj" -c Release -o /app/Build

# Publish Stage
FROM build AS publish
RUN dotnet publish "Mango.Web.csproj" -c Release -o /app/publish

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 5086
ENV ASPNETCORE_URLS=http://+:5086
ENTRYPOINT ["dotnet", "Mango.Web.dll"]
