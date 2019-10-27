using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Common.Dates;
using StructureMap;
using StructureMap.Graph;

namespace CleanArchitecture.Specification.Common
{
    public static class IoC
    {
        public static IContainer Initialize(AppContext appContext)
        {
#pragma warning disable 618
            ObjectFactory.Initialize(x =>
#pragma warning restore 618
            {
                SetScanningPolicy(x);

                x.For<IDatabaseService>()
                    .Use(appContext.DatabaseService);

                x.For<IInventoryService>()
                    .Use(appContext.InventoryService);

                x.For<IDateService>()
                    .Use(appContext.DateService);

            });

#pragma warning disable 618
            return ObjectFactory.Container;
#pragma warning restore 618
        }

        private static void SetScanningPolicy(IInitializationExpression x)
        {
            x.Scan(scan =>
            {
                scan.AssembliesFromApplicationBaseDirectory(
                    filter => filter.FullName.StartsWith("CleanArchitecture"));

                scan.WithDefaultConventions();
            });
        }
    }
}
