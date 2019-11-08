FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 7002
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Product.WebApi/Product.WebApi.csproj", "Product.WebApi/"]
RUN dotnet restore "Product.WebApi/Product.WebApi.csproj"
COPY . .
WORKDIR "/src/Product.WebApi"
RUN dotnet build "Product.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Product.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Product.WebApi.dll"]