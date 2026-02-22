# 🛒 MultiShop E-Commerce Microservice Project

This repository contains the **MultiShop E-Commerce Microservice Project**, developed by following **Murat Yücedağ’s** ASP.NET Core microservices training on Udemy.

The project is built using **ASP.NET Core 8.0** and **Web API**, and represents a **real-world, enterprise-level e-commerce platform**.  
It includes **Public (Default)**, **Admin**, and **User** panels and is designed with **modern backend architecture principles** in mind.

> 🎯 The main goal of this project is not only to build a working application, but to gain hands-on experience with **scalable**, **maintainable**, **secure**, and **distributed microservice architectures**.

This is an **educational project**. Some parts may be incomplete; however, the primary aim is to integrate and experiment with **advanced technologies** in a single, large-scale system.

---

## 🔥 Technology Stack

| Category | Technologies |
|--------|--------------|
| Backend Framework | ASP.NET Core 8.0, Web API |
| Databases | MongoDB, MSSQL, PostgreSQL, Redis |
| ORM & Mapping | Entity Framework Core, AutoMapper |
| Authentication | IdentityServer4, JWT |
| API Gateway | Ocelot |
| Messaging & Realtime | RabbitMQ, SignalR |
| Frontend | ASP.NET Core MVC, Razor Views, View Components |
| Containerization | Docker, Docker Compose, Portainer |
| Other | Google Cloud Storage, RapidAPI, Localization (RESX) |

---

## 🧠 Architectural Approach

The project is designed using **Microservice Architecture** principles.

Each business domain is implemented as:
- Independent services
- Separate databases
- Individually deployable components

This approach provides **high scalability**, **fault isolation**, and **ease of maintenance**.

---

## 🧩 Microservices Overview

| Microservice | Responsibility | Database |
|-------------|----------------|----------|
| Catalog | Product, category, slider, feature management | MongoDB |
| Discount | Coupon and discount operations | MSSQL |
| Order | Order and address management (CQRS) | MSSQL |
| Basket | User shopping cart operations | Redis |
| Cargo | Shipping and logistics processes | MSSQL |
| Comment | Product reviews and ratings | MSSQL |
| Message | User messaging system | PostgreSQL |
| Identity | Authentication & authorization | IdentityServer |
| Gateway | Central API routing | Ocelot |

---

## 🏗️ Architecture & Design Patterns

- Onion Architecture (Order Service)
- CQRS (Command Query Responsibility Segregation)
- MediatR
- Mediator Pattern
- Generic Repository Pattern
- DTO–Entity Mapping (AutoMapper)

---

## 🔐 Authentication & Authorization

Authentication is handled via **IdentityServer** using **OAuth 2.0 / OpenID Connect** standards.

### Implemented Flows
- Resource Owner Password Flow (User authentication)
- Client Credentials Flow (Service-to-Service communication)

Security features include:
- JWT-based authentication
- Role and policy-based authorization
- Secure cookie and token management

---

## 🌐 Ocelot API Gateway

All microservices are exposed through a single entry point using **Ocelot API Gateway**.

Key benefits:
- Centralized routing
- Token validation
- Authorization
- Service isolation

---

## 🐳 Docker & Containerization

All services are containerized using **Docker**.

- Docker Compose for multi-container orchestration
- Persistent storage with Docker Volumes
- Container management via Portainer

---

## ⚡ Real-Time & Messaging

- **SignalR**
  - Message notifications
  - Comment count updates
  - Dashboard statistics

- **RabbitMQ**
  - Asynchronous communication
  - Event-driven messaging between services

---

## 🌍 External Integrations

- Google Cloud Storage – Product image storage
- RapidAPI – Currency rates, weather data, external services
- RESX Localization – Multi-language support (TR / EN)

---

## ✅ Conclusion

This project serves as a **comprehensive reference** for:
- Microservice architecture
- Distributed systems
- Secure authentication
- Modern backend development practices

> 📌 Many real-world backend scenarios are implemented and experienced within this project.

---

