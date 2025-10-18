# Tasks for Implementation: New Zealand Hospital Doctor Channeling System


## Setup & Project Initialization
T001. [X] Initialize monorepo structure for Angular 20+ frontend and .NET 8 backend
T002. [X] Set up local development environment (Node.js, .NET 8 SDK, Angular CLI, etc.)
T003. [X] Install and configure local SQL Server (localdb) with connection string:
	"ConnectionString": "Data Source=(localdb)\\localDB;Initial Catalog=EChanneling;Integrated Security=True"
T004. Create the tables on the EChanneling database locally
T005. Set up environment variables and local configuration files for backend and frontend
T006. Run backend and frontend in local development mode
T007. Establish code quality, linting, and formatting standards

## Data Model & Contracts [P]
T008. Define FHIR-compliant data models for Patient, Doctor, Appointment, Prescription, Medical Record, etc.
T009. Implement API contracts (OpenAPI 3.0) for all major modules
T010. Set up authentication/authorization contracts (Azure AD B2C, custom roles)

## Patient Management System
T011. Build patient registration and onboarding flows (with cultural/language fields)
T012. Integrate with NZ Health Index Number (NHI) lookup
T013. Implement family history, genetic, and social determinants tracking
T014. Develop patient profile management UI and backend

## Doctor Portal & Management
T015. Create doctor onboarding and profile management
T016. Implement AI-powered patient prioritization and clinical decision support
T017. Build scheduling, availability, and smart template features

## Telehealth Platform
T018. Integrate HD video, AR tools, and real-time vital sign monitoring
T019. Implement session pause/resume logic for connectivity loss
T020. Add AI-powered consultation scoring and summarization
T021. Ensure accessibility and multilingual support

## Prescription System
T022. Integrate with NZ EPS and PHARMAC APIs
T023. Implement drug availability, cost alternatives, and adherence monitoring
T024. Add AI-driven dosage and interaction checks

## Analytics & Reporting
T025. Build Power BI integration for dashboards
T026. Implement clinical KPI, audit log, and population health reporting

## Barcode & Inventory
T027. Implement barcode handling for patients, labs, radiology, inventory
T028. Integrate with supply chain and asset management

## Security, Privacy & Compliance
T029. Enforce NZ Privacy Act, HIPAA, ISO 27001, SOC 2 controls
T030. Implement audit logging, consent management, and data retention

## Voice & AI Features
T031. Integrate voice-to-text for clinical and patient notes
T032. Implement AI-powered note generation and subscription features

## IIOT & Device Integration
T033. Design for future IIOT device integration (Bluetooth/WiFi, HL7, report scanning)

## Testing & Quality Gates [P]
T034. Write unit, integration, and end-to-end tests for all modules
T035. Set up automated accessibility and performance testing
T036. Conduct security and compliance validation

## Documentation & Quickstart [P]
T037. Document API usage, data models, and user journeys
T038. Provide quickstart guides for developers and users

---

- [P] = Tasks that can be executed in parallel
- Tasks are dependency-ordered: setup → local environment → models/contracts → core modules → integrations → polish
- Each task should be further decomposed into actionable subtasks during sprint planning.
- All configuration and testing steps must support local development and execution using the provided local SQL Server connection string.
