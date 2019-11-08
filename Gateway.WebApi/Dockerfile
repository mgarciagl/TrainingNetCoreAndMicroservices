FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 44300
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Gateway.WebApi/Gateway.WebApi.csproj", "Gateway.WebApi/"]
RUN dotnet restore "Gateway.WebApi/Gateway.WebApi.csproj"
COPY . .
WORKDIR "/src/Gateway.WebApi"
RUN dotnet build "Gateway.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gateway.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gateway.WebApi.dll"]