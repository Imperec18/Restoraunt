using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WPF_APP_CORE_Restaurant.Component
{
    public class ToggleButton : Button
    {


        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsChecked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(ToggleButton), new PropertyMetadata(false));



        protected override void OnClick()
        {
            IsChecked = !IsChecked;
        }
    }
}
