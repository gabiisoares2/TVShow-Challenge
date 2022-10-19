using System;
namespace TVShow.Infrastructure.Audit
{
	public sealed class AuditEntrySaveChangesEvent
	{
		public AuditEntry Entry { get; set; }
	}
}

