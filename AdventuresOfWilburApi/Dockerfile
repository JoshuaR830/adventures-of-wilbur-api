FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["AdventuresOfWilburApi/AdventuresOfWilburApi.csproj", "AdventuresOfWilburApi/"]
RUN dotnet restore "AdventuresOfWilburApi/AdventuresOfWilburApi.csproj"
COPY . .
WORKDIR "/src/AdventuresOfWilburApi"
RUN dotnet build "AdventuresOfWilburApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdventuresOfWilburApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdventuresOfWilburApi.dll"]
