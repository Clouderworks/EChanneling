# [PROJECT_NAME] Constitution
<!-- Example: Spec Constitution, TaskFlow Constitution, etc. -->

## Core Principles


### I. Healthcare compliance standards for New Zealand
All solutions must adhere to New Zealand healthcare compliance standards, including legal, regulatory, and ethical requirements.

### II. Angular 20 + .NET Core + SQL Server technical stack
The core technical stack is Angular 20 for frontend, .NET Core for backend, and SQL Server for data storage. All components should align with this stack unless exceptions are justified and approved.

### III. Security and privacy frameworks
Security and privacy must be embedded throughout the system, following best practices and relevant frameworks for healthcare data protection in New Zealand.

### IV. Development workflow and quality standards
Development must follow defined workflows, including code reviews, automated testing, and continuous integration to ensure high quality and maintainability.

### V. Cultural sensitivity requirements for NZ healthcare
All features and communications must respect and reflect the cultural diversity and sensitivity required in New Zealand healthcare, including Māori and other communities.

### VI. Performance and scalability targets
The system must meet defined performance benchmarks and be designed for scalability to handle growth in users and data.


### VIII. Barcode handling for Patient, Labs, Radiology, and Inventory
The system must support barcode handling for patients, labs, radiology, and inventory, ensuring seamless integration with other healthcare services and accurate tracking throughout the healthcare process.

### VII. Integration requirements with NZ health systems and minimal dependency
Integration with New Zealand health systems is required, with a focus on minimizing external dependencies and ensuring interoperability.

### [PRINCIPLE_1_NAME]
<!-- Example: I. Library-First -->
[PRINCIPLE_1_DESCRIPTION]
<!-- Example: Every feature starts as a standalone library; Libraries must be self-contained, independently testable, documented; Clear purpose required - no organizational-only libraries -->

### [PRINCIPLE_2_NAME]
<!-- Example: II. CLI Interface -->
[PRINCIPLE_2_DESCRIPTION]
<!-- Example: Every library exposes functionality via CLI; Text in/out protocol: stdin/args → stdout, errors → stderr; Support JSON + human-readable formats -->

### [PRINCIPLE_3_NAME]
<!-- Example: III. Test-First (NON-NEGOTIABLE) -->
[PRINCIPLE_3_DESCRIPTION]
<!-- Example: TDD mandatory: Tests written → User approved → Tests fail → Then implement; Red-Green-Refactor cycle strictly enforced -->

### [PRINCIPLE_4_NAME]
<!-- Example: IV. Integration Testing -->
[PRINCIPLE_4_DESCRIPTION]
<!-- Example: Focus areas requiring integration tests: New library contract tests, Contract changes, Inter-service communication, Shared schemas -->

### [PRINCIPLE_5_NAME]
<!-- Example: V. Observability, VI. Versioning & Breaking Changes, VII. Simplicity -->
[PRINCIPLE_5_DESCRIPTION]
<!-- Example: Text I/O ensures debuggability; Structured logging required; Or: MAJOR.MINOR.BUILD format; Or: Start simple, YAGNI principles -->

## [SECTION_2_NAME]
<!-- Example: Additional Constraints, Security Requirements, Performance Standards, etc. -->

[SECTION_2_CONTENT]
<!-- Example: Technology stack requirements, compliance standards, deployment policies, etc. -->



## Key Entities

### 🏥 Core Administrative & Operational Entities
- **Hospital / Facility**: Represents physical/virtual hospitals, wards, departments (scheduling, resource management).
- **Staff (Non-doctor)**: Nurses, midwives, allied health professionals, technicians, administrators.
- **Role & Access Control**: Defines security/authentication (aligning with HISO privacy & Health Information Governance).

### 💊 Clinical & Care Entities
- **Medical Record / EHR (Encounter)**: Core record of all patient interactions, admissions, discharges, clinical notes.
- **Diagnosis / Condition**: Problem list, ICD-10/SNOMED-coded conditions.
- **Lab Test / Diagnostic Result**: Pathology, imaging, and test results (HL7/FHIR integration).
- **Treatment Plan / Care Pathway**: Plans linked to guidelines (e.g., NZ cancer care pathways, chronic disease management).
- **Allergies & Adverse Events**: Clinical safety-critical entity.
- **Immunisation Record**: Linked with the NZ Immunisation Register.

### 💰 Finance & Compliance Entities
- **Billing / Insurance**: Funding source (ACC, private insurance, public funding via Te Whatu Ora).
- **Claim**: Lodging and tracking claims (important for ACC-related hospital visits).
- **Subsidy / PHARMAC Funding**: For prescription medication cost management.

### 📦 Resource & Infrastructure Entities
- **Bed / Ward / Room**: For inpatient management and occupancy.
- **Equipment / Asset**: Medical devices, infusion pumps, scanners, wearable integrations.
- **Supply Chain / Inventory**: Medication stock, surgical supplies, PPE.

### 🌐 Public Health & Integration Entities
- **Emergency Contact / Next of Kin**: Patient support structures.
- **Community Services / Referrals**: Links to GPs, aged care, home care.
- **Health System Integration**: FHIR endpoints, HISO-compliant APIs, NZ Health Terminology Service (SNOMED CT, LOINC, NZULM).
- **Telehealth Session**: Virtual consultation entity with secure video integration.

### 📊 Analytics, Reporting & Monitoring Entities
- **Clinical KPI / Outcome**: Mortality, readmission rates, patient satisfaction.
- **Audit Log / Compliance Record**: Mandatory for NZ Privacy Act 2020 and HISO standards.
- **Population Health Data**: For Te Whatu Ora reporting, Māori & Pacific health equity monitoring.

### 1. Patient Management System
Features:
- Comprehensive patient profiles with cultural considerations
- Integration with NZ Health Index Number (NHI)
- Family medical history tracking
- Genetic predisposition analysis
- Social determinants of health tracking

### 2. Doctor Portal & Management
AI-Enhanced Features:
- Intelligent patient prioritization
- Clinical decision support with evidence citations
- Automated differential diagnosis suggestions
- Smart template generation for common conditions

### 3. Advanced Telehealth Platform
Advanced Capabilities:
- HD video with adaptive quality
- AR-enabled examination tools
- Real-time vital sign monitoring
- AI-powered consultation quality scoring
- Automated session summarization

### 4. Intelligent Prescription System
Integration with NZ Systems:
- New Zealand Electronic Prescription Service (EPS)
- Pharmaceutical Management Agency (PHARMAC) integration
- Real-time drug availability checking
- Cost-effective medication alternatives
AI Features:
- Personalized dosage recommendations
- Drug interaction predictions
- Adherence monitoring and reminders
- Side effect prediction and monitoring

### 5. Comprehensive Analytics Dashboard
- Integrate with Power BI

### 6. Barcode Handling
- Barcode handling for Patient, Labs, Radiology, Inventory handling with other services

## Governance
<!-- Example: Constitution supersedes all other practices; Amendments require documentation, approval, migration plan -->

[GOVERNANCE_RULES]
<!-- Example: All PRs/reviews must verify compliance; Complexity must be justified; Use [GUIDANCE_FILE] for runtime development guidance -->

**Version**: [CONSTITUTION_VERSION] | **Ratified**: [RATIFICATION_DATE] | **Last Amended**: [LAST_AMENDED_DATE]
<!-- Example: Version: 2.1.1 | Ratified: 2025-06-13 | Last Amended: 2025-07-16 -->