FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ET.Web/ET.Web.csproj", "ET.Web/"]
RUN dotnet restore "ET.Web/ET.Web.csproj"
COPY . .
WORKDIR "/src/ET.Web"
RUN dotnet build "ET.Web.csproj" -c Release -o /app/build

FROM build AS publish

RUN apt-get update -y
RUN curl -sL https://deb.nodesource.com/setup_10.x | bash --debug
RUN apt-get install nodejs -yq

RUN dotnet publish "ET.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ET.Web.dll"]