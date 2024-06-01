from mcr.microsoft.com/dotnet/sdk AS build

WORKDIR /app

# copy csproj and restore as distinct layers (CACHE)
COPY *.sln .
COPY API/*.csproj ./API/
COPY Domain/*.csproj ./Domain/
COPY Core/*.csproj ./Core/
COPY Core.Tests/*.csproj ./Core.Tests/

RUN dotnet restore

# copy everything else and build app
COPY API/ ./API/
COPY Domain ./Domain/
COPY Core ./Core/
COPY Core.Tests ./Core.Tests/

WORKDIR /app/API

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet AS runtime

WORKDIR /app

COPY --from=build /app/API/out ./

ENTRYPOINT ["dotnet", "API.dll"]

