# Todo App Setup Guide

## 🚨 **Prerequisites Required**

You need to install these tools first:

### **1. Install .NET 9.0 SDK**
- Download from: https://dotnet.microsoft.com/download/dotnet/9.0
- Choose "SDK" (not just runtime)
- Install and restart your computer
- Verify: Open command prompt and type `dotnet --version`

### **2. Install Node.js 18+**
- Download from: https://nodejs.org/
- Choose LTS version
- Install and restart your computer
- Verify: Open command prompt and type `node --version` and `npm --version`

## 🚀 **Quick Start (After Installing Prerequisites)**

### **Option 1: Use the Batch File (Windows)**
1. Double-click `run-project.bat`
2. Wait for both services to start
3. Open http://localhost:3000 in your browser

### **Option 2: Use PowerShell Script**
1. Right-click on `run-project.ps1`
2. Select "Run with PowerShell"
3. Wait for both services to start
4. Open http://localhost:3000 in your browser

### **Option 3: Manual Setup**
1. **Start Backend:**
   ```bash
   cd TodoApp.API
   dotnet restore
   dotnet run
   ```

2. **Start Frontend (in new terminal):**
   ```bash
   cd todo-app-react
   npm install
   npm start
   ```

## 📊 **What You'll See**

- **Frontend**: http://localhost:3000 - Modern Todo App interface
- **Backend API**: http://localhost:5000/graphql - GraphQL endpoint
- **Database**: SQLite file (`todoapp.db`) - No Docker needed!

## 🔧 **Troubleshooting**

### **If .NET is not recognized:**
- Restart your computer after installing .NET SDK
- Check if .NET is in your PATH environment variable

### **If Node.js is not recognized:**
- Restart your computer after installing Node.js
- Check if Node.js is in your PATH environment variable

### **If ports are busy:**
- Close other applications using ports 3000 or 5000
- Or change ports in the configuration files

### **If build fails:**
- Make sure you're in the correct directory
- Run `dotnet restore` in the API folder
- Run `npm install` in the React folder

## ✅ **Success Indicators**

- Backend shows: "Now listening on: http://localhost:5000"
- Frontend shows: "webpack compiled successfully"
- Browser opens to http://localhost:3000
- You can add and toggle tasks

## 🎯 **Features**

- ✅ Add new tasks with title and description
- ✅ View all tasks in a clean UI
- ✅ Toggle task status between "Pending" and "Completed"
- ✅ Real-time updates
- ✅ Modern Adobe React Spectrum UI
- ✅ SQLite database (no Docker required)

The project now uses SQLite instead of SQL Server, so you don't need Docker!

