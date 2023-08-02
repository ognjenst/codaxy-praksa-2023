#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0-alpine3.18-amd64 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine3.18-amd64 AS build
WORKDIR /src
COPY ["server/SOC.IoT/SOC.IoT.csproj", "SOC.IoT/"]
RUN dotnet restore "SOC.IoT/SOC.IoT.csproj"

COPY server/SOC.IoT ./SOC.IoT
WORKDIR "/src/SOC.IoT"
RUN dotnet build "SOC.IoT.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SOC.IoT.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SOC.IoT.dll"]