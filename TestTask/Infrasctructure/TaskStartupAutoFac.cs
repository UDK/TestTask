using Autofac;
using TestTask.DB.Domain;
using TestTask.DB.Repositories;
using TestTask.Infrasctructure.Configuration;
using Task = TestTask.DB.Domain.Task;

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
            //.WithParameter("settings", builder.Configuration.GetSection("ConfigureSettings").Get<ConfigureSettings>());
            //builder.RegisterType<Repository<Task, int>>().As<IRepository<Task, int>>();
            builder.Register(q => _configure).As<ConfigureSettings>().SingleInstance();
        }
    }
}
