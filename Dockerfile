
# 1. Build environment
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore src/GithubActionsDemo.Web/GithubActionsDemo.csproj

RUN dotnet publish src/GithubActionsDemo.Web/GithubActionsDemo.csproj \
    -c Release \
    --no-restore \
    -o /app/publish

# 2. Runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

USER $APP_UID

ENTRYPOINT ["dotnet", "GithubActionsDemo.dll"]
