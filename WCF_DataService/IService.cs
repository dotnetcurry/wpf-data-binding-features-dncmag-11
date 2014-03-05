using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCF_DataService
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        int CreateEmployee(EmployeeInfo emp);
        [OperationContract]
        EmployeeInfo[] GetEmployees();
    }

}
