namespace WeatherApp.Views
{
    using System.Windows;
    using System.Windows.Input;
    using ViewModels;

    public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      DataContextChanged += OnDataContextChanged;
    }

    void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs _)
    {
      var vm = DataContext as MainViewModel;
      
      vm.MapViewModel.PropertyChanged += (s, e) =>
      {
        if (e.PropertyName == "Center")
          _map.Center = vm.MapViewModel.Center;
        if (e.PropertyName == "ZoomLevel")
          _map.ZoomLevel = vm.MapViewModel.ZoomLevel;
                        
      };
      
      _map.Center = vm.MapViewModel.Center;
      _map.ZoomLevel = vm.MapViewModel.ZoomLevel;
    }

        private void OnMapMouseFocus(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // give it keyboard focus to force the focus away from the searcher
            Keyboard.Focus((FrameworkElement)sender);
        }
    }
}
