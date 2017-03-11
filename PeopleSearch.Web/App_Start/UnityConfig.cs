using System.Web.Mvc;
using Microsoft.Practices.Unity;
using People.Model.Config;
using People.Model.Service;
using Unity.Mvc5;

namespace PeopleSearch.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<PersonDbContext>(new PerResolveLifetimeManager());
            container.RegisterType<IPersonService, PersonService>();
            container.RegisterType<IInterestService, InterestService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}