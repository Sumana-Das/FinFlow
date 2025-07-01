# FinFlow – Modular Financial Workflow Platform

A cloud-native, microservices-based platform to manage personal and enterprise financial workflows.

## 🔧 Tech Stack
- ASP.NET Core 8 Web API
- Entity Framework Core
- MediatR + CQRS
- JWT Authentication
- RabbitMQ + MassTransit
- Ocelot API Gateway
- Serilog + Seq
- Docker + Docker Compose

## 📦 Microservices
- **UserService** – Auth, roles, JWT
- **TransactionService** – Income/expense tracking
- **ReportService** – Financial summaries
- **ApprovalService** – Reimbursement workflows
- **NotificationService** – Email/SMS alerts

## 🚀 Getting Started
```bash
docker-compose up --build
