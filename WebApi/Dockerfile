FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet build -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS run
WORKDIR /app
COPY --from=build /app/out/* .

EXPOSE 5001
ENTRYPOINT [ "./WebApi" ]