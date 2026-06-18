namespace MyProjectService.Domain
{
 
    public record Name
    {
        public string Value { get; }

        private Name(string value)
        {
            Value = value;
        }
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
         public Guid parentId { get; private set; }  
         public Name Name { get; private set; }         
    
         public string slug { get; private set; }
                
         public string path { get; private set; }    
          
         public DateTime createdAt { get; private set; }

         public DateTime updatedAt { get; private set; }
    
        public IReadOnlyList<DepartmentLocation> DepartmentLocations => _departmentlocations;
        public IReadOnlyList<DepartmentPosition> DepartmentPositions => _departmentpositions;
    
        public Department(Guid id, Guid? parentId, Name name, string slug, string parentPath)
        {
            Id = id;
            this.parentId = parentId ?? Guid.Empty;
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.slug = slug;
            path = string.IsNullOrEmpty(parentPath) ? slug : $"{parentPath}/{slug}";
            createdAt = DateTime.UtcNow;
            updatedAt = DateTime.UtcNow;
            _departmentlocations = new List<DepartmentLocation>();
            _departmentpositions = new List<DepartmentPosition>();
        }
    }

    public class Location
    {
         public Guid Id { get; }
         public Name Name { get; private set; }         
             
         public Address address { get; private set; }

         public DateTime createdAt { get; private set; }

         public DateTime updatedAt { get; private set; }
    
         public Location(Guid id, Name name, Address address)
         {
             Id = id;
             this.Name = name ?? throw new ArgumentNullException(nameof(name));
             this.address = address ?? throw new ArgumentNullException(nameof(address));
             createdAt = DateTime.UtcNow;
             updatedAt = DateTime.UtcNow;
         }
    
    }
    
    public class Position
    {
         public Guid Id { get; }          
         public Name Name { get; private set; }         
             
         public DateTime createdAt { get; private set; }

         public DateTime updatedAt { get; private set; }

        public Position(Guid id, Name name)
        {
            Id = id;
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            createdAt = DateTime.UtcNow;
            updatedAt = DateTime.UtcNow;
        }
    
    
    }

    public class DepartmentLocation
    {
         public Guid Id { get; }
        
         public Guid departmentId { get; }
        
         public Guid locationId { get; }

         public bool isPrimary { get; private set; }    
    
        public DepartmentLocation(Guid id, Guid departmentId, Guid locationId, bool isPrimary)
        {
            Id = id;
            this.departmentId = departmentId;
            this.locationId = locationId;
            this.isPrimary = isPrimary;
        }
    }
  
    public class DepartmentPosition
    {
         public Guid Id { get; }
          
         public Guid departmentId { get; }
        
         public Guid positionId { get; }
   
            public DepartmentPosition(Guid id, Guid departmentId, Guid positionId)
            {
                Id = id;
                this.departmentId = departmentId;
                this.positionId = positionId;
            }
    }
}
    

