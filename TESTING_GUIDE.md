# 🧪 Rent-A-Ride API Testing Guide

This guide explains how to run the application, set up the database, and test the API endpoints using Swagger.

## 🚀 1. Prerequisites

Ensure you have **PostgreSQL** installed and running on your local machine.
The application expects the database at: `localhost:5432`.
Credentials: `postgres` / `postgres` (or update `appsettings.json`).

## 🏃 2. Running the Application

1. Open a terminal in the API project directory:
   ```bash
   cd src/RentARide.API
   ```

2. Run the application:
   ```bash
   dotnet run
   ```

3. The application will start and automatically:
   - Create the database (`rentaride`) if it doesn't exist.
   - Apply migrations.
   - Seed initial data (Admin user, Vehicles, Amenities).

4. Open your browser and navigate to the Swagger UI:
   - **http://localhost:5000/swagger** (or the port shown in your terminal)

## 🔑 3. Initial Credentials

The system seeds a default Administrator account:

- **Email**: `admin@rentaride.com`
- **Password**: `Admin123!`

## 🧪 4. Testing Workflow (Happy Path)

Follow these steps to verify the system works:

### Step A: Login as Admin
1. Open **Auth** > `POST /api/Auth/login`.
2. Enter the Admin credentials above.
3. Copy the `token` from the response.
4. Scroll to top of Swagger, click **Authorize**.
5. Type `Bearer <paste_token>` and click **Authorize**.
6. Close the modal.

### Step B: Verify Data (as Admin)
1. Open **Vehicles** > `GET /api/Vehicles`.
   - You should see seeded vehicles (Toyota RAV4, Honda CR-V).
2. Open **Users** > `GET /api/Users`.
   - You should see the Admin user.

### Step C: Register as a Customer
1. **Logout** (refresh page or clear Authorize token).
2. Open **Auth** > `POST /api/Auth/register`.
3. Enter customer details:
   ```json
   {
     "email": "customer@test.com",
     "password": "Password123!",
     "firstName": "John",
     "lastName": "Doe"
   }
   ```
4. Copy the new `token` from the response.
5. Click **Authorize** and input `Bearer <new_token>`.

### Step D: Rent a Vehicle
1. Get a Vehicle ID from Step B (e.g., verify `GET /api/Vehicles` again).
2. Open **Rentals** > `POST /api/Rentals`.
3. Use the following payload:
   ```json
   {
     "vehicleId": "paste_vehicle_guid_here",
     "startDate": "2026-06-01",
     "endDate": "2026-06-05",
     "amenityIds": []
   }
   ```
4. Execute. You should get a `201 Created` response with rental details.

### Step E: View Rental History
1. Open **Rentals** > `GET /api/Rentals/history`.
2. You should see the rental you just created.

## 🛠️ Troubleshooting

- **Database Connection Error**: Ensure Postgres is running and credentials in `src/RentARide.API/appsettings.json` matches your setup.
- **"Unable to resolve service"**: Run `dotnet clean` and `dotnet build` again.
- **Verification Failed**: If any step fails, check the console output for error logs.
