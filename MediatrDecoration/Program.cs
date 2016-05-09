using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using StructureMap;
using StructureMap.Graph;

namespace MediatrDecoration
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(cfg => cfg.Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.AddAllTypesOf(typeof(IRequestHandler<,>));
                scanner.AddAllTypesOf(typeof(INotificationHandler<>));

                cfg.For(typeof(IRequestHandler<,>)).DecorateAllWith(typeof(LoggingDecorator<,>));

                cfg.For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
                cfg.For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
                cfg.For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));
                cfg.For<IMediator>().Use<Mediator>();
            }));

            var mediator = container.GetInstance<IMediator>();

            var s = mediator.Send(new Command());
        }
    }
}
