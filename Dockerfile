	FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
	WORKDIR /source
	COPY . .
	RUN dotnet restore "./FA.JustBlog/FA.JustBlog.csproj" --disable-parallel
	RUN dotnet publish "./FA.JustBlog/FA.JustBlog.csproj" -c Release -o /app --no-restore

	FROM mcr.microsoft.com/dotnet/aspnet:6.0
	WORKDIR /app
	COPY --from=build /app ./

	ENTRYPOINT ["dotnet", "FA.JustBlog.dll"]