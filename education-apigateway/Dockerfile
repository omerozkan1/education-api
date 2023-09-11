FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["education-apigateway/education-apigateway.csproj", "education-apigateway/"]
RUN dotnet restore "education-apigateway/education-apigateway.csproj"
COPY . .
WORKDIR "/src/education-apigateway"
RUN dotnet build "education-apigateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "education-apigateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "education-apigateway.dll"]