using Autofac;
using System.Monitoring.Contract;
using System.Monitoring.Process;

namespace System.Monitoring
{
    public static class Builder
    {
        public static void BuildUp(ContainerBuilder containerBuilder )
        {
            containerBuilder.RegisterType<ProcessDataProvider>().As<IProcesDataProvider>().SingleInstance();
        }
    }
}
