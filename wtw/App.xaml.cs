using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WTW.Controllers;
using WTW.DI;
using WTW.Interfaces;
using WTW.Models;

namespace wtw
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			Configure();
		}

		private void Configure()
		{
			IContainer container = JerDIContainer.GetInstance();
			container.Register<IController, UsersController>(LifeCycleType.Singleton);
			container.Register<IEmailService, EmailService>(LifeCycleType.Singleton);
			container.Register<ICalculator, Calculator>();

			var controller = container.Resolve<IController>();
			var emailService = container.Resolve<IEmailService>();
			var calculator = container.Resolve<ICalculator>();
		}
	}
}
