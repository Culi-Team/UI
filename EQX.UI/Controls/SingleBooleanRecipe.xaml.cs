using EQX.Core.Recipe;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for SingleBooleanRecipe.xaml
    /// </summary>
    public partial class SingleBooleanRecipe : UserControl
    {
        public bool IsHeader
        {
            get { return (bool)GetValue(IsHeaderProperty); }
            set { SetValue(IsHeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsHeader.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsHeaderProperty =
            DependencyProperty.Register("IsHeader", typeof(bool), typeof(SingleBooleanRecipe), new PropertyMetadata(false));

        public bool Value
        {
            get { return (bool)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(bool), typeof(SingleBooleanRecipe), new PropertyMetadata(false));
        public SingleBooleanRecipe(SingleRecipeDescriptionAttribute singleRecipeDescription)
        {
            SingleRecipeDescription = singleRecipeDescription;
            InitializeComponent();
            this.DataContext = this;
        }

        public SingleRecipeDescriptionAttribute SingleRecipeDescription { get; set; }
    }
}
