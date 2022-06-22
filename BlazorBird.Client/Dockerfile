FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BlazorBird.Client/BlazorBird.Client.csproj", "BlazorBird.Client/"]
RUN dotnet restore "BlazorBird.Client/BlazorBird.Client.csproj"
COPY . .
WORKDIR "/src/BlazorBird.Client"
RUN dotnet build "BlazorBird.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorBird.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorBird.Client.dll"]
