FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["src/ParkingSystem.Web.Host/ParkingSystem.Web.Host.csproj", "src/ParkingSystem.Web.Host/"]
COPY ["src/ParkingSystem.Web.Core/ParkingSystem.Web.Core.csproj", "src/ParkingSystem.Web.Core/"]
COPY ["src/ParkingSystem.Application/ParkingSystem.Application.csproj", "src/ParkingSystem.Application/"]
COPY ["src/ParkingSystem.Core/ParkingSystem.Core.csproj", "src/ParkingSystem.Core/"]
COPY ["src/ParkingSystem.EntityFrameworkCore/ParkingSystem.EntityFrameworkCore.csproj", "src/ParkingSystem.EntityFrameworkCore/"]
WORKDIR "/src/src/ParkingSystem.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/ParkingSystem.Web.Host", "src/ParkingSystem.Web.Host"]
COPY ["src/ParkingSystem.Web.Core", "src/ParkingSystem.Web.Core"]
COPY ["src/ParkingSystem.Application", "src/ParkingSystem.Application"]
COPY ["src/ParkingSystem.Core", "src/ParkingSystem.Core"]
COPY ["src/ParkingSystem.EntityFrameworkCore", "src/ParkingSystem.EntityFrameworkCore"]
WORKDIR "/src/src/ParkingSystem.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "ParkingSystem.Web.Host.dll"]
