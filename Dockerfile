# Build image stage
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build-stage
RUN apk update && apk add bash && apk add curl
# Label defined to prune the image created during this stage by using the command: $ docker image prune --filter label=stage=build
LABEL stage=build
WORKDIR /api

# nuget restore
# Install Credential Provider and set env variables to enable Nuget restore with auth
ARG PAT
RUN wget -qO- https://raw.githubusercontent.com/Microsoft/artifacts-credprovider/master/helpers/installcredprovider.sh | bash
ENV NUGET_CREDENTIALPROVIDER_SESSIONTOKENCACHE_ENABLED true
ENV VSS_NUGET_EXTERNAL_FEED_ENDPOINTS "{\"endpointCredentials\": [{\"endpoint\":\"https://pkgs.dev.azure.com/ntguiri/_packaging/ntguiri/nuget/v3/index.json\", \"password\":\"${PAT}\"}]}"
ENV DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER 0

COPY . .
RUN dotnet restore -s "https://pkgs.dev.azure.com/ntguiri/_packaging/ntguiri/nuget/v3/index.json" -s "https://api.nuget.org/v3/index.json" "NG.DBManager.sln"

# dotnet build and publish
RUN dotnet build -c Release --no-restore
RUN dotnet test /api/tst/NG.DBManager.Test.UnitTest/NG.DBManager.Test.UnitTest.csproj --filter FullyQualifiedName~InMemory -c Release --logger "trx;LogFileName=result.trx" --no-build -r /publish/test
RUN dotnet publish -c Release --no-build -o /publish

# Runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /publish

COPY --from=build-stage /publish .

ENTRYPOINT ["dotnet", "NG.DBManager.Presentation.API.dll"]