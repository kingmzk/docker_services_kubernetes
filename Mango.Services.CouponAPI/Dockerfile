
# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy only the CouponAPI project file
COPY ["Mango.Services.CouponAPI/Mango.Services.CouponAPI.csproj", "Mango.Services.CouponAPI/"]

# Restore dependencies for the project
RUN dotnet restore "Mango.Services.CouponAPI/Mango.Services.CouponAPI.csproj"

# Copy the rest of the source code
COPY . .

WORKDIR "/src/Mango.Services.CouponAPI"
RUN dotnet build "Mango.Services.CouponAPI.csproj" -c Release -o /app/Build

# Publish Stage
FROM build AS publish
RUN dotnet publish "Mango.Services.CouponAPI.csproj" -c Release -o /app/publish

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 5082

ENV ASPNETCORE_URLS=http://+:5082
ENTRYPOINT ["dotnet", "Mango.Services.CouponAPI.dll"]