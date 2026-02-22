    
namespace ProjectModel;

    public class Project
    {
        public int Id { get; set; }
        public string? Name { get; set; }


        public Project() { }
        public Project(string name)
        {
        Name = name;
        }

    public override string ToString()
    {
        return Name;
    }
    }

