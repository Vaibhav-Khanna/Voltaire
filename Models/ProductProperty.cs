using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace voltaire.Models
{
    public class ProductProperty
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

        public string PropertyValue { get; set; }

        public List<string> ItemSource { get; set; }

        public ProductProperty ObjectClone(ProductProperty obj)
        {
            return (ProductProperty)obj.MemberwiseClone();
        }

    }

    public enum PropertyType
    {
        IsText, IsBoolean, IsPicker, IsEditor
    }


}
