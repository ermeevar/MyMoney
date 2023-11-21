using Castle.Windsor;

namespace Calculator.Start.CastleWindsorConfiguration;

/// <summary>
/// Контейнер зависимостей
/// </summary>
public static class Container
{
    /// <summary>
    /// Текущий контейнер
    /// </summary>
    public static readonly IWindsorContainer CurrentContainer;

    /// <summary>
    /// Инициализация текущего коонтейнера с инсталяцией модулей
    /// </summary>
    static Container() => 
        CurrentContainer = new WindsorContainer().Install(new ContainerInstaller());
}