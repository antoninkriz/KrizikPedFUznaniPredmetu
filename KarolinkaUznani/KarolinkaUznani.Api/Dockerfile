FROM microsoft/dotnet:aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5000

FROM microsoft/dotnet:sdk AS build
WORKDIR /src
COPY . .
RUN dotnet restore
WORKDIR /KarolinkaUznani.Api/
RUN dotnet build --no-restore -c Release -o /app

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT ["dotnet", "KarolinkaUznani.Api.dll"]
