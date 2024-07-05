using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace EQX.UI.Controls
{
    /// <summary>
    /// Interaction logic for LogDisplayer.xaml
    /// </summary>
    public partial class LogDisplayer : UserControl
    {
        public LogDisplayer()
        {
            InitializeComponent();

            NotifyAppender.Appender.Notification.CollectionChanged += Notification_CollectionChanged;
        }

        private void Notification_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var selectedIndex = logListBox.Items.Count - 1;
            if (selectedIndex < 0)
                return;

            logListBox.SelectedIndex = selectedIndex;
            logListBox.UpdateLayout();

            logListBox.ScrollIntoView(logListBox.SelectedItem);
        }
    }
}
