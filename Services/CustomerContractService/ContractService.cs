using new_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_project.Services.CustomerContractService
{
    public interface ContractService
    {
        public byte[] 
            GeneratePdfContract(Project project);
    }
}
