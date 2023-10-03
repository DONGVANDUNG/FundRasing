using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using esign.Authorization;

namespace esign
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(
        typeof(esignApplicationSharedModule),
        typeof(esignCoreModule)
        )]
    public class esignApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(esignApplicationModule).GetAssembly());
        }
    }
}