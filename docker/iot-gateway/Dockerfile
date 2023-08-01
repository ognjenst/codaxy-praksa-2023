#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine3.18-amd64 AS base
WORKDIR /app
EXPOSE 80

FROM node:20-alpine3.17 AS node-build
WORKDIR /usr/src/soc

COPY ["client/src/package.json", "./client/src/"]
COPY ["client/src/yarn.lock", "./client/src/"]

WORKDIR /usr/src/soc/client/src
RUN yarn 

WORKDIR /usr/src/soc
COPY ["client", "./client"]
COPY ["server/SOC.IoT.ApiGateway/wwwroot", "./server/SOC.IoT.ApiGateway/wwwroot"]

ARG API_URL
ENV API_URL $API_URL

WORKDIR /usr/src/soc/client/src
RUN yarn build

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

COPY --from=node-build /usr/src/soc/server/SOC.IoT.ApiGateway/wwwroot /src/SOC.IoT.ApiGateway/wwwroot

WORKDIR "/src/SOC.IoT.ApiGateway"
RUN dotnet build "SOC.IoT.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SOC.IoT.ApiGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SOC.IoT.ApiGateway.dll"]