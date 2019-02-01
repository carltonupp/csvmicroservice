FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 52583
EXPOSE 44327

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["CsvMicroservice.Api/CsvMicroservice.Api.csproj", "CsvMicroservice.Api/"]
RUN dotnet restore "CsvMicroservice.Api/CsvMicroservice.Api.csproj"
COPY . .
WORKDIR "/src/CsvMicroservice.Api"
RUN dotnet build "CsvMicroservice.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CsvMicroservice.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CsvMicroservice.Api.dll"]