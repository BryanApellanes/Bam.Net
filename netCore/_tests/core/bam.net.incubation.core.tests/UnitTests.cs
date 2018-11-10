using Bam.Net.Incubation;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;

namespace Bam.Net.Tests
{
    [Serializable]
    public class UnitTests : CommandLineTestInterface
    {

        public class Primate
        {
        }

        public class Monkey : Primate
        {
        }

        public class Gorilla : Monkey
        {
            public Gorilla(IFruit fruit)
            {
                this.Fruit = fruit;
            }

            public IFruit Fruit { get; private set; }
        }
        public interface IFruit
        { }
        public class Banana : IFruit
        { }
        public class Apple : IFruit
        { }

        interface IVegetable
        { }

        public class Tomato : IFruit
        { }

        public abstract class Eater
        {
            public Eater(IFruit food)
            {
                Food = food;
            }
            public IFruit Food { get; set; }
        }

        [UnitTest]
        public static void IncubatorShouldGiveMeWhatISet()
        {
            Incubator i = new Incubator();
            i.Set(typeof(Primate), new Monkey());
            Primate m = i.Get<Primate>();
            Expect.IsTrue(m.GetType() == typeof(Monkey));
        }

        [UnitTest]
        public static void IncubatorShouldGiveMeWhatISet2()
        {
            Incubator i = new Incubator();
            i.Set<Primate>(new Monkey());
            Primate m = i.Get<Primate>();
            Expect.IsTrue(m.GetType() == typeof(Monkey));
        }

        [UnitTest]
        public static void IncubatorShouldTakeAFuncAndReturnResult()
        {
            Incubator i = new Incubator();
            Func<Primate> f = () => { return new Monkey(); };
            i.Set(typeof(Primate), f);
            Primate m = i.Get<Primate>();
            Expect.IsTrue(m.GetType() == typeof(Monkey));
        }

        [UnitTest]
        public static void IncubatorShouldTakeAFuncAndReturnResult2()
        {
            Incubator i = new Incubator();
            i.Set<Primate>(() => { return new Monkey(); });
            Primate m = i.Get<Primate>();
            Expect.IsTrue(m.GetType() == typeof(Monkey));
        }

        [UnitTest]
        public static void IncubatorShouldTakeAFuncAndReturnByClassName()
        {
            Incubator i = new Incubator();
            i.Set<Primate>(() => { return new Monkey(); });
            object m = i.Get("Primate");
            Expect.IsTrue(m.GetType() == typeof(Monkey));
        }

        [UnitTest]
        public static void IncubatorShouldTakeAFuncAndPopOutSpecifiedType()
        {
            Incubator i = new Incubator();
            Type type;
            i.Set<Primate>(() => { return new Monkey(); });
            object m = i.Get("Primate", out type);
            Expect.IsTrue(type == typeof(Primate));
        }

        [UnitTest]
        public static void FluentIncubatorTest()
        {
            Incubator i = Requesting.A<Primate>().Returns<Monkey>();
            Primate m = i.Get<Primate>();
            Expect.IsTrue(m.GetType() == typeof(Monkey));
        }

        [UnitTest]
        public static void FluentConstructorTests()
        {
            Incubator withBanana = Requesting.A<IFruit>().Returns<Banana>();
            withBanana.AskingFor<Primate>().Returns<Gorilla>();
            Primate p = withBanana.Get<Primate>();
            Expect.IsTrue(p is Gorilla);
            Gorilla g = (Gorilla)p;
            Expect.IsTrue(g.Fruit.GetType() == typeof(Banana));
        }

        [UnitTest]
        public static void FluentConstructorTests2()
        {
            Incubator withApple = Requesting.A<IFruit>().Returns<Apple>();
            withApple.AskingFor<Monkey>().Returns<Gorilla>();
            Primate p = withApple.Get<Monkey>();
            Expect.IsTrue(p is Gorilla);
            Gorilla g = (Gorilla)p;
            Expect.IsTrue(g.Fruit.GetType() == typeof(Apple));
        }

        [UnitTest]
        public static void FluentConstructorTests3()
        {
            Incubator withApple = Requesting.A<IFruit>().Returns<Apple>();
            withApple.Bind<Monkey>().To<Gorilla>();
            Primate p = withApple.Get<Monkey>();
            Expect.IsTrue(p is Gorilla);
            Gorilla g = (Gorilla)p;
            Expect.IsTrue(g.Fruit.GetType() == typeof(Apple));
        }
    }
}
