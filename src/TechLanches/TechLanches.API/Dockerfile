FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TechLanches.API.csproj", "."]
RUN dotnet restore "./TechLanches.API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "TechLanches.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TechLanches.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechLanches.API.dll"]
