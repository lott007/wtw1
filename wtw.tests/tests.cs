using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WTW.DI;
using WTW.Exceptions;

namespace wtw.tests
{
    public class tests
    {

		[Test]
		public void ResolveObjectTest()
		{
			var container = JerDIContainer.GetInstance();

			container.Register<ITypeToResolve, ConcreteType>();

			var instance = container.Resolve<ITypeToResolve>();

			Assert.That(instance, Is.InstanceOf<ConcreteType>());
		}

		[Test]
		public void TypeNotRegisteredTest()
		{
			var container = JerDIContainer.GetInstance();

			Exception exception = null;
			try
			{
				container.Resolve<ITypeToResolve>();
			}
			catch (Exception ex)
			{
				exception = ex;
			}

			Assert.That(exception, Is.InstanceOf<TypeNotRegisteredException>());
		}

		[Test]
		public void ResolveObjectWithRegisteredParamsTest()
		{
			var container = JerDIContainer.GetInstance();

			container.Register<ITypeToResolve, ConcreteType>();
			container.Register<ITypeToResolveWithConstructorParams, ConcreteTypeWithConstructorParams>();

			var instance = container.Resolve<ITypeToResolveWithConstructorParams>();

			Assert.That(instance, Is.InstanceOf<ConcreteTypeWithConstructorParams>());
		}


		[Test]
		public void CreateTransientTest()
		{
			var container = JerDIContainer.GetInstance();

			container.Register<ITypeToResolve, ConcreteType>(LifeCycleType.Transient);

			var instance = container.Resolve<ITypeToResolve>();

			Assert.That(container.Resolve<ITypeToResolve>(), Is.Not.SameAs(instance));
		}
	}

	public interface ITypeToResolve
	{
	}

	public class ConcreteType : ITypeToResolve
	{
	}

	public interface ITypeToResolveWithConstructorParams
	{
	}

	public class ConcreteTypeWithConstructorParams : ITypeToResolveWithConstructorParams
	{
		public ConcreteTypeWithConstructorParams(ITypeToResolve typeToResolve)
		{
		}
	}
}
