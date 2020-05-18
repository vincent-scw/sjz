using MediatR;
using System;
using System.Collections.Generic;

namespace SJZ.Common.Domain
{
    public class Entity<TKey>
        where TKey : IComparable
    {
        public virtual TKey Id { get; protected set; }
        public string CreatedBy { get; protected set; }
        public DateTimeOffset CreatedDate { get; protected set; }
        public string UpdatedBy { get; protected set; }
        public DateTimeOffset? UpdatedDate { get; protected set; }

        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public bool IsTransient()
        {
            return this.Id.Equals(default(TKey));
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity<TKey>) || GetType() != obj.GetType())
            {
                return false;
            }

            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            Entity<TKey> item = (Entity<TKey>)obj;

            if (item.IsTransient() || this.IsTransient())
            {
                return false;
            }

            return item.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                return Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)
            }

            return GetHashCode();
        }

        public static bool operator ==(Entity<TKey> left, Entity<TKey> right)
        {
            if (object.Equals(left, null))
            {
                return object.Equals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(Entity<TKey> left, Entity<TKey> right)
        {
            return !(left == right);
        }
    }
}
