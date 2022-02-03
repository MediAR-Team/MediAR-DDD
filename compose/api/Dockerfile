FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

COPY ./src/ ./
RUN dotnet restore

RUN dotnet publish ./API/MediAR.MainAPI/ -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0

WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "MediAR.MainAPI.dll"]