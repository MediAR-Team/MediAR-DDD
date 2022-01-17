FROM mcr.microsoft.com/mssql/server:2019-CU14-ubuntu-20.04

# Switch to root user for access to apt-get install
USER root

COPY ./src/Database/DatabaseMigrator/bin/Release/net5.0/publish/ Migrator/
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
