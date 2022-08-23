FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["KitchenPlanner/KitchenPlanner.csproj", "KitchenPlanner/"]
RUN dotnet restore "KitchenPlanner/KitchenPlanner.csproj"
COPY . .
WORKDIR "/src/KitchenPlanner"
RUN dotnet build "KitchenPlanner.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KitchenPlanner.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KitchenPlanner.dll"]
