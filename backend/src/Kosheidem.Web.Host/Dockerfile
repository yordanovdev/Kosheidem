FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/Kosheidem.Web.Host/Kosheidem.Web.Host.csproj", "src/Kosheidem.Web.Host/"]
COPY ["src/Kosheidem.Web.Core/Kosheidem.Web.Core.csproj", "src/Kosheidem.Web.Core/"]
COPY ["src/Kosheidem.Application/Kosheidem.Application.csproj", "src/Kosheidem.Application/"]
COPY ["src/Kosheidem.Core/Kosheidem.Core.csproj", "src/Kosheidem.Core/"]
COPY ["src/Kosheidem.EntityFrameworkCore/Kosheidem.EntityFrameworkCore.csproj", "src/Kosheidem.EntityFrameworkCore/"]
WORKDIR "/src/src/Kosheidem.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/Kosheidem.Web.Host", "src/Kosheidem.Web.Host"]
COPY ["src/Kosheidem.Web.Core", "src/Kosheidem.Web.Core"]
COPY ["src/Kosheidem.Application", "src/Kosheidem.Application"]
COPY ["src/Kosheidem.Core", "src/Kosheidem.Core"]
COPY ["src/Kosheidem.EntityFrameworkCore", "src/Kosheidem.EntityFrameworkCore"]
WORKDIR "/src/src/Kosheidem.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "Kosheidem.Web.Host.dll"]
