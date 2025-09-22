# Test script for the frontend
Write-Host "Testing Frontend..."

# Navigate to frontend directory
cd todo-app-react

# Install dependencies
Write-Host "Installing dependencies..."
npm install

# Build the application
Write-Host "Building application..."
npm run build

Write-Host "Frontend test completed."
