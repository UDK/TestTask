using Autofac;
using MongoDB.Bson;
using TestTask.DB.Domain;
using TestTask.DB.Repositories;
using TestTask.Infrasctructure.Configuration;
using TaskDomain = TestTask.DB.Domain.Task;

namespace TestTask.Infrasctructure
{
    public class TaskStartupAutoFac : Module
    {
        private readonly ConfigureSettings _configure;

        public TaskStartupAutoFac(ConfigureSettings configure)
        {
            _configure = configure;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Repository<,>)).As(typeof(IRepository<,>)).InstancePerLifetimeScope();
            builder.Register(q => _configure).As<ConfigureSettings>().SingleInstance();
        }
    }
}
