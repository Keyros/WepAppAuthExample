FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebApp.RandomStringGenerator/WebApp.RandomStringGenerator.csproj", "WebApp.RandomStringGenerator/"]
RUN dotnet restore "WebApp.RandomStringGenerator/WebApp.RandomStringGenerator.csproj"
COPY . .
WORKDIR "/src/WebApp.RandomStringGenerator"
RUN dotnet build "WebApp.RandomStringGenerator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApp.RandomStringGenerator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApp.RandomStringGenerator.dll"]
