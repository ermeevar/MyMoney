using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Calculator.Extensions;

/// <summary>
/// Расширение для <see cref="WindsorContainer"/>
/// </summary>
public static class WindsorContainerExtension
{
    /// <summary>Зарегистрировать компонент как singleton</summary>
    /// <typeparam name="TInterface">Абстракция</typeparam>
    /// <typeparam name="TImplementation">Имплементация</typeparam>
    public static void RegisterSingleton<TInterface, TImplementation>(this IWindsorContainer container)
        where TInterface : class
        where TImplementation : TInterface
        => container.Register(Component.For<TInterface>().ImplementedBy<TImplementation>().LifestyleSingleton());
}