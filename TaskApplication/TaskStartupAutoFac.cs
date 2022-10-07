using Autofac;
using TaskApplication.Application.CreateTask;
using TaskApplication.Application.DeleteTask;
using TaskApplication.Application.GetAllTasks;
using TaskApplication.Application.UpdateTask;
using TaskApplication.DB.Repositories;
using TaskInfrastructure.Configuration;
using TaskDomain = TaskApplication.DB.Domain.Task;

namespace TaskApplication;

public class TaskModule : Module
{
    private readonly ConfigureSettings _configure;

    public TaskModule(ConfigureSettings configure)
    {
        _configure = configure;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(Repository<,>)).As(typeof(IRepository<,>)).InstancePerLifetimeScope();
        builder.Register(q => _configure).As<ConfigureSettings>().SingleInstance();

        builder.RegisterType<GetAllTasksQuery>().AsImplementedInterfaces();
        builder.RegisterType<GetAllTasksHandler>().AsImplementedInterfaces();
        builder.RegisterType<CreateTaskCommand>().AsImplementedInterfaces();
        builder.RegisterType<CreateTaskHandler>().AsImplementedInterfaces();
        builder.RegisterType<DeleteTaskCommand>().AsImplementedInterfaces();
        builder.RegisterType<DeleteTaskHandler>().AsImplementedInterfaces();
        builder.RegisterType<UpdateTaskCommand>().AsImplementedInterfaces();
        builder.RegisterType<UpdateTaskHandler>().AsImplementedInterfaces();
    }
}
