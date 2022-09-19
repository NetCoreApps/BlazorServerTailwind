#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["BlazorServerTailwind/BlazorServerTailwind.csproj", "BlazorServerTailwind/"]
COPY ["BlazorServerTailwind.ServiceInterface/BlazorServerTailwind.ServiceInterface.csproj", "BlazorServerTailwind.ServiceInterface/"]
COPY ["BlazorServerTailwind.ServiceModel/BlazorServerTailwind.ServiceModel.csproj", "BlazorServerTailwind.ServiceModel/"]
RUN dotnet restore "BlazorServerTailwind/BlazorServerTailwind.csproj"
COPY . .
WORKDIR "/src/BlazorServerTailwind"
RUN dotnet build "BlazorServerTailwind.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorServerTailwind.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorServerTailwind.dll"]