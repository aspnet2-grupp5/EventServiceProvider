# Event Management gRPC System

This project implements a gRPC-based Event Management System in C#/.NET. It provides a backend service with proto definitions for managing Events, Categories, Locations, and Statuses.

## 🧩 Project Structure

- **GrpcServiceServer/**: gRPC server implementation.
- **GrpcServiceClient/**: Console app client (optional UI).
- **Protos/**: `.proto` definitions for all services and messages.
- **README.md**: Documentation.

## 📦 Services Overview

### EventService

- `CreateEvent(EventRequest)`: Adds a new event.
- `GetAllEvents(Empty)`: Returns all events.
- `GetEventById(GetEventByIdRequest)`: Returns a specific event.
- `UpdateEvent(EventRequest)`: Updates an existing event.
- `DeleteEvent(GetEventByIdRequest)`: Deletes an event by ID.

### CategoryService

- `GetAllCategories(Empty)`: Returns all categories.
- `GetCategory(CategoryIdRequest)`: Returns a category by ID.

### LocationService

- `GetAllLocations(Empty)`: Returns all locations.
- `GetLocation(LocationIdRequest)`: Returns a location by ID.

### StatusService

- `GetAllStatuses(Empty)`: Returns all statuses.
- `GetStatus(StatusIdRequest)`: Returns a status by ID.

## 📄 Proto Files

All `.proto` files are located in the `Protos/` folder and compiled using `Grpc.Tools`.

### Sample: Event.proto

```proto
syntax = "proto3";

option csharp_namespace = "GrpcServiceServer.Protos";

package event;

import "google/protobuf/timestamp.proto";

message Event {
  string EventId = 1;
  string EventTitle = 2;
  string EventImage = 3;
  string Description = 4;
  google.protobuf.Timestamp Date = 5;
  double Price = 6;
  int32 Quantity = 7;
  int32 SoldQuantity = 8;
  Category Category = 9;
  Location Location = 10;
  Status Status = 11;
}
```

## 🏗️ How to Build & Run

### Requirements

- [.NET 7+ SDK](https://dotnet.microsoft.com/en-us/download)
- Visual Studio 2022 or newer (or `dotnet CLI`)

### Build Steps

```bash
git clone <repo-url>
cd GrpcServiceServer
dotnet build
dotnet run
```

### Client Usage

Use the `GrpcServiceClient` or any gRPC client (e.g., [BloomRPC](https://github.com/bloomrpc/bloomrpc)) to test.

## 📨 Example Requests & Responses

### EventService

#### AddEventForm

**Request:**

```json
{
  "EventTitle = Sample Event",
   "Description = This is a sample event description.",
   "Date = DateTime.UtcNow.AddDays(30)",
   "Price = 50.00m",
   "Quantity = 100",
   "CategoryName = Health",
   "Address = Solnavägen 5B",
   "StatusName = Past",
}
```

**Response:**

```json
{
  "status_code": 200,
  "message": "Event successfully created"
}
```

### CategoryService

#### GetCategory

**Request:**

```json
{
  "categoryId": 1
}
```

**Response:**

```json
{
  "categoryId": 1,
  "categoryName": "Music"
}
```

### LocationService

#### GetLocation

**Request:**

```json
{
  "locationId": 2
}
```

**Response:**

```json
{
  "locationId": 2,
  "address": "123 Main St",
  "city": " Tech City"
}
```

### StatusService

#### GetStatus

**Request:**

```json
{
  "statusId": 1
}
```

**Response:**

```json
{
  "statusId": 1,
  "statusName": "Active"
}
```

## ✅ Features
- Add, update, delete, and retrieve events.
- Lookup for categories, locations, and statuses.
- Strongly-typed messages via gRPC.
- Timestamp support.

- Add, update, delete, and retrieve events.
- Lookup for categories, locations, and statuses.
- Strongly-typed messages via gRPC.
- Timestamp support.


## 📄 License

MIT License (or your chosen license).


## Notes

- This project uses gRPC only. There is no REST API.
- For API details, always refer to the `.proto` files.
- Example data for testing can be found in the `Documentation/` folder.
```

1. **Find the .proto files** in the `Protos/` directory.
2. **Generate client/server code** using the proto files in your language of choice.
3. **Use a gRPC client** (e.g., [grpcurl](https://github.com/fullstorydev/grpcurl), [BloomRPC](https://github.com/bloomrpc/bloomrpc), or your own code) to interact with the API.
```

## 📊 Activity Diagram  
**Create an Event**  
![Create an Event]![Activity Diagram](https://github.com/user-attachments/assets/c95d59c3-4066-48ff-97bb-688f9eb5ef26)
![image](https://github.com/user-attachments/assets/b84cd656-5f5e-4563-9938-2890cea523e5)


```

## 📈 Sequence Diagram  
**Create an Event**  
![Create an Event](https://github.com/user-attachments/assets/7edbe458-1c2f-4550-8319-08962ab979d0)
``` 
