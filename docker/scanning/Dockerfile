#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0-alpine3.18-amd64 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine3.18-amd64 AS build
WORKDIR /src
COPY ["server/SOC.Scanning/SOC.Scanning.csproj", "SOC.Scanning/"]
RUN dotnet restore "SOC.Scanning/SOC.Scanning.csproj"

COPY server/SOC.Scanning ./SOC.Scanning
WORKDIR "/src/SOC.Scanning"
RUN dotnet build "SOC.Scanning.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SOC.Scanning.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SOC.Scanning.dll"]