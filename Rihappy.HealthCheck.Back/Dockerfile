#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-bullseye-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-bullseye-slim AS build
WORKDIR /src
COPY ["Rihappy.HealthCheck.API/Rihappy.HealthCheck.API.csproj", "Rihappy.HealthCheck.API/"]

RUN dotnet restore "Rihappy.HealthCheck.API/Rihappy.HealthCheck.API.csproj"
COPY . .
WORKDIR "/src/Rihappy.HealthCheck.API"
RUN dotnet build "Rihappy.HealthCheck.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rihappy.HealthCheck.API.csproj" -c Release -o /app/publish

FROM base AS final

RUN apt-get update 
RUN apt-get install -y tzdata 
RUN cp /usr/share/zoneinfo/America/Sao_Paulo /etc/localtime
RUN echo "America/Sao_Paulo" > /etc/timezone
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rihappy.HealthCheck.API.dll"]