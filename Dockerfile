FROM mcr.microsoft.com/dotnet/sdk:6.0.100-alpine3.14-amd64

# Choose between Debug and Release
ARG runtimeenv="Debug"
ENV runtimeenv $runtimeenv

RUN mkdir -p /app/src/

WORKDIR /app/src

COPY WebService.Core WebService.Core 
COPY WebService.Core.Tests WebService.Core.Tests
COPY WebService.Entities WebService.Entities 
COPY WebService.Infrastructure WebService.Infrastructure
COPY WebService.Infrastructure.Tests WebService.Infrastructure.Tests
COPY BDSA2021-FinalProject.sln BDSA2021-FinalProject.sln

EXPOSE 5000-8000

RUN dotnet dev-certs https

ENTRYPOINT [ "dotnet", "run", "--project", "WebService.Core" ]
# RUN dotnet build --configuration ${runtimeenv}
# WORKDIR /app/src/WebService.Core/bin/${runtimeenv}/net6.0/
# ENTRYPOINT [ "./WebService.Core" ]

# ENTRYPOINT [ "/bin/sh" ]

#CMD ["dotnet", "run", "--project", "WebService.Core" ]