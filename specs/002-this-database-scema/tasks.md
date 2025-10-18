# Tasks: Unified Database Schema for EChanneling

**Input**: Design documents from `/specs/002-this-database-scema/`
**Prerequisites**: plan.md (required)

## Execution Flow (main)
```
1. Load plan.md from feature directory
2. Generate tasks by category:
   → Setup: project init, dependencies, linting
   → Core: C# model classes for each entity in the schema
   → Integration: DB context, configuration, documentation
   → Polish: unit tests, docs
3. Apply task rules:
   → Different files = mark [P] for parallel
   → Same file = sequential (no [P])
   → Models before services
   → Everything before polish
4. Number tasks sequentially (T001, T002...)
5. Create parallel execution examples
6. Validate task completeness
7. Return: SUCCESS (tasks ready for execution)
```

## Format: `[ID] [P?] Description`
- **[P]**: Can run in parallel (different files, no dependencies)
- Include exact file paths in descriptions

## Phase 3.1: Setup
- [ ] T001 Create `backend/Models/` directory if not exists
- [ ] T002 [P] Ensure .NET project references for data annotations and SQL types

## Phase 3.2: Core Implementation (C# Object Models)
- [ ] T003 [P] Create `Patient` model in `backend/Models/Patient.cs`
- [ ] T004 [P] Create `User` model in `backend/Models/User.cs`
- [ ] T005 [P] Create `Doctor` model in `backend/Models/Doctor.cs`
- [ ] T006 [P] Create `Appointment` model in `backend/Models/Appointment.cs`
- [ ] T007 [P] Create `Prescription` model in `backend/Models/Prescription.cs`
- [ ] T008 [P] Create `VitalSign` model in `backend/Models/VitalSign.cs`
- [ ] T009 [P] Create `Invoice` model in `backend/Models/Invoice.cs`
- [ ] T010 [P] Create `Payment` model in `backend/Models/Payment.cs`
- [ ] T011 [P] Create `Notification` model in `backend/Models/Notification.cs`
- [ ] T012 [P] Create `Role` model in `backend/Models/Role.cs`
- [ ] T013 [P] Create `Permission` model in `backend/Models/Permission.cs`
- [ ] T014 [P] Create `AuditLog` model in `backend/Models/AuditLog.cs`
- [ ] T015 [P] Create all other models for remaining tables in `backend/Models/`

## Phase 3.3: Integration
- [ ] T016 Create or update `DbContext` for all entities in `backend/Models/EChannelingDbContext.cs`
- [ ] T017 Configure entity relationships and constraints in `EChannelingDbContext.cs`
- [ ] T018 Add XML documentation for all models and properties

## Phase 3.4: Polish
- [ ] T019 [P] Add unit tests for model validation in `backend/Tests/Models/`
- [ ] T020 [P] Add summary documentation for the schema in `docs/database-schema.md`

## Parallel Execution Guidance
- All [P] tasks in Phase 3.2 and 3.4 can be executed in parallel (different files)
- T016-T018 must be done after all models are created
- T019-T020 can be done after integration

## Dependency Notes
- Setup (T001-T002) → Models (T003-T015) [P] → Integration (T016-T018) → Polish (T019-T020) [P]

---

**Ready for execution. Each task is specific and file-scoped for LLM/agent automation.**
