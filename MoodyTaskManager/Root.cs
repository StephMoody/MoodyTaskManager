using Autofac;
using MoodyTaskManager.Contract;
using MoodyTaskManager.Data;
using MoodyTaskManager.Infrastructure;
using MoodyTaskManager.View;
using MoodyTaskManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoodyTaskManager
{
    class Root
    {
        private ILifetimeScope _container;

        public async void Run()
        {
            Initialize();

            using (_container.BeginLifetimeScope())
            {
                
                await InitializeRepositories();

                TaskManagerWindowViewModel viewModel = _container.Resolve<TaskManagerWindowViewModel>();
                TaskManagerWindow taskManagerWindow = new TaskManagerWindow {DataContext = viewModel};
                InitializeUpdateItems();
                taskManagerWindow.ShowDialog();
            }
        }

        private void Initialize()
        {
            BuildUp();
        }

        private async Task InitializeRepositories()
        {
            IEnumerable<IRepositoryBase> repositories = _container.Resolve<IEnumerable<IRepositoryBase>>();
            foreach (var repository in repositories)
            {
                await repository.Initialize();
                await repository.Load();
            }
        }

        private void InitializeUpdateItems()
        {
            IEnumerable<IUpdateItem> allUpdateItems = _container.Resolve<IEnumerable<IUpdateItem>>();

            List<IIntervallTriggeredUpdateController> allUpdateController = new List<IIntervallTriggeredUpdateController>();
            foreach (IUpdateItem updateItem in allUpdateItems)
            {
                IIntervallTriggeredUpdateController taskWindowUpdateController = _container.Resolve<IIntervallTriggeredUpdateController>();
                taskWindowUpdateController.RegisterTimer(TimeSpan.FromSeconds(1));
                taskWindowUpdateController.RegisterUpdateItem(updateItem);
                allUpdateController.Add(taskWindowUpdateController);
            }

            foreach (IIntervallTriggeredUpdateController updateController in allUpdateController)
            {
                updateController.Start();
            }
        }

        private void BuildUp()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<TaskManagerWindowViewModel>().AsSelf().InstancePerLifetimeScope();
            containerBuilder.RegisterType<TaskManagerData>().AsSelf().InstancePerLifetimeScope();
            containerBuilder.RegisterType<AllTasksViewModel>().AsSelf().As<IUpdateItem>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<TaskWindowUpdateController>().As<IIntervallTriggeredUpdateController>().InstancePerDependency();
            containerBuilder.RegisterType<FilteredTasksViewModel>().AsSelf().As<IUpdateItem>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<FilteredTasksHost>().As<IFilteredTasksHost>().SingleInstance();
            containerBuilder.RegisterType<TaskManagerRepository>().As<ITaskManagerRepository>().As<IRepositoryBase>().SingleInstance();

            System.Monitoring.Builder.BuildUp(containerBuilder);

            _container = containerBuilder.Build();
        }
    }
}
