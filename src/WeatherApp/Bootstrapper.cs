namespace WeatherApp
{
    using System.IO;
    using System.Windows;
    using ViewModels;
    using Views;

    public class Bootstrapper
    {
        public Bootstrapper(Application app)
        {
          Application = app;      
        }

        public Application Application { get; private set; }

        public void Run()
        {
            var dir = Directory.GetCurrentDirectory();

            string filepath = dir + "\\..\\..\\..\\..\\data\\coords.csv";

            var dataAccess = new DataAccess.DataAccess(filepath);

            var vm = new MainViewModel(dataAccess);
            var mainWindow = new MainWindow { DataContext = vm };
            mainWindow.Show();
        }   
    }
}