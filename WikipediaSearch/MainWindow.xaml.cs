using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WikipediaSearch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            var searchInput = (from eve in Observable.FromEvent<KeyEventHandler,KeyEventArgs>(handler =>
            {
                KeyEventHandler keyHandler = (sender, e) => handler(e);

                return keyHandler;
            },handler => Search.KeyUp += handler,
                handler => Search.KeyUp -= handler)
                
                select (Search.Text))
                
                .Throttle(TimeSpan.FromSeconds(0.5));



            var searchOperation = searchInput.ObserveOn(SynchronizationContext.Current).Subscribe(text =>
            {
                lblSearch.Text = "Searching text: " + text;

                lblProgress.Visibility = Visibility.Visible;
                lblSearch.Visibility = Visibility.Visible;
                WebBrowser1.Navigate(new Uri("http://en.wikipedia.org/wiki/"
                                            + text));
            });



            var navigationComplete =
                Observable.FromEvent<NavigatedEventHandler, NavigationEventArgs>(handler =>
                {
                    NavigatedEventHandler navHandler = (sender, e) => handler(e);

                    return navHandler;
                }, handler => WebBrowser1.Navigated += handler,
                    handler => WebBrowser1.Navigated -= handler);


            navigationComplete.ObserveOn(SynchronizationContext.Current).Subscribe((navArg) =>
            {
                lblProgress.Visibility = Visibility.Hidden;
                lblSearch.Text = "Start new search";
                Search.Text = string.Empty;
            });




        }
    }
}
