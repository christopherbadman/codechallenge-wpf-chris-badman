namespace WeatherApp
{
  using System.Windows;

  public partial class App
  {
        public App()
        {
            this.DispatcherUnhandledException += AppUnhandledException;
        }

        private void AppUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            // todo: logging
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var bootstrapper = new Bootstrapper(this);
            bootstrapper.Run();
        }
    }
}