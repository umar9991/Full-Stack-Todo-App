@echo off
echo Starting Todo App Project...
echo.

echo Step 1: Starting Backend API...
start "Backend API" cmd /k "cd TodoApp.API && dotnet run"
timeout /t 5 /nobreak > nul

echo Step 2: Starting Frontend...
start "Frontend" cmd /k "cd todo-app-react && npm start"

echo.
echo Project is starting up!
echo Backend API: http://localhost:5000/graphql
echo Frontend: http://localhost:3000
echo.
echo Press any key to exit...
pause > nul
