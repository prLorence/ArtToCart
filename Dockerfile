FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 8080
EXPOSE 8081
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/ArtToCart.Api/ArtToCart.Api.csproj", "src/ArtToCart.Api/"]
RUN dotnet restore "src/ArtToCart.Api/ArtToCart.Api.csproj"
COPY . .
WORKDIR "/src/src/ArtToCart.Api"
RUN dotnet build "ArtToCart.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ArtToCart.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArtToCart.Api.dll"]
