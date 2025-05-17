ARG PROJECT_NAME=app.proyectKevinBarre.api

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
ARG PROJECT_NAME
ENV PROJECT=$PROJECT_NAME
WORKDIR /app
COPY . .
RUN dotnet publish ${PROJECT} -c Release -o out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
ARG PROJECT_NAME
ENV PROJECT=$PROJECT_NAME
WORKDIR /app
COPY --from=build-env /app/out .

ENV ASPNETCORE_ENVIRONMENT=docker
ENV ASPNETCORE_URLS=http://*:81
EXPOSE 81

ENTRYPOINT dotnet ${PROJECT}.dll
