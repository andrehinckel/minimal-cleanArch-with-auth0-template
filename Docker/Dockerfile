FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MyMinimalCleanArchWithAuth0/MyMinimalCleanArchWithAuth0.csproj", "MyMinimalCleanArchWithAuth0/"]
RUN dotnet restore "MyMinimalCleanArchWithAuth0/MyMinimalCleanArchWithAuth0.csproj"
COPY . .
WORKDIR "/src/MyMinimalCleanArchWithAuth0"
RUN dotnet build "MyMinimalCleanArchWithAuth0.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MyMinimalCleanArchWithAuth0.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyMinimalCleanArchWithAuth0.dll"]
