# Stage 1: Build Angular application
FROM node:16 AS angular-build
WORKDIR /app/client
COPY client/package*.json ./
RUN npm install
COPY client/ .
RUN npm run build --prod

# Stage 2: Build .NET application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS dotnet-build

# Install Git
RUN apt-get update && apt-get install -y git && rm -rf /var/lib/apt/lists/*
RUN which git

WORKDIR /app/server
COPY server/*.csproj ./
RUN dotnet restore --use-current-runtime
COPY server/ .
RUN dotnet publish -c Release -o out /app/server --use-current-runtime --self-contained false --no-restore

# Stage 3: Create the final image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=dotnet-build /app/server/out ./
COPY --from=angular-build /app/client/dist ./wwwroot

# Set the entry point for the .NET application
ENTRYPOINT ["dotnet", "mega-clip-game-hub-api.dll"]
