#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Aspnetcore.PagingSortingFiltering.API/Aspnetcore.PagingSortingFiltering.API.csproj", "Aspnetcore.PagingSortingFiltering.API/"]
RUN dotnet restore "Aspnetcore.PagingSortingFiltering.API/Aspnetcore.PagingSortingFiltering.API.csproj"
COPY . .
WORKDIR "/src/Aspnetcore.PagingSortingFiltering.API"
RUN dotnet build "Aspnetcore.PagingSortingFiltering.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Aspnetcore.PagingSortingFiltering.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Aspnetcore.PagingSortingFiltering.API.dll"]