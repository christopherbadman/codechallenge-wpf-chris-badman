using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Collections;
using System.ComponentModel;
using System.Windows.Controls;

using WeatherApp.Services;

namespace WeatherApp.Controls
{
    /// <summary>
    /// Interaction logic for Searcher.xaml
    /// </summary>
    public partial class Searcher : UserControl
    {
        // the current input search string
        private string _input;

        public static readonly DependencyProperty SuggestionTextProperty =
            DependencyProperty.Register("SuggestionText", typeof(string), typeof(Searcher));

        /// <summary>
        /// The text to display in the searcher as a hint to help the user
        /// </summary>
        public string SuggestionText
        {
            get { return (string)GetValue(SuggestionTextProperty); }
            set { SetValue(SuggestionTextProperty, value); }
        }

        public static readonly DependencyProperty SearchableItemsSourceProperty =
            DependencyProperty.Register("SearchableItemsSource", typeof(IEnumerable), typeof(Searcher), new PropertyMetadata(new PropertyChangedCallback(OnSearchableItemsSourceChanged)));

        /// <summary>
        /// The collection of items that are searchable
        /// </summary>
        public IEnumerable SearchableItemsSource
        {
            get { return (IEnumerable)GetValue(SearchableItemsSourceProperty); }
            set { SetValue(SearchableItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(Searcher));

        /// <summary>
        /// The currently selected item in the searcher
        /// </summary>
        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly DependencyProperty SearchServiceProperty =
            DependencyProperty.Register("SearchService", typeof(ISearchService), typeof(Searcher));

        /// <summary>
        /// The search service provided
        /// </summary>
        public ISearchService SearchService
        {
            get { return (ISearchService)GetValue(SearchServiceProperty); }
            set { SetValue(SearchServiceProperty, value); }
        }

        private static readonly DependencyProperty FilteredItemsProperty =
            DependencyProperty.Register("FilteredItems", typeof(ICollectionView), typeof(Searcher));

        /// <summary>
        /// The filtered results based on the current search string
        /// </summary>
        private ICollectionView FilteredItems
        {
            get { return (ICollectionView)GetValue(FilteredItemsProperty); }
            set { SetValue(FilteredItemsProperty, value); }
        }

        public Searcher()
        {
            InitializeComponent();
        }

        private bool Filter(object item)
        {
            string str = item.ToString();

            // Normally I would implement the 'search' as such, but this is net very testable...

            //return (_input != null) &&
            //       (string.Compare(_input, SuggestionText) != 0) &&
            //       (str.IndexOf(_input, StringComparison.CurrentCultureIgnoreCase) >= 0);            

            if (SearchService == null) return false;

            return (_input != null) &&
                   (string.Compare(_input, SuggestionText) != 0) &&
                   (SearchService.Match(_input).Contains(item));
        }

        /// <summary>
        /// Hanlder for the TextChanged event of the searcher
        /// </summary>
        private void OnInputChanged(object sender, RoutedEventArgs e)
        {
            var cb = (ComboBox)sender;
            if (cb != null)
            {
                _input = cb.Text;

                // refresh our filter
                if (FilteredItems != null) FilteredItems.Refresh();
            }
        }

        /// <summary>
        /// Updates the source of our search filter
        /// </summary>
        private void UpdateFilterSource()
        {
            FilteredItems = CollectionViewSource.GetDefaultView(SearchableItemsSource);
            FilteredItems.Filter = Filter;
        }

        /// <summary>
        /// Handler for the PropertyChanged event of the SearchableItemsSource property
        /// </summary>
        /// <param name="o">The Searcher instance</param>
        /// <param name="e">The PropertyChanged args</param>
        private static void OnSearchableItemsSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var searcher = (Searcher)o;
            if (searcher != null)
            {
                // items source has changed so we need to update our filter source accordingly
                searcher.UpdateFilterSource();
            }
        }    
    }
}
