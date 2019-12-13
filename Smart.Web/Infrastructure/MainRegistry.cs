using System;
using System.Linq;
using System.Reflection;
using MySql.Data.MySqlClient;
using Smart.Web.Infrastructure;
using StructureMap;

namespace Smart.Web.Infrastructure
{
    public class MainRegistry : Registry
    {
        public MainRegistry()
        {
            this.DataAccess();
            this.Infrastructure();
        }

        private void DataAccess()
        {
            this.For<MySqlConnection>()
                .Use("MySql connection.", ctx => new MySqlConnection(Global.AppConfig.ConnectionString))
                .ContainerScoped();

            this.For<OVKDb>().Use<OVKDb>();
        }

        private void Infrastructure()
        {
            this.For<MainLogger>().Singleton();

            // Handlers
            foreach (var handler in Global.Handlers.GetAllHandlers())
            {
                this.For(handler.HandlerType).ContainerScoped();
            }

            // Services
            var serviceTypes = Assembly.GetExecutingAssembly().DefinedTypes.Select(info => info.AsType())
                                .Where(type => type.IsClass && type.Name.EndsWith("Service"))
                                .ToList();

            foreach (Type type in serviceTypes)
            {
                this.For(type).ContainerScoped();
            }
        }
    }
}