using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF45_DataBinding.MyRef;

namespace WPF45_DataBinding
{
    /// <summary>
    /// Interaction logic for MainWindows_Update_Collection_UI.xaml
    /// </summary>
    public partial class MainWindows_Update_Collection_UI : Window
    {

        MyRef.ServiceClient Proxy;

        public MainWindows_Update_Collection_UI()
        {
            InitializeComponent();
            BindingOperations.EnableCollectionSynchronization(NewEmployees, lockObject);
            Proxy = new MyRef.ServiceClient(); 
        }


            private void btngetdata_Click(object sender, RoutedEventArgs e)
            {
                try
                {
                    //UpdatingOlderWay();
                    UpdatingUsingWPF45();
                    dgemp.ItemsSource = NewEmployees;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message); 
                }
            }



        /// <summary>
        /// The method shows the use of Task class
        /// to update the DataGrid on UI after making call to
        /// the WCF Service
        /// </summary>
        void UpdatingOlderWay()
        {
           // CancellationToken ct = new CancellationToken();

            Task taskGetEmployees = Task.Factory.StartNew<ObservableCollection<EmployeeInfo>>((obj) =>
            {
                ObservableCollection<EmployeeInfo> Employees = new ObservableCollection<EmployeeInfo>();
                var Emps = Proxy.GetEmployees();

                foreach (var item in Emps)
                {
                    Employees.Add(item);
                }
                return Employees;
            }, CancellationToken.None).ContinueWith(emp => {
                dgemp.ItemsSource = emp.Result;
            },TaskScheduler.FromCurrentSynchronizationContext());
        }


        //Defining the lock object used for Synchronization
        private static object lockObject = new object();
        //Defining the Collection object to receive data from external service
        ObservableCollection<EmployeeInfo> NewEmployees = new ObservableCollection<EmployeeInfo>();
        


        /// <summary>
        /// The below method will demonstrate the mechanism
        /// of accessing the collection on non-UI thread
        /// on the UI to Update the UI
        /// </summary>
        void UpdatingUsingWPF45()
        {
            Task taskGetEmployees = Task.Factory.StartNew<ObservableCollection<EmployeeInfo>>((obj) =>
            {
                var Emps = Proxy.GetEmployees();

                foreach (var item in Emps)
                {
                    NewEmployees.Add(item);
                }
                return NewEmployees;
            },null);
        }
    }
}
