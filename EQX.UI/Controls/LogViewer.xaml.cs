using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using EQX.Core.Units;
using log4net.Core;

namespace EQX.UI.Controls
{
    public partial class LogViewer : UserControl
    {
        private List<string> level = new List<string> { "ALL", "DEBUG", "INFO", "WARN", "ERROR", "FATAL", "OFF" };

        public string LogDirectory
        {
            get { return (string)GetValue(LogDirectoryProperty); }
            set { SetValue(LogDirectoryProperty, value); }
        }
        public ObservableCollection<string> Unit
        {
            get { return (ObservableCollection<string>)GetValue(UnitProperty); }
            set { SetValue(UnitProperty, value); }
        }

        public static readonly DependencyProperty UnitProperty =
            DependencyProperty.Register("Unit", typeof(ObservableCollection<string>), typeof(LogViewer), new PropertyMetadata(new ObservableCollection<string> { }));

        public static readonly DependencyProperty LogDirectoryProperty =
            DependencyProperty.Register("LogDirectory", typeof(string), typeof(LogViewer), new PropertyMetadata(""));


        public LogViewer()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void LoadLogFiles()
        {
            LogFileListBox.Items.Clear();
            var logFiles = Directory.GetFiles(LogDirectory, "");
            for (int i = 0; i < logFiles.Length; ++i)
            {
                LogFileListBox.Items.Add(Path.GetFileName(logFiles[logFiles.Length - 1 - i]));
            }
        }

        private void LoadLevel()
        {
            cboLevel.Items.Clear();
            foreach (var level in level)
            {
                cboLevel.Items.Add(level);
            }
            cboLevel.SelectedIndex = 0;
        }

        private void LogFileListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LogFileListBox.SelectedItem != null)
            {
                LoadData();
            }
        }

        private void cboUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LogFileListBox.SelectedItem != null)
            {
                LoadData();
            }
        }

        private void cboLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LogFileListBox.SelectedItem != null)
            {
                LoadData();
            }
        }

        private ObservableCollection<string> Filter(string unit, string level)
        {
            if (unit == "ALL" && level == "ALL") return logList;
            ObservableCollection<string> result = new ObservableCollection<string>();
            foreach (string item in logList)
            {
                if (unit == "ALL")
                {
                    if (item.Contains(level))
                    {
                        result.Add(item);
                    }
                    continue;
                }
                if (level == "ALL")
                {
                    if (item.Contains(unit))
                    {
                        result.Add(item);
                    }
                    continue;
                }
                if (item.Contains(level) && item.Contains(unit))
                {
                    result.Add(item);
                }
            }
            return result;
        }
        private void LoadData()
        {
            if (string.IsNullOrEmpty(LogDirectory)) return;

            string selectedFile = Path.Combine(LogDirectory, (string)LogFileListBox.SelectedItem);
            string logContent = File.ReadAllText(selectedFile);
            logList = new ObservableCollection<string>(logContent.Split("\n"));
            logList = Filter(cboLevel.SelectedItem.ToString(), cboUnit.SelectedItem.ToString());
            logListBox.ItemsSource = logList;
        }

        private ObservableCollection<string> logList = new ObservableCollection<string>();

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLogFiles();
            LoadLevel();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            logListBox.ItemsSource = null;
            LoadLogFiles();
            LoadLevel();
            cboUnit.SelectedIndex = 0;
        }
    }
}