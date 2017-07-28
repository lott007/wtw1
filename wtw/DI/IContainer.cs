using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTW.DI
{
	interface IContainer
	{
		void Register<TTypeToRegister, TConcreteObject>(LifeCycleType lifeCycle = LifeCycleType.Transient);

		TTypeToResolve Resolve<TTypeToResolve>();
	}
}
