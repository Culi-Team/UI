using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using EQX.Core.Common;

namespace EQX.UI.Controls
{
    /// <summary>
    /// Interaction logic for AlertNotifyView.xaml
    /// </summary>
    public partial class AlertNotifyView : Window, INotifyPropertyChanged
    {
        private AlertModel _alertModel;
        private string _selectedLanguage;
        private List<string> _availableLanguages = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        public AlertModel AlertModel
        {
            get => _alertModel;
            set
            {
                _alertModel = value;
                OnPropertyChanged();
            }
        }

        public bool IsWarning { get; set; }
        public IAlertService? AlertService { get; set; }

        public List<string> AvailableLanguages
        {
            get => _availableLanguages;
            private set
            {
                _availableLanguages = value;
                OnPropertyChanged();
            }
        }

        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                _selectedLanguage = value;
                OnPropertyChanged();
                OnLanguageChanged();
            }
        }

        public AlertNotifyView()
        {
            InitializeComponent();
            DataContext = this;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        public static bool? ShowDialog(IAlertService alertService, int alertId, bool isWarning = false)
        {
            return ShowDialog(alertService.GetById(alertId), isWarning, alertService);
        }

        public static bool? ShowDialog(AlertModel alarmModel, bool isWarning = false, IAlertService? alertService = null)
        {
            return Application.Current.Dispatcher.Invoke(() =>
            {
                bool isShown = Application.Current.Windows.OfType<AlertNotifyView>().Any();
                if (isShown) Application.Current.Windows.OfType<AlertNotifyView>().First().Close();

                isShown = Application.Current.Windows.OfType<MessageBoxEx>().Any();
                if (isShown) Application.Current.Windows.OfType<MessageBoxEx>().First().Close();


                var mainWindow = Application.Current.MainWindow;
                double width = mainWindow.ActualWidth;
                double height = mainWindow.ActualHeight;

                AlertNotifyView messageBoxEx = new AlertNotifyView()
                {
                    IsWarning = isWarning,
                    AlertModel = alarmModel,
                    AlertService = alertService,
                    Width = width * 0.6,
                    Height = height * 0.6,
                };

                messageBoxEx.InitializeLanguages();

                messageBoxEx.ShowDialog();
                return messageBoxEx.DialogResult;
            });
        }

        private void InitializeLanguages()
        {
            var languageList = AlertService?.SupportedCultures?.ToList() ?? new List<string> { "English", "Vietnamese" };
            AvailableLanguages = languageList;
            SelectedLanguage = AlertService?.CurrentCulture ?? languageList.FirstOrDefault();
        }

        private void OnLanguageChanged()
        {
            if (AlertService == null || AlertModel == null || string.IsNullOrWhiteSpace(SelectedLanguage))
            {
                return;
            }

            AlertService.ChangeCulture(SelectedLanguage);
            AlertModel = AlertService.GetById(AlertModel.Id);
            OnPropertyChanged(nameof(AlertModel));
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.Interop.ComponentDispatcher.IsThreadModal)
            {
                DialogResult = false;
            }
            else
            {
                Close();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
