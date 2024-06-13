FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["backend/src/CinemaService/CinemaService.API/CinemaService.API.csproj", "backend/src/CinemaService/CinemaService.API/"]
COPY ["backend/src/Shared/Shared.csproj", "backend/src/Shared/"]
COPY ["backend/src/CinemaService/CinemaService.Application/CinemaService.Application.csproj", "backend/src/CinemaService/CinemaService.Application/"]
COPY ["backend/src/CinemaService/CinemaService.Core/CinemaService.Core.csproj", "backend/src/CinemaService/CinemaService.Core/"]
COPY ["backend/src/CinemaService/CinemaService.Infrastructure/CinemaService.Infrastructure.csproj", "backend/src/CinemaService/CinemaService.Infrastructure/"]
RUN dotnet restore "backend/src/CinemaService/CinemaService.API/CinemaService.API.csproj"
COPY . .
WORKDIR "/src/backend/src/CinemaService/CinemaService.API"
RUN dotnet build "CinemaService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CinemaService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CinemaService.API.dll"]
