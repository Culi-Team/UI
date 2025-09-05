using EQX.UI.Interlock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EQX.UI.Controls
{
    public class InterlockButton : Button
    {
        public string? InterlockKey
        {
            get => (string?)GetValue(InterlockKeyProperty);
            set => SetValue(InterlockKeyProperty, value);
        }

        public static readonly DependencyProperty InterlockKeyProperty =
            DependencyProperty.Register(
                nameof(InterlockKey),
                typeof(string),
                typeof(InterlockButton),
                new PropertyMetadata(null));

        public InterlockButton()
        {
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
            IsEnabled = false;
            IsHitTestVisible = false;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            InterlockService.Default.InterlockChanged += OnInterlockChanged;
            InterlockService.Default.Reevaluate();
        }

        private void OnUnloaded(object? sender, RoutedEventArgs e)
        {
            InterlockService.Default.InterlockChanged -= OnInterlockChanged;
        }

        private void OnInterlockChanged(string key, bool satisfied)
        {
            Dispatcher.Invoke(() =>
            {
                if (string.Equals(key, InterlockKey, StringComparison.Ordinal))
                {
                    IsEnabled = satisfied;
                    IsHitTestVisible = satisfied;
                }
            });
        }
    }
}
