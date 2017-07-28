using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTW.Exceptions
{
	public class TypeNotRegisteredException : Exception
	{
		public TypeNotRegisteredException(string msg) : base(msg)
		{
		}


	}
}
