FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80
ENV CONFIGURATION=Debug

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["Huboo.App/Huboo.App.csproj", "Huboo.App/"]
RUN dotnet restore "Huboo.App/Huboo.App.csproj"
COPY . .
WORKDIR "/src/Huboo.App"
RUN dotnet build "Huboo.App.csproj" -c $CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "Huboo.App.csproj" -c $CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Huboo.App.dll"]
