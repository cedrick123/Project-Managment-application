using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_project.Models
{
    public interface Repository<T> where T : class
    {
        public List<T> GetAll();
        public void Create(T t);
        public T Read(int id);
        public void Delete(T t);
        public void Update(T t);
    }
}
