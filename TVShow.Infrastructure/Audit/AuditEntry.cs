using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using TVShow.Infrastructure.Audit.Enum;

namespace TVShow.Infrastructure.Audit
{
    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }
        public Guid ScopeId { get; set; }
        public EntityEntry Entry { get; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string EntityName { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public AuditTypeEnum AuditType { get; set; }
        public List<string> ChangedColumns { get; } = new List<string>();
        public string FunctionName { get; set; }

        public Audit ToAudit()
        {
            var audit = new Audit();
            audit.Id = Guid.NewGuid();
            audit.UserId = UserId;
            audit.UserName = UserName;
            audit.Type = AuditType.ToString();
            audit.EntityName = EntityName;
            audit.FunctionName = FunctionName;
            audit.DateTime = DateTime.Now;
            audit.PrimaryKey = JsonConvert.SerializeObject(KeyValues,new Newtonsoft.Json.Converters.StringEnumConverter());
            audit.OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues, new Newtonsoft.Json.Converters.StringEnumConverter());
            audit.NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues, new Newtonsoft.Json.Converters.StringEnumConverter());
            audit.AffectedColumns = ChangedColumns.Count == 0 ? null : JsonConvert.SerializeObject(ChangedColumns, new Newtonsoft.Json.Converters.StringEnumConverter());
            return audit;
        }

        public static AuditEntry Create(EntityEntry entry)
        {
            var auditEntry = new AuditEntry(entry);
            auditEntry.EntityName = entry.Entity.GetType().Name;

            foreach (var property in entry.Properties)
            {
                string propertyName = property.Metadata.Name;
                if (property.Metadata.IsPrimaryKey())
                {
                    auditEntry.KeyValues[propertyName] = property.CurrentValue;
                    continue;
                }
                switch (entry.State)
                {
                    case EntityState.Added:
                        auditEntry.ChangedColumns.Add(propertyName);
                        auditEntry.AuditType = AuditTypeEnum.Incluir;
                        auditEntry.NewValues[propertyName] = property.CurrentValue;
                        break;
                    case EntityState.Deleted:
                        auditEntry.AuditType = AuditTypeEnum.Excluir;
                        auditEntry.OldValues[propertyName] = property.OriginalValue;
                        break;
                    case EntityState.Modified:
                        if (property.IsModified)
                        {
                            auditEntry.ChangedColumns.Add(propertyName);
                            auditEntry.AuditType = AuditTypeEnum.Alterar;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                        }
                        else
                        {
                            auditEntry.NewValues[propertyName] = property.OriginalValue;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                        }
                        break;
                }
            }

            return auditEntry;
        }
    }

    public class Audit
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Type { get; set; }
        public string EntityName { get; set; }
        public string FunctionName { get; set; }
        public DateTime DateTime { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string AffectedColumns { get; set; }
        public string PrimaryKey { get; set; }
    }
}

