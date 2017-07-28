using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTW.DI
{
	class RegisteredObject
	{
		public RegisteredObject(Type typeToResolve, Type concreteType, LifeCycleType lifeCycle)
		{
			TypeToResolve = typeToResolve;
			ConcreteType = concreteType;
			MyLifeCycleType = lifeCycle;
		}

		public LifeCycleType MyLifeCycleType { get; set; }

		public Type TypeToResolve { get; set; }

		public Type ConcreteType { get; set; }

		public object Instance { get; private set; }


		public void CreateMe(params object[] args)
		{
			Instance = Activator.CreateInstance(ConcreteType, args);
		}
	}
}
