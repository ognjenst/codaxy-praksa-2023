#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine3.18-amd64 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine3.18-amd64 AS build
WORKDIR /src
COPY ["server/SOC.Conductor/SOC.Conductor.csproj", "SOC.Conductor/"]
RUN dotnet restore "SOC.Conductor/SOC.Conductor.csproj"

COPY server/SOC.Conductor ./SOC.Conductor
WORKDIR "/src/SOC.Conductor"
RUN dotnet build "SOC.Conductor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SOC.Conductor.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SOC.Conductor.dll"]