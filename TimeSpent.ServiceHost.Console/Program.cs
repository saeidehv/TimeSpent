using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SM = System.ServiceModel;
using TimeSpent.Business.Bootstrapper;
using TimeSpent.Business.Managers;
using System.Security.Principal;
using System.Threading;
using TimeSpent.Common;
using TimeSpent.Core;
using System.Transactions;

namespace TimeSpent.ServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            // principal for unattended process ( EX: time_elasped)
            GenericPrincipal principal = new GenericPrincipal(new GenericIdentity("Radin"), new string[] { Security.TimeSpentAdminRole});
            Thread.CurrentPrincipal = principal;

            ObjectBase.Container = MEFLoader.Init();

            Console.WriteLine("Starting up services ...");
            Console.WriteLine("");

            System.Timers.Timer timer = new System.Timers.Timer(60000);
            timer.Elapsed += Timer_Elapsed;
       //     timer.Start();

            SM.ServiceHost hostTimeEntryManager = new SM.ServiceHost(typeof(TimeEntryManager));
            SM.ServiceHost hostCategoryManager = new SM.ServiceHost(typeof(CategoryManager));
            SM.ServiceHost hostProjectManager = new SM.ServiceHost(typeof(ProjectManager));
            SM.ServiceHost hostAccountManager = new SM.ServiceHost(typeof(AccountManager));

            StartService(hostTimeEntryManager, "TimeEntryManager");
            StartService(hostCategoryManager, "CategoryManager");
            StartService(hostProjectManager, "ProjectManager");
            StartService(hostAccountManager, "AccountManager");

            Console.WriteLine("");
            Console.WriteLine("Press [enter] to exit.");
            Console.ReadLine();

            StopService(hostTimeEntryManager, "TimeEntryManager");
            StopService(hostCategoryManager, "CategoryManager");
            StopService(hostProjectManager, "ProjectManager");
            StopService(hostAccountManager, "AccountManager");
        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            TimeEntryManager manager = new TimeEntryManager();

            using (TransactionScope scop = new TransactionScope())
            {
                try
                {
                    scop.Complete();
                }
                catch(Exception exp)
                {
                    Console.WriteLine($"There was an exception {exp.Message}");
                }
            }
        }

        static void StartService(SM.ServiceHost host, string serviceDescription)
        {
            host.Open();
            Console.WriteLine($"Service {serviceDescription} is starting ...");

            foreach(var endpoint in host.Description.Endpoints )
            {
                Console.WriteLine("Listening on endpoint ");
                Console.WriteLine($"Address {endpoint.Address.Uri}" );
                Console.WriteLine($"Binding {endpoint.Binding.Name}");
                Console.WriteLine($"Contract {endpoint.Contract.Name}");

            }
            Console.WriteLine("");
        }

        static void StopService( SM.ServiceHost host , string serviceDescription)
        {
            host.Close();
            Console.WriteLine($"Service {serviceDescription} stopped ");
        }
    }
}
