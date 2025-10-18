export interface AuditLog {
  timestamp: string;
  user: string;
  action: string;
  entity: string;
  entityId: string;
  details?: string;
}
