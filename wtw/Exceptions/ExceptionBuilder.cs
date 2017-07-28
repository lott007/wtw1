using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTW.Exceptions
{
	public class ExceptionBuilder
	{

		public static TypeNotRegisteredException BuildTypeNotRegistered(Type t)
		{
			return new TypeNotRegisteredException(string.Format("This type ({0}) was not able to be registered, it's too ugly :o(", t.Name));
		}
	}
}
