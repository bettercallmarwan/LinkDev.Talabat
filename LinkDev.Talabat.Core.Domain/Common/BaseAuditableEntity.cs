﻿namespace LinkDev.Talabat.Core.Domain.Common
{
    public abstract class BaseAuditableEntity<TKey> : BaseEntity<TKey> 
        where TKey : IEquatable<TKey>
    {
        public required string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } 
        public required string LastModifiedBy { get; set; } 
        public DateTime LastModifiedOn { get; set; } 


    }
}
