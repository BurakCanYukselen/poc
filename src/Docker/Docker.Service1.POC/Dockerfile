FROM mcr.microsoft.com/dotnet/aspnet:3.1-bionic AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Docker/Docker.Service1.POC/Docker.Service1.POC.csproj", "src/Docker/Docker.Service1.POC/"]
RUN dotnet restore "src/Docker/Docker.Service1.POC/Docker.Service1.POC.csproj"
COPY . .
WORKDIR "/src/src/Docker/Docker.Service1.POC"
RUN dotnet build "Docker.Service1.POC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Docker.Service1.POC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Docker.Service1.POC.dll"]