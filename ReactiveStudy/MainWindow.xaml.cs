using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
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

namespace ReactiveStudy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Observe(object sender, EventArgs e)
        {

            //var source = Observable.Range(1,100);


            var source = new Subject<int>();

           

             source.Scan(Agg).Subscribe(number => Messages.Items.Add(number),
                ex => Messages.Items.Add("OnError: " + ex.Message),
                 () => Messages.Items.Add("OnCompleted"));


              for (int i = 1; i < 101; i++)
            {
                source.OnNext(i);
            }

        }

        private int Agg(int arg1, int arg2)
        {
            return arg1 + arg2;
        }
    }
}
