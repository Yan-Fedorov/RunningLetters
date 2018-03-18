using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningLetters
{
    public class IoCBuilder
    {
        public static IContainer Building()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Logic>()
                .AsSelf().AsImplementedInterfaces()
                .SingleInstance();
            builder.RegisterType<RunningLetter>()
                .AsSelf().AsImplementedInterfaces()
                .SingleInstance();
            builder.RegisterType<UserInteraction>().AsSelf().SingleInstance();
            builder.RegisterType<GameDataService>().AsSelf().SingleInstance();
            builder.RegisterType<CheckCollision>().AsSelf().SingleInstance();

            return builder.Build();
        }
    }
}

