Write-Host "Starting Todo App Project..." -ForegroundColor Green
Write-Host ""

Write-Host "Step 1: Starting Backend API..." -ForegroundColor Yellow
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd TodoApp.API; dotnet run"
Start-Sleep -Seconds 5

Write-Host "Step 2: Starting Frontend..." -ForegroundColor Yellow
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd todo-app-react; npm start"

Write-Host ""
Write-Host "Project is starting up!" -ForegroundColor Green
Write-Host "Backend API: http://localhost:5000/graphql" -ForegroundColor Cyan
Write-Host "Frontend: http://localhost:3000" -ForegroundColor Cyan
Write-Host ""
Write-Host "Press any key to exit..." -ForegroundColor Yellow
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
