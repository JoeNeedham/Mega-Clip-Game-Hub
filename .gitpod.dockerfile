# .gitpod.dockerfile

# Use the official Node.js 16 and .NET 7 SDK as the base image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-dotnet
FROM node:16 AS build-node

# Set the working directory for .NET
WORKDIR /app/server

# Install the .NET SDK
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
       apt-transport-https \
       unzip \
    && curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel 7.0 \
    && ln -s $HOME/.dotnet/dotnet /usr/bin/dotnet

# Copy the .NET project files to the container
COPY server .

# Build the .NET project
RUN dotnet publish -c Release -o out

# Set the working directory for Node.js
WORKDIR /app/client

# Install npm dependencies and build Angular
RUN npm install \
    && npm run build

# Final image
FROM mcr.microsoft.com/dotnet/aspnet:7.0

# Set the working directory
WORKDIR /app

# Copy the output from the .NET build stage
COPY --from=build-dotnet /app/server/out /app/server/out

# Copy the Angular build output
COPY --from=build-node /app/client/dist /app/client/dist

# Expose the ports used by Angular and .NET
EXPOSE 4200
EXPOSE 5000

# Start the application
CMD ["dotnet", "mega-clip-game-hub-api.dll"]
