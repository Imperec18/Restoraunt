using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;
using Prism.Commands;
using WPF_APP_CORE_Restaurant.Models;
using System.Windows;
using Microsoft.Win32;

namespace WPF_APP_CORE_Restaurant.ViewModels
{
    public class AddDishViewModel : BindableBase
    {
        public event Action ClickAdded;

        private Dish dish;
        public Dish GetDish
        {
            get { return dish; }
            set { SetProperty(ref dish,value); }
        }

        public DelegateCommand loadImage
        {
            get
            {
                return new DelegateCommand(() => {
                    OpenFileDialog openimage = new OpenFileDialog();
                    if(openimage.ShowDialog() == true)
                    {
                        dish.Image = new System.IO.FileInfo(openimage.FileName);
                    }
                });
            }
        }
        public DelegateCommand OK
        {
            get
            {
                return new DelegateCommand(() => { ClickAdded?.Invoke(); });
            }
        }

        public AddDishViewModel()
        {
            GetDish = new Dish();
        }
    }
}
