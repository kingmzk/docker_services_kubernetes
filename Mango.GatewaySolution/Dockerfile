# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy project file
COPY ["Mango.GatewaySolution/Mango.GatewaySolution.csproj", "Mango.GatewaySolution/"]
RUN dotnet restore "Mango.GatewaySolution/Mango.GatewaySolution.csproj"

# Copy full source
COPY . .
WORKDIR "/src/Mango.GatewaySolution"

# Publish
RUN dotnet publish "Mango.GatewaySolution.csproj" -c Release -o /app/publish

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app

# Copy published output
COPY --from=build /app/publish .

# Copy Ocelot config files
COPY Mango.GatewaySolution ./ocelot

# Gateway Port
EXPOSE 5087
ENV ASPNETCORE_URLS=http://+:5087

ENTRYPOINT ["dotnet", "Mango.GatewaySolution.dll"]
