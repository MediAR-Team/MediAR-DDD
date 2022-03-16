FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

COPY ./src/Database/DatabaseMigrator/ ./
RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/mssql/server:2019-CU14-ubuntu-20.04

# Switch to root user for access to apt-get install
USER root

COPY --from=build-env /app/out Migrator/
COPY ./src/Database/MediAR.Database/Scripts/ Scripts/
COPY ./compose/database/create-db.sql .
COPY ./compose/database/upgrade-db.sh .
COPY ./compose/database/entrypoint.sh .

RUN wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb && \
    dpkg -i packages-microsoft-prod.deb && \
    rm packages-microsoft-prod.deb && \
    apt-get update && \
    apt-get install -y apt-transport-https && \
    apt-get update && \
    apt-get install -y dotnet-runtime-5.0

RUN chmod +x ./entrypoint.sh

# Switch back to mssql user and run the entrypoint script
USER mssql
ENTRYPOINT /bin/bash ./entrypoint.sh
