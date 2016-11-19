//using System;
//using System.Diagnostics;
//using Castle.DynamicProxy;
//using EPiServer.Data;
//using EPiServer.DataAbstraction;
//using EPiServer.DataAccess;
//using EPiServer.Framework;
//using EPiServer.Framework.Initialization;
//using EPiServer.Scheduler;
//using EPiServer.ServiceLocation;
//using StructureMap.Pipeline;
//using InitializationModule = EPiServer.Web.InitializationModule;

//namespace TechFellow.ScheduledJobOverview
//{
//    [ModuleDependency(typeof(InitializationModule))]
//    [InitializableModule]
//    public class InitializeModule : IConfigurableModule
//    {
//        public void Initialize(InitializationEngine context) { }

//        public void Uninitialize(InitializationEngine context) { }

//        public void ConfigureContainer(ServiceConfigurationContext context)
//        {
//            try
//            {
//                var dp = new ProxyGenerator();
//                var target = context.Container.GetInstance<ScheduledJobRepository>();
//                var interceptor = new StatisticsInterceptor();

//                var newInstance = dp.CreateClassProxyWithTarget(target, interceptor);

//                context.Container.Inject(newInstance);

//                context.Container.Configure(config =>
//                                            {
//                                                //config.For<ScheduledJobRepository>().Use(newInstance);
//                                                //config.For<ISchedulerService>(Lifecycles.Singleton).Use<CustomerSchedulerService>();
//                                                //config.For<SchedulerDB>().Use<CustomerSchedulerDB>();
//                                            });
//            }
//            catch (Exception e)
//            {
                
//            }
//        }
//    }

//    public class StatisticsInterceptor : IInterceptor {
//        public void Intercept(IInvocation invocation)
//        {
//            invocation.Proceed();
//        }
//    }

//    //public class CustomerSchedulerDB : SchedulerDB {
//    //    public CustomerSchedulerDB(IDatabaseHandler databaseHandler) : base(databaseHandler) { }

//    //    public new void ReportExecuteItem(Guid id, int status, string text)
//    //    {
//    //        base.ReportExecuteItem(id, status, text);
//    //    }
//    //}

//    //public class CustomerSchedulerService : SchedulerService
//    //{
//    //    internal static Injected<ServiceAccessor<ScheduledJobRepository>> Repository { get; set; }

//    //    protected override void ExecuteJob()
//    //    {
//    //        var sw = new Stopwatch();

//    //        sw.Start();
//    //        base.ExecuteJob();
//    //        sw.Stop();

//    //        var duration = sw.ElapsedMilliseconds;

//    //        using (var ctx = new ScheduledJobsStatisticsContext())
//    //        {
//    //            ctx.ScheduledJobsStatistics.Add(new ScheduledJobsStatisticsEntry
//    //                                            {
//    //                                                JobId = CurrentScheduledItem.ID,
//    //                                                Name = "Unknown",
//    //                                                DurationInMilliseconds = duration,
//    //                                                ExecutedAt = DateTime.UtcNow
//    //                                            });

//    //            ctx.SaveChanges();
//    //        }
//    //    }
//    //}
//}
