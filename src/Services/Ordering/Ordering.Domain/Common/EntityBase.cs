using System;

namespace Ordering.Domain.Common
{
    public abstract class EntityBase
    {
        public int Id { get; protected set; }
        public string CreatedBy { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public string LastModifiedBy { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }
    }
}