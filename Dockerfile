# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS base
WORKDIR /src

COPY src/Squares.WebAPI/Squares.WebAPI.csproj src/Squares.WebAPI/Squares.WebAPI.csproj
COPY src/Squares.Application/Squares.Application.csproj src/Squares.Application/Squares.Application.csproj
COPY src/Squares.Domain/Squares.Domain.csproj src/Squares.Domain/Squares.Domain.csproj
COPY src/Squares.Infrastructure/Squares.Infrastructure.csproj src/Squares.Infrastructure/Squares.Infrastructure.csproj
RUN dotnet restore src/Squares.WebAPI/Squares.WebAPI.csproj

COPY . .

RUN dotnet build src/Squares.WebAPI/Squares.WebAPI.csproj -c Release -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM base AS publish
RUN dotnet publish src/Squares.WebAPI/Squares.WebAPI.csproj -c Release -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Squares.WebAPI.dll"]
