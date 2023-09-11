FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["education-api/education-api.csproj", "education-api/"]
COPY ["education-application/education-application.csproj", "education-application/"]
COPY ["education-domain/education-domain.csproj", "education-domain/"]
COPY ["education-infrastructure/education-infrastructure.csproj", "education-infrastructure/"]
RUN dotnet restore "education-api/education-api.csproj"
COPY . .
WORKDIR "/src/education-api"
RUN dotnet build "education-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "education-api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "education-api.dll"]