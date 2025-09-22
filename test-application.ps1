# Comprehensive test script for the Todo App
Write-Host "=== Todo App Comprehensive Test ===" -ForegroundColor Green

# Test 1: Backend API
Write-Host "`n1. Testing Backend API..." -ForegroundColor Yellow
cd TodoApp.API
try {
    dotnet build
    if ($LASTEXITCODE -eq 0) {
        Write-Host "✓ Backend builds successfully" -ForegroundColor Green
    } else {
        Write-Host "✗ Backend build failed" -ForegroundColor Red
        exit 1
    }
} catch {
    Write-Host "✗ Backend test failed: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

# Test 2: Frontend
Write-Host "`n2. Testing Frontend..." -ForegroundColor Yellow
cd ../todo-app-react
try {
    npm install
    npm run build
    if ($LASTEXITCODE -eq 0) {
        Write-Host "✓ Frontend builds successfully" -ForegroundColor Green
    } else {
        Write-Host "✗ Frontend build failed" -ForegroundColor Red
        exit 1
    }
} catch {
    Write-Host "✗ Frontend test failed: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

# Test 3: Docker Compose
Write-Host "`n3. Testing Docker Compose..." -ForegroundColor Yellow
cd ..
try {
    docker-compose config
    if ($LASTEXITCODE -eq 0) {
        Write-Host "✓ Docker Compose configuration is valid" -ForegroundColor Green
    } else {
        Write-Host "✗ Docker Compose configuration is invalid" -ForegroundColor Red
        exit 1
    }
} catch {
    Write-Host "✗ Docker Compose test failed: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

Write-Host "`n=== All Tests Passed! ===" -ForegroundColor Green
Write-Host "The application is ready to run with: docker-compose up --build" -ForegroundColor Cyan
