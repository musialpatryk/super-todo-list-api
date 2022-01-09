FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./TodoListApiSqlite/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY ../TodoListApiSqlite ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 80 443
ENTRYPOINT ["dotnet", "TodoListApiSqlite.dll"]