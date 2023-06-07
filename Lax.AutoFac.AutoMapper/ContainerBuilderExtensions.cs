using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using AutoMapper;
using NodaTime;
using NodaTime.Extensions;

namespace Lax.AutoFac.AutoMapper {

    public static class ContainerBuilderExtensions {

        public static ContainerBuilder RegisterAutoMapperProfiles(this ContainerBuilder builder,
            params Assembly[] assemblies) {
            //register all profile classes in the calling assembly

            builder.RegisterAssemblyTypes(assemblies).As<Profile>().InstancePerDependency();

            builder.Register(context => new MapperConfiguration(cfg => {
                cfg.CreateMap<DateTime, LocalDateTime>().ConvertUsing(_ => _.ToLocalDateTime());
                cfg.CreateMap<DateTime, LocalDate>().ConvertUsing(_ => _.ToLocalDateTime().Date);
                cfg.CreateMap<DateTime, LocalTime>().ConvertUsing(_ => _.ToLocalDateTime().TimeOfDay);

                foreach (var profile in context.Resolve<IEnumerable<Profile>>()) {
                    cfg.AddProfile(profile);
                }
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();

            return builder;
        }

    }

}