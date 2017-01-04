using Caliburn.Micro;
using SteveHemond.MusicSheetViewer.Messages;
using SteveHemond.MusicSheetViewer.ViewModels;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;

namespace SteveHemond.MusicSheetViewer
{
    sealed partial class App : CaliburnApplication
    {
        /// <summary>
        /// The container
        /// </summary>
        private WinRTContainer container;

        /// <summary>
        /// The event aggregator
        /// </summary>
        private IEventAggregator eventAggregator;

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Override to configure the framework and setup your IoC container.
        /// </summary>
        protected override void Configure()
        {
            this.container = new WinRTContainer();
            this.container.RegisterWinRTServices();

            this.container
                .PerRequest<ShellViewModel>()
                .PerRequest<WelcomeViewModel>();

            // ToDo: Change path to view and viewmodels folder in different assemblies if necessary.
            ////Override the default subnamespaces
            //var config = new TypeMappingConfiguration
            //{
            //    DefaultSubNamespaceForViews = "UWP.Views",
            //    DefaultSubNamespaceForViewModels = "Ui.ViewModels"
            //};

            //ViewLocator.ConfigureTypeMappings(config);
            //ViewModelLocator.ConfigureTypeMappings(config);

            this.eventAggregator = this.container.GetInstance<IEventAggregator>();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            // Note we're using DisplayRootViewFor (which is view model first)
            // this means we're not creating a root frame and just directly
            // inserting ShellView as the Window.Content
            this.DisplayRootViewFor<ShellViewModel>();

            // It's kind of weird having to use the event aggregator to pass 
            // a value to ShellViewModel, could be an argument for allowing
            // parameters or launch arguments
            if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
            {
                this.eventAggregator.PublishOnUIThread(new ResumeStateMessage());
            }
        }

        /// <summary>
        /// Override this to add custom behavior when the application transitions to Suspended state from some other state.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        protected override void OnSuspending(object sender, SuspendingEventArgs e)
        {
            this.eventAggregator.PublishOnUIThread(new SuspendStateMessage(e.SuspendingOperation));
        }

        /// <summary>
        /// Override this to provide an IoC specific implementation.
        /// </summary>
        /// <param name="service">The service to locate.</param>
        /// <param name="key">The key to locate.</param>
        /// <returns>The located service.</returns>
        protected override object GetInstance(Type service, string key)
        {
            return this.container.GetInstance(service, key);
        }

        /// <summary>
        /// Override this to provide an IoC specific implementation
        /// </summary>
        /// <param name="service">The service to locate.</param>
        /// <returns>The located services.</returns>
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return this.container.GetAllInstances(service);
        }

        /// <summary>
        /// Override this to provide an IoC specific implementation.
        /// </summary>
        /// <param name="instance">The instance to perform injection on.</param>
        protected override void BuildUp(object instance)
        {
            this.container.BuildUp(instance);
        }
    }
}