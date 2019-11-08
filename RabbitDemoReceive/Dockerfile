FROM mcr.microsoft.com/dotnet/core/runtime:3.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["RabbitDemoReceive/RabbitDemoReceive.csproj", "RabbitDemoReceive/"]
RUN dotnet restore "RabbitDemoReceive/RabbitDemoReceive.csproj"
COPY . .
WORKDIR "/src/RabbitDemoReceive"
RUN dotnet build "RabbitDemoReceive.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RabbitDemoReceive.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RabbitDemoReceive.dll"]