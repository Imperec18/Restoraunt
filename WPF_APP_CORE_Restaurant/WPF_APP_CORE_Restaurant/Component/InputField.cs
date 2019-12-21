using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace WPF_APP_CORE_Restaurant.Component
{
    public class InputField : TextBox
    {


        public InputField()
        {
            IsReadOnly = true;
            MouseDoubleClick += InputField_MouseDoubleClick;
        }

        private void InputField_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IsReadOnly = !IsReadOnly;
        }
    }
}
