# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy only the ProductAPI project file
COPY ["Mango.Services.ProductApi/Mango.Services.ProductAPI.csproj", "Mango.Services.ProductApi/"]

# Restore dependencies
RUN dotnet restore "Mango.Services.ProductApi/Mango.Services.ProductAPI.csproj"

# Copy remaining source
COPY . .

# Build
WORKDIR "/src/Mango.Services.ProductApi"
RUN dotnet build "Mango.Services.ProductAPI.csproj" -c Release -o /app/Build

# Publish Stage
FROM build AS publish
RUN dotnet publish "Mango.Services.ProductAPI.csproj" -c Release -o /app/publish

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 5083
ENV ASPNETCORE_URLS=http://+:5083
ENTRYPOINT ["dotnet", "Mango.Services.ProductAPI.dll"]
