using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace new_project.Authorization
{
    public class OperationAuthorizationRequirementGenerator 
    {
        private List<string> _operations;

        public OperationAuthorizationRequirementGenerator(string _name)
        {
            _operations = new List<string>
            {
                $"{_name}.Create",
                $"{_name}.View",
                $"{_name}.Update",
                $"{_name}.Delete"
            };
        }
        public string CreateOperation()
        {
            return _operations[0];
        }
        public string ViewOperation()
        {
            return _operations[1];
        }

        public List<string> GetOperations()
        {
            return _operations;
        }

        public string DeleteOperation()
        {
            return _operations[3];
        }
        public string UpdateOperation()
        {
            return _operations[2];
        }


    }
}
