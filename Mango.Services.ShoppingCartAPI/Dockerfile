# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy only the ShoppingCartAPI project file
COPY ["Mango.Services.ShoppingCartAPI/Mango.Services.ShoppingCartAPI.csproj", "Mango.Services.ShoppingCartAPI/"]

# Restore dependencies
RUN dotnet restore "Mango.Services.ShoppingCartAPI/Mango.Services.ShoppingCartAPI.csproj"

# Copy remaining source
COPY . .

# Build
WORKDIR "/src/Mango.Services.ShoppingCartAPI"
RUN dotnet build "Mango.Services.ShoppingCartAPI.csproj" -c Release -o /app/Build

# Publish Stage
FROM build AS publish
RUN dotnet publish "Mango.Services.ShoppingCartAPI.csproj" -c Release -o /app/publish

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 5084
ENV ASPNETCORE_URLS=http://+:5084
ENTRYPOINT ["dotnet", "Mango.Services.ShoppingCartAPI.dll"]