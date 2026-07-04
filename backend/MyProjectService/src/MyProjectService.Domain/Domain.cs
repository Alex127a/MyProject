namespace MyProjectService.Domain
{
 
    public record Name
    {
        public string Value { get; }

        private Name(string value)
        {
            Value = value;
        }
        
        private Name() : this(string.Empty) { }
        
        public static Name Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Имя не может быть пустым.", nameof(value));
            }
            return new Name(value);
    
        }    
    }
    
    public record Address
    {
        public string Value { get; }

        private Address() : this(string.Empty) { }
        private Address(string value)
        {
            Value = value;
        }

        public static Address Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Адрес не может быть пустым.", nameof(value));
            }

            return new Address(value);   
        }    
    }
        
    
    public class Department
    {
         
         private readonly List<DepartmentLocation> _departmentlocations;
         private readonly List<DepartmentPosition> _departmentpositions;
         public Guid Id { get; }
         public Guid ParentId { get; private set; }  
         public Name Name { get; private set; }         
    
         public string Slug { get; private set; }
                
         public string Path { get; private set; }    
          
         public DateTime CreatedAt { get; private set; }

         public DateTime UpdatedAt { get; private set; }
    
        public IReadOnlyList<DepartmentLocation> DepartmentLocations => _departmentlocations;
        public IReadOnlyList<DepartmentPosition> DepartmentPositions => _departmentpositions;
          
          
           
            public Department(Guid id, Guid? parentId, Name name, string slug, string parentPath)
        {
            Id = id;
            this.ParentId = parentId ?? Guid.Empty;
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Slug = slug;
            this.Path = string.IsNullOrEmpty(parentPath) ? slug : $"{parentPath}/{slug}";
            this.CreatedAt = DateTime.UtcNow;
            this.UpdatedAt = DateTime.UtcNow;
            _departmentlocations = new List<DepartmentLocation>();
            _departmentpositions = new List<DepartmentPosition>();
        }
        private Department() { } 
        
    }
    public class Location
    {
         public Guid Id { get; }
         public Name Name { get; private set; }         
             
         public Address Address { get; private set; }

         public DateTime CreatedAt { get; private set; }

         public DateTime UpdatedAt { get; private set; }
    
         public Location(Guid id, Name name, Address address)
         {
             Id = id;
             this.Name = name ?? throw new ArgumentNullException(nameof(name));
             this.Address = address ?? throw new ArgumentNullException(nameof(address));
             this.CreatedAt = DateTime.UtcNow;
             this.UpdatedAt = DateTime.UtcNow;
         }
         private Location() { }
        
    
    
    
    }
    
    public class Position
    {
         public Guid Id { get; }          
         public Name Name { get; private set; }         
             
         public DateTime CreatedAt { get; private set; }

         public DateTime UpdatedAt { get; private set; }

        public Position(Guid id, Name name)
        {
            Id = id;
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.CreatedAt = DateTime.UtcNow;
            this.UpdatedAt = DateTime.UtcNow;
        }
         private Position() { }
         
    }

    public class DepartmentLocation
    {
         public Guid Id { get; }
        
         public Guid DepartmentId { get; }
        
         public Guid LocationId { get; }

         public bool IsPrimary { get; private set; }    
    
        public DepartmentLocation(Guid id, Guid departmentId, Guid locationId, bool isPrimary)
        {
            Id = id;
            this.DepartmentId = departmentId;
            this.LocationId = locationId;
            this.IsPrimary = isPrimary;
        } 
       private DepartmentLocation() { }
    }
  
    public class DepartmentPosition
    {
         public Guid Id { get; }
          
         public Guid DepartmentId { get; }
        
         public Guid PositionId { get; }
   
            public DepartmentPosition(Guid id, Guid departmentId, Guid positionId)
            {
                Id = id;
                this.DepartmentId = departmentId;
                this.PositionId = positionId;
            }
         private DepartmentPosition() { }
    
    }
}
  

