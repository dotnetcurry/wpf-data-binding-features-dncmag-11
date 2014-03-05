using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCF_DataService
{
    public class Service: IService
    {
        CompanyEntities objContext;
        public Service()
        {
            objContext = new CompanyEntities(); 
        }

        public int CreateEmployee(EmployeeInfo emp)
        {
            objContext.AddToEmployeeInfoes(emp);
            objContext.SaveChanges();
            return emp.EmpNo;
        }

        public EmployeeInfo[] GetEmployees()
        {
            return objContext.EmployeeInfoes.ToArray();
        }
    }
}
