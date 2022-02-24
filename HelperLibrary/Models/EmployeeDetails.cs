using HelperLibrary.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelperLibrary.Models
{
    public class EmployeeDetails : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public Company Company { get; set; }
    }
}
