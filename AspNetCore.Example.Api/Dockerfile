FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["AspNetCore.Example.Api/AspNetCore.Example.Api.csproj", "AspNetCore.Example.Api/"]
COPY ["AspNetCore.Example.Domain/AspNetCore.Example.Domain.csproj", "AspNetCore.Example.Domain/"]
COPY ["AspNetCore.Example.Application/AspNetCore.Example.Application.csproj", "AspNetCore.Example.Application/"]
COPY ["AspNetCore.Example.Infra/AspNetCore.Example.Infra.csproj", "AspNetCore.Example.Infra/"]
RUN dotnet restore "AspNetCore.Example.Api/AspNetCore.Example.Api.csproj"
COPY . .
WORKDIR "/src/AspNetCore.Example.Api"
RUN dotnet build "AspNetCore.Example.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AspNetCore.Example.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AspNetCore.Example.Api.dll"]