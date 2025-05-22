# Stage 1

from mcr.microsoft.com/dotnet/sdk:6.0 as build

run mkdir -p /app
workdir /app
copy ["src/", ""]
copy ["nuget.config", ""]
arg FEED_ACCESSTOKEN
run sed -i "s|</configuration>|<packageSourceCredentials><oga.core.nuget><add key=\"Username\" value=\"Docker\" /><add key=\"ClearTextPassword\" value=\"${FEED_ACCESSTOKEN}\" /></oga.core.nuget></packageSourceCredentials></configuration>|" nuget.config

run dotnet restore "OGA.AAS.BackEnd.Domain/OGA.AAS.BackEnd.Domain.csproj" --configfile nuget.config --interactive
run dotnet restore "OGA.AAS.BackEnd.Infrastructure/OGA.AAS.BackEnd.Infrastructure.csproj" --configfile nuget.config --interactive
run dotnet restore "OGA.AAS.BackEnd.Application/OGA.AAS.BackEnd.Application.csproj" --configfile nuget.config --interactive
run dotnet restore "OGA.AAS.BackEnd.Business/OGA.AAS.BackEnd.Business.csproj" --configfile nuget.config --interactive
run dotnet restore "OGA.AAS.BackEnd.WebAPI/OGA.AAS.BackEnd.WebAPI.csproj" --configfile nuget.config --interactive

copy . /app
run dotnet publish "OGA.AAS.BackEnd.WebAPI/OGA.AAS.BackEnd.WebAPI.csproj" -c Release --no-restore -o publish

# Stage 2

from mcr.microsoft.com/dotnet/aspnet:6.0
copy --from=build /app/publish ./
expose 5000
entrypoint ["dotnet", "OGA.AAS.BackEnd.WebAPI.dll"]