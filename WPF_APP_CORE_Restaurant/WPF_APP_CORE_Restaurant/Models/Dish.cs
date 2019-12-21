using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;

namespace WPF_APP_CORE_Restaurant.Models
{
    public class Dish : BindableBase
    {
        public int ID { get; set; }

        private string name;
        public string Name {
            get
            {
                return name;
            }
            set
            {
                SetProperty(ref name,value);
            }
        }

        private string description;
        public string Description {
            get
            {
                return description;
            }
            set
            {
                SetProperty(ref description,value);
            }
        }

        private FileInfo image;
        public FileInfo Image {
            get
            {
                return image;
            }
            set
            {
                SetProperty(ref image,value);
            }
        }

        public BitmapImage getimage
        {
            get
            {
                return new BitmapImage(new Uri(Image.FullName,UriKind.Absolute));
            }
        }

        private int price;
        public int Price
        {
            get { return price; }
            set { SetProperty(ref price,value); }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set { SetProperty(ref isSelected, value); }
        }

        private int dishesCount;
        public int DishesCount
        {
            get { return dishesCount; }
            set { SetProperty(ref dishesCount,value); }
        }

    }
}
