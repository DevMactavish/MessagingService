FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

COPY . .
WORKDIR /src/Api/MessagingService.Api

RUN dotnet build "MessagingService.Api.csproj" --configfile /NuGet.Config -c Release -o /app/build
RUN dotnet publish "MessagingService.Api.csproj" --configfile /NuGet.Config -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final

WORKDIR /app
EXPOSE 80
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "MessagingService.Api.dll"]