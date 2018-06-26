using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace voltaire.Models
{
    public class ProductProperty : INotifyPropertyChanged
    {

        public ProductProperty(PropertyType Type)
        {
            PropertyType = Type;
        }


        PropertyType propertytype;

      

        public PropertyType PropertyType
        {
            get { return propertytype; }
            set
            {
                propertytype = value;
            }
        }

        public string PropertyName { get; set; }

        string val;
        public string PropertyValue { get { return val; } set { val = value; RaisePropertyChanged(); } }

        List<string> _source;
        public List<string> ItemSource { get { return _source; } set{ _source = value; RaisePropertyChanged(); } }

        public List<string> AllSource { get; set; }

        public bool IsNumberKeyboard { get; set; }

        public bool IsVisible { get; set; } = true;

        public ProductProperty ObjectClone(ProductProperty obj)
        {
            return (ProductProperty)obj.MemberwiseClone();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public enum PropertyType
    {
        IsText, IsBoolean, IsPicker, IsEditor, IsLabel
    }


}
