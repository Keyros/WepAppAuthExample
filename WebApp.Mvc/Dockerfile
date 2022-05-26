FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebApp.Mvc/WebApp.Mvc.csproj", "WebApp.Mvc/"]
COPY ["WebApp.Dal/WebApp.Dal.csproj", "WebApp.Dal/"]
RUN dotnet restore "WebApp.Mvc/WebApp.Mvc.csproj"
COPY . .
WORKDIR "/src/WebApp.Mvc"
RUN dotnet build "WebApp.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApp.Mvc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApp.Mvc.dll"]
