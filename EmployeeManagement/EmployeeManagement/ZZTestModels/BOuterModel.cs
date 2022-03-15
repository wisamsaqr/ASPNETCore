using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ZZTestModels
{
    public class BOuterModel
    {
        public string OuterModelProp { get; set; }
        public BInnerModel InnerModel { get; set; }
    }
}