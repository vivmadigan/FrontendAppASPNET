# FrontendAppASPNET

A lightweight test frontend for the Invoice API that mocks the login process to verify role‐based authorization and UI behavior.

## Description

This ASP.NET Core MVC app simulates two roles—**User** and **Admin**—without a real authentication backend. It’s designed to:

- Validate that the Invoice API correctly enforces permissions  
- Demonstrate UI variations based on role claims  

## Features

- **Mocked Authentication**  
  - Select “User” or “Admin” on a simple login form  
  - Role stored in session and sent as an `X-USER-ROLE` header on API calls  

- **Role-Based UI Logic**  
  - **User**: View and create invoices only  
  - **Admin**: Full CRUD access (view, create, update, delete)  

- **Invoice Interaction**  
  - Fetches invoice list via `GET /api/invoices`  
  - Posts new invoices with `POST /api/invoices`  
  - Updates and deletes when permitted by role  

- **Error Simulation**  
  - Displays `401 Unauthorized` and `403 Forbidden` UI flows when role restrictions are triggered  

## Technologies

- ASP.NET Core MVC (Razor Pages)  
- Typed `HttpClient` for API calls  
- Session-based role storage  
