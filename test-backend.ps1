# Test script for the backend API
Write-Host "Testing Backend API..."

# Start the backend
Write-Host "Starting backend..."
cd TodoApp.API
dotnet run &
$backendPid = $!

# Wait for backend to start
Start-Sleep -Seconds 10

# Test GraphQL endpoint
Write-Host "Testing GraphQL endpoint..."
try {
    $response = Invoke-RestMethod -Uri "http://localhost:5000/graphql" -Method POST -ContentType "application/json" -Body '{"query":"query { getAllTasks { id title description status } }"}'
    Write-Host "Backend test successful: $($response | ConvertTo-Json)"
} catch {
    Write-Host "Backend test failed: $($_.Exception.Message)"
}

# Stop the backend
Stop-Process -Id $backendPid -Force
Write-Host "Backend test completed."
