FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 9005
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["WebApi1/WebApi1.csproj", "WebApi1/"]
RUN dotnet restore "WebApi1/WebApi1.csproj"
COPY . .
WORKDIR "/src/WebApi1"
RUN dotnet build "WebApi1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi1.dll"]