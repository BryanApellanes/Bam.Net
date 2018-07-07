using Bam.Net.Incubation;

namespace Bam.Net.CoreServices
{
    /// <summary>
    /// Fluent entry point to FluentIncubationContext
    /// </summary>
    public static class Requesting
    {
        public static FluentIncubationContext<I> A<I>()
        {
            return new FluentIncubationContext<I>();
        }

        /// <summary>
        /// Bind the specified type I ( same as Bind )
        /// </summary>
        /// <typeparam name="I"></typeparam>
        /// <param name="serviceRestry"></param>
        /// <returns></returns>
        public static FluentIncubationContext<I> AskingFor<I>(this ServiceRegistry serviceRestry)
        {
            return new FluentIncubationContext<I>(serviceRestry);
        }

        public static FluentIncubationContext<I> For<I>(this ServiceRegistry serviceRegistry)
        {
            return new FluentIncubationContext<I>(serviceRegistry);
        }

        public static FluentCtorContext<I> ForCtor<I>(this ServiceRegistry serviceRegistry, string parameterName)
        {
            return new FluentCtorContext<I>(serviceRegistry, parameterName);
        }

        /// <summary>
        /// Bind the specified type I ( same as AskingFor )
        /// </summary>
        /// <typeparam name="I"></typeparam>
        /// <param name="serviceRegistry"></param>
        /// <returns></returns>
        public static FluentIncubationContext<I> Bind<I>(this ServiceRegistry serviceRegistry)
        {
            return new FluentIncubationContext<I>(serviceRegistry);
        }
    }
}
