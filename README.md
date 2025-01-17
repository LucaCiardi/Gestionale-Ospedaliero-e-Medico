# Hospital Management System

A web-based hospital management system developed with ASP.NET Core MVC as part of the Generation Italy Junior .NET Developer course.  
The goal was to manage a set of entities (Hospitals, Doctors, and Patients) with CRUD operations, advanced filtering, sorting, and reporting.

## Project Overview

This application handles three main entities and their relationships:
- Hospitals (Ospedali)
- Doctors (Medici)
- Patients (Pazienti)

It also includes features such as:
- Dynamic Filtering and Sorting on lists
- Database-located Views, Stored Procedures, and Functions for advanced reporting
- A layered architecture with clean separation of concerns

## Features

1. CRUD Operations:
   - Create, Read, Update, and Delete for Hospitals, Doctors, and Patients.
   - Form validations and server-side checks.

2. Filtering & Sorting:
   - Multiple filters for each entity (e.g., by hospital, department, date ranges).
   - Sortable columns with ascending/descending toggle.
   - Visual indicators (arrows) for active sorting columns.

3. Database Capabilities:
   - Foreign Key constraints with cascade delete.
   - Custom Views for aggregated stats (e.g., vw_DashboardGenerale).
   - Stored Procedures for complex queries (e.g., sp_GetStatisticheOspedale).
   - Functions (e.g., fn_CalcolaEta for calculating patient ages).
   - Triggers for automatic updates (e.g., trg_UpdateMedicoStats).
   - Indices to optimize frequent queries.

4. Responsive Design:
   - Bootstrap 5 layout.
   - Clean UI with partial views for shared headers, footers, or scripts.

5. Validation:
   - Data Annotations for model validation.
   - Client-side and server-side validation checks.

## Project Structure

```
Gestionale_Ospedaliero_e_Medico/
├── Data/
│   ├── OspedaleDbContext.cs        // Entity Framework Core DbContext
│   └── (SQL scripts for triggers, views, stored procedures, functions)
├── Models/
│   ├── Medico.cs
│   ├── Ospedale.cs
│   ├── Paziente.cs
│   └── (Filter Models, View Models, etc.)
├── Services/
│   ├── Interfaces/
│   │   ├── IMedicoService.cs
│   │   ├── IOspedaleService.cs
│   │   └── IPazienteService.cs
│   └── Implementations/
│       ├── MedicoService.cs
│       ├── OspedaleService.cs
│       └── PazienteService.cs
├── Controllers/
│   ├── HomeController.cs
│   ├── MedicoController.cs
│   ├── OspedaleController.cs
│   └── PazienteController.cs
├── Views/
│   ├── Shared/
│   │   ├── _Layout.cshtml
│   │   └── _ValidationScriptsPartial.cshtml
│   ├── Medico/
│   │   └── (Index, Create, Edit, Delete, Details)
│   ├── Ospedale/
│   │   └── (Index, Create, Edit, Delete, Details)
│   └── Paziente/
│       └── (Index, Create, Edit, Delete, Details)
├── wwwroot/
│   ├── css/
│   └── js/
└── appsettings.json
```

### SQL Resources

Within the Data/ folder (or a specialized SQL/ folder), you may find:
- Functions  
  - fn_CalcolaEta.sql for patient age calculation.
- Stored Procedures  
  - sp_GetStatisticheOspedale.sql for hospital stats.
  - sp_RicercaPazienti.sql for advanced patient searches.
- Views  
  - vw_DashboardGenerale.sql for system-wide stats.
  - vw_StatisticheMedici.sql for doctor performance.
- Triggers  
  - trg_UpdateMedicoStats.sql for automatic data updates post-deletion.
- Indexes  
  - Scripts that define non-clustered indexes for frequent queries (naming starts with IX_).

## Setup Instructions

1. Clone the Repository  
   ```
   git clone https://github.com/LucaCiardi/Gestionale_Ospedaliero_e_Medico.git
   ```

2. Configure the DB Connection  
   - Update appsettings.json with your SQL Server connection string.

3. Migrate & Seed  
   - Run dotnet ef migrations add InitialCreate (if migrations not included).  
   - Run dotnet ef database update.

4. Run the Application  
   - Via CLI: dotnet run  
   - Via IDE: Press F5 or "Run" in Visual Studio.

5. Browse  
   - Go to https://localhost:{port}/  
   - Explore Hospitals, Doctors, and Patients pages.

## Usage

- Hospitals (Ospedali)  
  - Create, view, edit, and delete an Ospedale.  
  - Filter by Sede, name, or Pubblico (public vs. private).  
  - Sort by name, location, type, or number of doctors.

- Doctors (Medici)  
  - Create, view, edit, and delete a Medico.  
  - Assign a hospital to each doctor.  
  - Filter by name, Cognome, hospital, Reparto, or role (Primario).  
  - Sort by name, department, or patient count.

- Patients (Pazienti)  
  - Create, view, edit, and delete a Paziente.  
  - Assign a specific Medico for each patient.  
  - Filter by name, Cognome, CodiceFiscale, date range, or assigned Medico.  
  - Sort by name, admission date, assigned doctor, or hospital.

## Additional Notes

- Validation  
  - Data annotations for numeric bounds, required fields, and relationships.  
  - Model validations displayed via Bootstrap alert styles.

- Performance Optimizations  
  - Indices on frequently queried columns  
  - Eager loading with .Include(...) for performance and readability.

- Security  
  - Anti-forgery tokens in forms.  
  - Basic role management not yet implemented, but can be expanded.

## Author & Acknowledgements

- Author: Luca Ciardi  
- Course: Generation Italy Junior .NET Developer course  

## License

This project is licensed under the MIT License - see the LICENSE file for details.

---

Developed by Luca Ciardi as part of Generation Italy's Junior .NET Developer Course.
