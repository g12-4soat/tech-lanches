#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TechLanches.API/TechLanches.API.csproj", "TechLanches.API/"]
RUN dotnet restore "TechLanches.API/TechLanches.API.csproj"
COPY . .
WORKDIR "/src/TechLanches.API"
RUN dotnet build "TechLanches.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TechLanches.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechLanches.API.dll"]