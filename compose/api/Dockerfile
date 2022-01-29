FROM mcr.microsoft.com/dotnet/aspnet:5.0

COPY ./src/API/MediAR.MainAPI/bin/Release/net5.0/ App/
WORKDIR /App
ENTRYPOINT ["dotnet", "MediAR.MainAPI.dll"]