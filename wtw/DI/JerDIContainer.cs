using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTW.Exceptions;

namespace WTW.DI
{
	public class JerDIContainer : IContainer
	{

		private IList<RegisteredObject> _registeredObjects = new List<RegisteredObject>();

		private static JerDIContainer instance;

		public static JerDIContainer GetInstance()
		{
			if (instance == null)
			{
				instance = new JerDIContainer();
			}
			return instance;
		}


		public void Register<TTypeToRegister, TConcreteObject>(LifeCycleType lifeCycle = LifeCycleType.Transient)
		{
			_registeredObjects.Add(new RegisteredObject(typeof(TTypeToRegister), typeof(TConcreteObject), lifeCycle));
		}

		public TTypeToResolve Resolve<TTypeToResolve>()
		{
			return (TTypeToResolve)Resolve(typeof(TTypeToResolve));
		}

		public object Resolve(Type typeToResolve)
		{
			var registeredObject = _registeredObjects.FirstOrDefault(x => x.TypeToResolve == typeToResolve);
			if (registeredObject == null)
			{
				throw ExceptionBuilder.BuildTypeNotRegistered(typeToResolve);
			}

			return CreateObject(registeredObject);
		}


		// Recursive method that creates the objects that are in the constructors
		private object CreateObject(RegisteredObject regObj)
		{
			// If it is Transient then always create a new object
			if (regObj.Instance == null || regObj.MyLifeCycleType == LifeCycleType.Transient)
			{
				var parameters = ResolveConstructorParams(regObj);
				regObj.CreateMe(parameters.ToArray());
			}
			// If it is a Singleton then don't use the existing instance instead of creating a new one
			return regObj.Instance;
		}

		private IEnumerable<object> ResolveConstructorParams(RegisteredObject regObj)
		{
			var constructorInfo = regObj.ConcreteType.GetConstructors().First();
			foreach (var param in constructorInfo.GetParameters())
			{
				// Adding the "yield" here changes the return type to an IEnumerable
				yield return Resolve(param.ParameterType);
			}
		}
	}
}
