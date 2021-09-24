using Microsoft.EntityFrameworkCore.Metadata;
using new_project.Authorization;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace new_project.Models
{
    public class NewRoleViewModel 
    {
        public NewRoleViewModel() { }
        public NewRoleViewModel(IEnumerable<IEntityType> enumerable)
        {
            listOfPermissions = new List<Permission>();
            foreach (var entity in enumerable)
            {
                if(entity != null)
                {
                    var operations = new OperationAuthorizationRequirementGenerator(entity.ClrType.Name).GetOperations();
                    foreach (var operation in operations)
                    {
                        listOfPermissions.Add(new Permission(operation));
                    }
                }
            }
        }

        public string roleName { get; set; }
        public List<Permission> listOfPermissions { get; set; }
    }
}
