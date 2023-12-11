# Stage 1: Build Angular application
FROM node:16 AS angular-build
WORKDIR /app/client
COPY client/package*.json ./
RUN npm install
COPY client/ .
RUN npm run build --prod

# Stage 2: Build .NET application
FROM gitpod/workspace-dotnet:7.0

# Stage 3: Create the final image
COPY --from=angular-build /app/client/dist ./wwwroot
