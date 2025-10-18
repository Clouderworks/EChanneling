// Removed self-import to resolve TS2440 conflicts

export interface PatientRegistration {
  id: string;
  nhiNumber: string;
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  gender: string;
  ethnicity: string;
  language: string;
  address: string;
  phone: string;
  email: string;
  familyHistory: FamilyHistoryEntry[];
  geneticRisks: GeneticRiskEntry[];
  socialDeterminants: SocialDeterminants;
}

export interface FamilyHistoryEntry {
  relation: string;
  condition: string;
  notes?: string;
}

export interface GeneticRiskEntry {
  gene: string;
  riskLevel: string;
  notes?: string;
}

export interface SocialDeterminants {
  occupation?: string;
  education?: string;
  housing?: string;
  incomeLevel?: string;
  maritalStatus?: string;
  notes?: string;
}
