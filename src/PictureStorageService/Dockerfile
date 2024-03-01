#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
LABEL org.opencontainers.image.source = "https://github.com/UntitleCMS/PictureStorageService"
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/PictureStorageService/PictureStorageService.csproj", "PictureStorageService/"]
RUN dotnet restore "PictureStorageService/PictureStorageService.csproj"
COPY ./src .
WORKDIR "/src/PictureStorageService"
RUN dotnet build "PictureStorageService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PictureStorageService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
RUN mkdir /img-storage
VOLUME ["/img-storage"]
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PictureStorageService.dll"]