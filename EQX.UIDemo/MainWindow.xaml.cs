using EQX.UI.Controls;
using EQX.UI.Dtos;
using EQX.UI.Services;
using EQX.UIDemo.StartupHelpers;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EQX.UIDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IAlarmService alarmService;

        public MainWindow(IAlarmService alarmService)
        {
            InitializeComponent();
            this.alarmService = alarmService;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AlarmNotifyView alarmNotifyView = new AlarmNotifyView();
            AlarmNotifyModel alarmNotifyModel = new AlarmNotifyModel();
            alarmNotifyModel = alarmService.GetById(1);

            alarmNotifyView.DataContext = new AlarmNotifyViewModel(alarmNotifyModel);
            alarmNotifyView.Show();
        }
    }
}