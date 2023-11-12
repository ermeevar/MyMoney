using Calculator.Extensions;
using Calculator.Services.Abstractions;
using Calculator.Services.Implementations;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Calculator.Start.CastleWindsorConfiguration;

/// <summary>
/// Инсталяция зависимостей
/// </summary>
public class ContainerInstaller : IWindsorInstaller
{
    /// <inheritdoc/>
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
        container.RegisterSingleton<IVacationPaysService, VacationPaysService>();
    }
}