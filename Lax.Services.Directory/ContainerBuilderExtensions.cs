using Autofac;

namespace Lax.Services.Directory {

    public static class ContainerBuilderExtensions {

        public static ContainerBuilder RegisterDirectoryServices(this ContainerBuilder builder,
            params string[] ldapConnectionStrings) {
            builder.Register(context => new DirectoryServicesOptions {
                LdapConnectionStrings = ldapConnectionStrings
            }).As<DirectoryServicesOptions>().InstancePerDependency();

            builder.RegisterType<DirectoryServices>().As<IDirectoryServices>().InstancePerDependency();

            return builder;
        }

    }

}