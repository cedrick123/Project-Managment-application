namespace new_project.Models
{
    public class Permission
    {
        public bool selected
        {
            get; set;
        }
        public string name
        {
            get; set;
        }

        public Permission() { }
        public Permission(string name)
        {
            this.name = name;
        }
    }
}