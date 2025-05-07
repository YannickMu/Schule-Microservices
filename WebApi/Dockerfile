FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet build

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS run
WORKDIR /app
COPY --from=build /app/bin/Debug/net8.0/* .

EXPOSE 5001
ENTRYPOINT [ "./WebApi" ]