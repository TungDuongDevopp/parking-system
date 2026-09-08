FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

RUN dotnet tool install -g Microsoft.Web.LibraryManager.Cli
RUN ln -s ~/.dotnet/tools/libman /usr/local/bin/libman

WORKDIR /src
COPY ["src/ParkingSystem.Web.Mvc/ParkingSystem.Web.Mvc.csproj", "src/ParkingSystem.Web.Mvc/"]
COPY ["src/ParkingSystem.Web.Core/ParkingSystem.Web.Core.csproj", "src/ParkingSystem.Web.Core/"]
COPY ["src/ParkingSystem.Application/ParkingSystem.Application.csproj", "src/ParkingSystem.Application/"]
COPY ["src/ParkingSystem.Core/ParkingSystem.Core.csproj", "src/ParkingSystem.Core/"]
COPY ["src/ParkingSystem.EntityFrameworkCore/ParkingSystem.EntityFrameworkCore.csproj", "src/ParkingSystem.EntityFrameworkCore/"]
WORKDIR "/src/src/ParkingSystem.Web.Mvc"
RUN dotnet restore 

WORKDIR /src
COPY ["src/ParkingSystem.Web.Mvc", "src/ParkingSystem.Web.Mvc"]
COPY ["src/ParkingSystem.Web.Core", "src/ParkingSystem.Web.Core"]
COPY ["src/ParkingSystem.Application", "src/ParkingSystem.Application"]
COPY ["src/ParkingSystem.Core", "src/ParkingSystem.Core"]
COPY ["src/ParkingSystem.EntityFrameworkCore", "src/ParkingSystem.EntityFrameworkCore"]
WORKDIR "/src/src/ParkingSystem.Web.Mvc"
RUN libman restore
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "ParkingSystem.Web.Mvc.dll"]
