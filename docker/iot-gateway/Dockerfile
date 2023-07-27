#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine3.18-amd64 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine3.18-amd64 AS build
WORKDIR /src

COPY ["server/SOC.IoT.ApiGateway/SOC.IoT.ApiGateway.csproj", "SOC.IoT.ApiGateway/"]
COPY ["server/SOC.Conductor.Client/SOC.Conductor.Client.csproj", "SOC.Conductor.Client/"]
COPY ["server/SOC.IoT.Base/SOC.IoT.Base.csproj", "SOC.IoT.Base/"]
COPY ["server/SOC.IoT.Domain/SOC.IoT.Domain.csproj", "SOC.IoT.Domain/"]

RUN dotnet restore "SOC.IoT.ApiGateway/SOC.IoT.ApiGateway.csproj"

COPY server/SOC.IoT.ApiGateway ./SOC.IoT.ApiGateway
COPY server/SOC.Conductor.Client ./SOC.Conductor.Client
COPY server/SOC.IoT.Base ./SOC.IoT.Base
COPY server/SOC.IoT.Domain ./SOC.IoT.Domain

WORKDIR "/src/SOC.IoT.ApiGateway"
RUN dotnet build "SOC.IoT.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SOC.IoT.ApiGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SOC.IoT.ApiGateway.dll"]