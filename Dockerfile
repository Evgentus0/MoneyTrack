FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

RUN mkdir /DockerSource
WORKDIR /DockerSource

COPY . /DockerSource

RUN dotnet restore
RUN dotnet publish -c release -o /DockerOutput --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
RUN mkdir /DockerOutput
WORKDIR /DockerOutput
COPY --from=build /DockerOutput ./

CMD ASPNETCORE_URLS=http://*:$PORT dotnet MoneyTrack.Web.Api.dll