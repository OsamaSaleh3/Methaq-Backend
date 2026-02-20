using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; private set; }

        protected BaseEntity() => Id = Guid.NewGuid();

        public void MarkAsUpdated() => UpdatedAt = DateTime.UtcNow;
    }
}
