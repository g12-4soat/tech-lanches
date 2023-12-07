namespace TechLanches.Core
{
    public abstract class Entity
    {
        protected Entity()
        {

        }

        protected Entity(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }

        private List<DomainEvent> _domainEvents;
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        public void AdicionarEventoDominio(DomainEvent domainEvent)
        {
            _domainEvents ??= new List<DomainEvent>();
            _domainEvents.Add(domainEvent);
        }

        public void RemoverEventoDominio(DomainEvent domainEvent)
        {
            _domainEvents?.Remove(domainEvent);
        }

        public void LimparEventoDominio()
        {
            _domainEvents?.Clear();
        }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
#pragma warning disable IDE0041 // Use 'is null' check
            if (ReferenceEquals(null, compareTo)) return false;
#pragma warning restore IDE0041 // Use 'is null' check

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() ^ 93) + Id.GetHashCode();
        }
    }
}