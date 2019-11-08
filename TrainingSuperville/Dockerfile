FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 7010
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["TrainingSuperville/TrainingSuperville.csproj", "TrainingSuperville/"]
RUN dotnet restore "TrainingSuperville/TrainingSuperville.csproj"
COPY . .
WORKDIR "/src/TrainingSuperville"
RUN dotnet build "TrainingSuperville.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TrainingSuperville.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TrainingSuperville.dll"]