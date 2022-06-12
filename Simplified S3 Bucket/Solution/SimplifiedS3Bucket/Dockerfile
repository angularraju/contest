FROM mcr.microsoft.com/dotnet/sdk:6.0 AS Build-env

WORKDIR /app/SimplifiedS3Bucket
COPY . .
RUN dotnet restore

RUN dotnet publish -c Release -o /app/out

#Build image
FROM  mcr.microsoft.com/dotnet/aspnet:6.0
ENV ASPNETCORE_URLS=http://+:5050
EXPOSE 5050
WORKDIR /app
COPY --from=Build-env app/out .
ENTRYPOINT ["dotnet", "SimplifiedS3Bucket.dll"]
