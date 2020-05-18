#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["SJZ.ImageService/SJZ.ImageService.csproj", "SJZ.ImageService/"]
RUN dotnet restore "SJZ.ImageService/SJZ.ImageService.csproj"
COPY . .
WORKDIR "/src/SJZ.ImageService"
RUN dotnet build "SJZ.ImageService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SJZ.ImageService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SJZ.ImageService.dll"]