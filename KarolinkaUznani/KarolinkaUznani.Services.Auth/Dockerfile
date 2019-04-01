FROM microsoft/dotnet
WORKDIR /dotnetapp
COPY ./bin/Docker .
ENV ASPNETCORE_URLS http://*:5050
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT dotnet KarolinkaUznani.Services.Auth.dll
