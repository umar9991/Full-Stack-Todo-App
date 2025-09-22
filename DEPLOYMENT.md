# Deployment Guide

## Quick Start

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd todo-app
   ```

2. **Run the application with Docker Compose**
   ```bash
   docker-compose up --build
   ```

3. **Access the application**
   - Frontend: http://localhost:3000
   - Backend API: http://localhost:5000/graphql
   - GraphQL Playground: http://localhost:5000/graphql

## Manual Setup (Development)

### Backend Setup

1. Navigate to the backend directory:
   ```bash
   cd TodoApp.API
   ```

2. Install dependencies:
   ```bash
   dotnet restore
   ```

3. Update connection string in `appsettings.json` if needed

4. Run the application:
   ```bash
   dotnet run
   ```

### Frontend Setup

1. Navigate to the frontend directory:
   ```bash
   cd todo-app-react
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Generate Relay types:
   ```bash
   npm run relay
   ```

4. Start the development server:
   ```bash
   npm start
   ```

## Testing

Run the comprehensive test script:
```bash
./test-application.ps1
```

## Troubleshooting

### Common Issues

1. **Port conflicts**: Make sure ports 3000, 5000, and 1433 are available
2. **Database connection**: Ensure SQL Server is running and accessible
3. **Node modules**: Delete `node_modules` and run `npm install` again
4. **Docker issues**: Run `docker-compose down` and `docker-compose up --build`

### Logs

- Backend logs: Check the console output when running `dotnet run`
- Frontend logs: Check the browser console and terminal output
- Docker logs: Run `docker-compose logs <service-name>`

## Production Deployment

For production deployment, consider:

1. **Environment variables**: Set proper connection strings and API URLs
2. **Security**: Configure CORS, authentication, and HTTPS
3. **Database**: Use a managed database service
4. **Monitoring**: Add logging and monitoring solutions
5. **Scaling**: Use container orchestration (Kubernetes, Docker Swarm)

## Architecture

```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   React App     │    │   .NET API      │    │   SQL Server    │
│   (Port 3000)   │◄──►│   (Port 5000)   │◄──►│   (Port 1433)   │
│   Adobe Spectrum│    │   GraphQL       │    │   Database      │
└─────────────────┘    └─────────────────┘    └─────────────────┘
```

## Features Implemented

- ✅ GraphQL API with HotChocolate
- ✅ React frontend with Adobe React Spectrum
- ✅ Relay GraphQL client
- ✅ Docker containerization
- ✅ SQL Server database
- ✅ Clean Architecture
- ✅ TypeScript support
- ✅ Real-time task management
