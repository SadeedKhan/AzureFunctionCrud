using HelperLibrary.Models;
using HelperLibrary.Repositories.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelperLibrary.Repositories.Interfaces
{
    public interface IEmployeeRepository : IRepository<EmployeeDetails>
    {
    }
}
