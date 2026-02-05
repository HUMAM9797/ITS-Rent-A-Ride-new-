# Entity Relationship Documentation

## Complete Entity Relationship Diagram

```
                                    ┌─────────────────────┐
                                    │      AuditLog       │
                                    │   (Audit Trail)     │
                                    └─────────────────────┘
                                    (Standalone - tracks all changes)


┌──────────────────────┐
│        User          │
│  ┌────────────────┐  │
│  │ Id (PK)        │  │
│  │ FirstName      │  │
│  │ LastName       │  │
│  │ Email (Unique) │  │
│  │ PasswordHash   │  │
│  │ PhoneNumber    │  │
│  │ Role (Enum)    │  │
│  │ IsEmailVerified│  │
│  │ LastLoginAt    │  │
│  └────────────────┘  │
└──────────┬───────────┘
           │
           │ 1
           │
           │ Has Many
           │
           │ N
┌──────────▼────────────────────────────────────────────┐
│                      Rental                           │
│  ┌─────────────────────────────────────────────────┐  │
│  │ Id (PK)                                         │  │
│  │ UserId (FK) ──────────────────────┐             │  │
│  │ VehicleId (FK) ───────────┐       │             │  │
│  │ StartDate, EndDate         │       │             │  │
│  │ ActualPickupDate           │       │             │  │
│  │ ActualReturnDate           │       │             │  │
│  │ BasePrice                  │       │             │  │
│  │ AmenitiesPrice             │       │             │  │
│  │ TotalPrice                 │       │             │  │
│  │ Status (Enum)              │       │             │  │
│  │ Notes                      │       │             │  │
│  │ CancellationReason         │       │             │  │
│  └─────────────────────────────┼───────┼─────────────┘  │
└────────────────────────────────┼───────┼────────────────┘
                                 │       │
                    ┌────────────┘       └────────────┐
                    │                                 │
                    │ N                               │ 1
                    │                                 │
                    │ Belongs To                      │ Belongs To
                    │                                 │
                    │ 1                               │ N
        ┌───────────▼──────────┐          ┌──────────▼────────────┐
        │   RentalAmenity      │          │      Vehicle          │
        │   (Join Table)       │          │  ┌─────────────────┐  │
        │  ┌────────────────┐  │          │  │ Id (PK)         │  │
        │  │ Id (PK)        │  │          │  │ VehicleTypeId   │  │
        │  │ RentalId (FK)  │  │          │  │   (FK) ─────────┼──┼──┐
        │  │ AmenityId (FK) │  │          │  │ Make, Model     │  │  │
        │  │ Quantity       │  │          │  │ Year            │  │  │
        │  │ PricePerDay    │  │          │  │ LicensePlate    │  │  │
        │  │ TotalPrice     │  │          │  │ VIN             │  │  │
        │  └────────┬───────┘  │          │  │ Color           │  │  │
        └───────────┼──────────┘          │  │ SeatingCapacity │  │  │
                    │                     │  │ Transmission    │  │  │
                    │ N                   │  │ FuelType        │  │  │
                    │                     │  │ Mileage         │  │  │
                    │ References          │  │ PricePerDay     │  │  │
                    │                     │  │ Status (Enum)   │  │  │
                    │ 1                   │  │ ImageUrl        │  │  │
        ┌───────────▼──────────┐          │  └─────────────────┘  │  │
        │      Amenity         │          └──────────┬────────────┘  │
        │  ┌────────────────┐  │                     │                │
        │  │ Id (PK)        │  │                     │ 1              │ N
        │  │ Name           │  │                     │                │
        │  │ Description    │  │                     │ Has One        │ Belongs To
        │  │ PricePerDay    │  │                     │                │
        │  │ IsAvailable    │  │                     │ 1              │ 1
        │  │ IconUrl        │  │         ┌───────────▼──────────┐     │
        │  └────────────────┘  │         │ VehicleMaintenance   │     │
        └─────────────────────┘         │  ┌────────────────┐  │     │
                                        │  │ Id (PK)        │  │     │
                                        │  │ VehicleId (FK) │  │     │
                                        │  │ LastMaintDate  │  │     │
                                        │  │ NextMaintDate  │  │     │
                                        │  │ LastMaintMiles │  │     │
                                        │  │ NextMaintMiles │  │     │
                                        │  │ Description    │  │     │
                                        │  │ Cost           │  │     │
                                        │  └────────────────┘  │     │
                                        └─────────────────────┘     │
                                                                    │
                                                                    │ 1
                                                        ┌───────────▼──────────┐
                                                        │    VehicleType       │
                                                        │  ┌────────────────┐  │
                                                        │  │ Id (PK)        │  │
                                                        │  │ Name           │  │
                                                        │  │ Description    │  │
                                                        │  │ PriceMultiplier│  │
                                                        │  └────────────────┘  │
                                                        └─────────────────────┘
```

## Relationship Summary

### One-to-Many Relationships

1. **User → Rental** (1:N)
   - One user can make many rentals
   - `User.Rentals` navigation property
   - `Rental.UserId` foreign key

2. **Vehicle → Rental** (1:N)
   - One vehicle can have many rentals (over time)
   - `Vehicle.Rentals` navigation property
   - `Rental.VehicleId` foreign key

3. **VehicleType → Vehicle** (1:N)
   - One vehicle type categorizes many vehicles
   - `VehicleType.Vehicles` navigation property
   - `Vehicle.VehicleTypeId` foreign key

### One-to-One Relationships

4. **Vehicle ↔ VehicleMaintenance** (1:1)
   - Each vehicle has exactly one maintenance record
   - `Vehicle.VehicleMaintenance` navigation property
   - `VehicleMaintenance.Vehicle` navigation property
   - `VehicleMaintenance.VehicleId` foreign key

### Many-to-Many Relationships

5. **Rental ↔ Amenity** (N:M via RentalAmenity)
   - One rental can have many amenities
   - One amenity can be in many rentals
   - Join table: `RentalAmenity`
   - `Rental.RentalAmenities` navigation property
   - `Amenity.RentalAmenities` navigation property
   - Stores: Quantity, PricePerDay, TotalPrice

## Entity Inheritance

All entities inherit from `BaseEntity`:

```
BaseEntity (Abstract)
├── Implements: IAuditable
│   ├── CreatedAt
│   └── UpdatedAt
├── Implements: ISoftDeletable
│   ├── IsDeleted
│   └── DeletedAt
└── Properties:
    └── Id (Guid)

    ↓ Inherited By ↓

├── User
├── Vehicle
├── VehicleType
├── VehicleMaintenance
├── Rental
├── Amenity
├── RentalAmenity
└── AuditLog
```

## Enum Types

### UserRole
- `Customer = 0` - Regular users who can rent vehicles
- `Admin = 1` - Administrators with full system access

### VehicleStatus
- `Available = 0` - Vehicle is available for rental
- `Rented = 1` - Vehicle is currently rented
- `Maintenance = 2` - Vehicle is under maintenance

### RentalStatus
- `Active = 0` - Rental is currently active
- `Completed = 1` - Rental has been completed
- `Cancelled = 2` - Rental has been cancelled

## Database Indexes (To Be Configured)

### Recommended Indexes:
1. `User.Email` - Unique index for login
2. `Vehicle.LicensePlate` - Unique index
3. `Vehicle.Status` - For filtering available vehicles
4. `Rental.UserId` - For user rental history
5. `Rental.VehicleId` - For vehicle rental history
6. `Rental.Status` - For filtering active rentals
7. `Rental.StartDate, EndDate` - For date range queries

## Soft Delete Behavior

All entities support soft deletion:
- Setting `IsDeleted = true` marks the record as deleted
- `DeletedAt` stores the deletion timestamp
- Queries should filter out soft-deleted records by default
- Admins can view soft-deleted records for audit purposes

## Audit Trail

The `AuditLog` entity tracks:
- User actions (Create, Update, Delete)
- Entity changes (old values → new values)
- Timestamp and user information
- IP address and user agent
- Useful for compliance and debugging
