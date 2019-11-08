FROM mcr.microsoft.com/dotnet/core/runtime:3.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["RabbitDemoSend/RabbitDemoSend.csproj", "RabbitDemoSend/"]
RUN dotnet restore "RabbitDemoSend/RabbitDemoSend.csproj"
COPY . .
WORKDIR "/src/RabbitDemoSend"
RUN dotnet build "RabbitDemoSend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RabbitDemoSend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RabbitDemoSend.dll"]