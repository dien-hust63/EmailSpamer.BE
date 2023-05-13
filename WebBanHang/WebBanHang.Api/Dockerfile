FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App
COPY . .
ENTRYPOINT ["dotnet", "WebBanHang.Api.dll"]