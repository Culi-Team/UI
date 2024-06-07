using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EQX.UI.Controls
{
    internal class MessageBoxExViewModel : ObservableObject
    {
        public EventHandler MessageBoxExShownEvent { get; set; }

        #region Properties
        public string MessageDetail
		{
			get { return _messageDetail; }
            set
            {
                _messageDetail = value;
                OnPropertyChanged();
            }
		}

		public string Caption
		{
			get { return _caption; }
            set
            {
                _caption = value;
                OnPropertyChanged();
            }
        }

		public MessageBoxResult Result { get; set; }
        #endregion

        #region Public Methods
        public void Show(string message, string caption = "Confirm")
        {
            UpdateInformation(message, caption);
        }

        public void ShowDialog(string message, string caption = "Confirm")
        {
            UpdateInformation(message, caption);
        }
        #endregion

        #region Private Methods
        private void UpdateInformation(string message, string caption)
        {
            MessageDetail = message;
            Caption = caption;

            Result = MessageBoxResult.None;
        }
        #endregion

        #region Privates
        private string _messageDetail;
        private string _caption;
        #endregion
    }
}
