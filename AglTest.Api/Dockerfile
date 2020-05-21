FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS builder
WORKDIR /app
COPY . .
RUN dotnet publish -c Release -r linux-musl-x64 /p:PublishSingleFile=true /p:PublishTrimmed=true ./AglTest.Api -o /app/bin

FROM mcr.microsoft.com/dotnet/core/runtime-deps:3.1-alpine
WORKDIR /app
COPY --from=builder /app/bin .
ENV ASPNETCORE_URLS "http://*:80"
EXPOSE 80
ENTRYPOINT [ "/app/AglTest.Api" ]
