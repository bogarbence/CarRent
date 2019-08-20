using System;
using System.Collections.Generic;
using Caliburn.Micro;
using System.Windows;
using VU.ViewModels;

namespace VU
{
    public class Bootstrapper : BootstrapperBase
    {
        public SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();
            //LogManager.GetLog = type => new DebugLogger(type);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override void Configure()
        {
            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();
            _container.Singleton<ShellViewModel, ShellViewModel>();
            _container.Singleton<TestViewModel, TestViewModel>();
            _container.Singleton<AddCarViewModel, AddCarViewModel>();
            _container.Singleton<AddReservationViewModel, AddReservationViewModel>();
            _container.Singleton<CarStatusViewModel, CarStatusViewModel>();
            _container.Singleton<RiportsViewModel, RiportsViewModel>();
            _container.Singleton<LoginViewModel, LoginViewModel>();
            _container.Singleton<ReservationListViewModel, ReservationListViewModel>();
            base.Configure();
        }

        protected override object GetInstance(Type service, string key)
        {
            var instance = _container.GetInstance(service, key);
            if (instance != null)
                return instance;

            throw new InvalidOperationException("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}