# EventApi

A modern .NET 9 gRPC backend for managing events, categories, locations, and statuses.  
This project is designed for high performance, strong typing, and easy integration with various clients.

---

## Table of Contents

- [Overview](#overview)
- [Subsystems](#subsystems)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [gRPC API Documentation](#grpc-api-documentation)
- [How to Test](#how-to-test)
- [Diagrams](#diagrams)
- [License](#license)

---

## Overview

EventApi is a backend service for event management, built with C# 13 and .NET 9, using gRPC for all communication.  
It supports CRUD operations for events, categories, locations, and statuses, and is optimized for integration with web, mobile, or desktop clients.

---

## Subsystems

- **Events**: Create, update, delete, and list events.
- **Categories**: Manage event categories.
- **Locations**: Manage event locations.
- **Statuses**: Manage event statuses (e.g., Active, Past, Draft).

Each subsystem is implemented as a gRPC service, defined in the `Protos/` directory.

---

## Project Structure
EventApi/ ├── Controllers/         # (REST controllers, if any, are currently commented out) ├── Data/                # Entity models and DbContext ├── Documentation/       # Example data for documentation/testing ├── Factories/           # Mapping between entities and gRPC models ├── Handlers/            # Caching and other handlers ├── Migrations/          # EF Core migrations ├── Models/              # Data transfer models ├── Protos/              # .proto files (gRPC service definitions) ├── Repositories/        # Data access logic ├── Services/            # gRPC service implementations ├── Program.cs           # Application entry point └── EventApi.csproj      # Project file

---

## License

MIT License. See [LICENSE](LICENSE) for details.

---

## Notes

- This project uses gRPC only. There is no REST API.
- For API details, always refer to the `.proto` files.
- Example data for testing can be found in the `Documentation/` folder.



![image](https://github.com/user-attachments/assets/b84cd656-5f5e-4563-9938-2890cea523e5)



![image](https://github.com/user-attachments/assets/7edbe458-1c2f-4550-8319-08962ab979d0)


# gRPC API Documentation

This project uses **gRPC** for all API communication. The API is defined in Protocol Buffer (`.proto`) files.

## Subsystems

- **Events**: Manage events (CRUD operations)
- **Categories**: Manage event categories
- **Statuses**: Manage event statuses
- **Locations**: Manage event locations

Each subsystem is represented by a gRPC service in the corresponding `.proto` file.

## How to Use

1. **Find the .proto files** in the `Protos/` directory.
2. **Generate client/server code** using the proto files in your language of choice.
3. **Use a gRPC client** (e.g., [grpcurl](https://github.com/fullstorydev/grpcurl), [BloomRPC](https://github.com/bloomrpc/bloomrpc), or your own code) to interact with the API.

## Example: Event Service

**Service definition:**
