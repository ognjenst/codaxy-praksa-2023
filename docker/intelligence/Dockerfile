#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0-alpine3.18-amd64 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine3.18-amd64 AS build
WORKDIR /src
COPY ["server/SOC.Intelligence/SOC.Intelligence.csproj", "SOC.Intelligence/"]
RUN dotnet restore "SOC.Intelligence/SOC.Intelligence.csproj"

COPY server/SOC.Intelligence ./SOC.Intelligence
WORKDIR "/src/SOC.Intelligence"
RUN dotnet build "SOC.Intelligence.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SOC.Intelligence.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SOC.Intelligence.dll"]