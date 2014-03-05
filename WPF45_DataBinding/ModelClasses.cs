using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;


namespace WPF45_DataBinding
{

    /// <summary>
    /// The model class implementing the INotifyDataErrorInfo interface and INotifyPropertyChanged
    /// </summary>
    public class Employee : INotifyDataErrorInfo, INotifyPropertyChanged
    {
        private Dictionary<string, List<string>> modelerrors = new Dictionary<string, List<string>>();

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The ptoperty changed
        /// </summary>
        /// <param name="pName"></param>
        void onPropertyChanged(string pName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(pName));
                PerformValidation();
            }
        }

        int _EmpNo;

        public int EmpNo
        {
            get { return _EmpNo; }
            set
            {
                _EmpNo = value;
                onPropertyChanged("EmpNo");
            }
        }
        string _EmpName;

        public string EmpName
        {
            get { return _EmpName; }
            set
            {
                _EmpName = value;
                onPropertyChanged("EmpName");

            }
        }
        decimal _Salary;

        public decimal Salary
        {
            get { return _Salary; }
            set
            {
                _Salary = value;
                onPropertyChanged("Salary");

            }
        }
        string _DeptName;

        public string DeptName
        {
            get { return _DeptName; }
            set { _DeptName = value; }
        }
        string _Designation;

        public string Designation
        {
            get { return _Designation; }
            set { _Designation = value; }
        }

        /// <summary>
        /// Method to get errors on a specific property.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            List<string> errorsForProperties = new List<string>();
            if (propertyName != null)
            {
                modelerrors.TryGetValue(propertyName, out errorsForProperties);
                return errorsForProperties;
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// The Error Info on the object if any
        /// </summary>
        public bool HasErrors
        {
            get {
                try
                {
                    var errInfo = modelerrors.Values.FirstOrDefault(rec => rec.Count > 0);
                    if (errInfo != null)
                        return true;
                    else
                        return false;
                }
                catch
                {
                }
                return true;
            }
        }

        

        /// <summary>
        /// Perform Validation Asynchronously
        /// </summary>
        private void PerformValidation()
        {
            Task taskasyncvalidate = new Task(() => DataValidation());
            taskasyncvalidate.Start();
        }

        /// <summary>
        /// Logic for Validation
        /// </summary>
        private void DataValidation()
        {
            //Validation for EmpName
            List<string> empNameErrors;
            if (modelerrors.TryGetValue("EmpName", out empNameErrors) == false)
            {
                empNameErrors = new List<string>();
            }
            else
            {
                empNameErrors.Clear();
            }

            if (String.IsNullOrEmpty(EmpName))
            {
                empNameErrors.Add("The EmpName can't be null or empty.");
            }
            modelerrors["EmpName"] = empNameErrors;

            if (empNameErrors.Count > 0)
            {
                PropertyErrorsChanged("EmpName");
            }
            //Ends Here

            //Validations for Salary

            List<string> salErrors;
            if (modelerrors.TryGetValue("Salary", out salErrors) == false)
            {
                salErrors = new List<string>();
            }
            else
            {
                salErrors.Clear();
            }
            if (Salary <= 0 || Salary>70000)
            {
                salErrors.Add("The Salary must be greater than zero. and less than 70000");
            }
            else
            {
                salErrors.Clear();
            }
            modelerrors["Salary"] = salErrors;
            if (salErrors.Count > 0)
            {
                PropertyErrorsChanged("Salary");
            }
            //Ends Here
        }



        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        /// <summary>
        /// The ErrorChaged handler which will be on each property.
        /// </summary>
        /// <param name="propertyName"></param>
        public void PropertyErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null) {
                var evtargs = new DataErrorsChangedEventArgs(propertyName);
                ErrorsChanged.Invoke(this, evtargs);
            }
        }
    }
}