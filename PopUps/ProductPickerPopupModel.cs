using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using voltaire.Models;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;

namespace voltaire.PopUps
{
    public class ProductPickerPopupModel : INotifyPropertyChanged 
    {
        
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void EventHandler();

		public event EventHandler ItemSelectedChanged; //  event handler when a item is selected

        public ProductPickerPopupModel() //  add products to the popup list
        {
            ItemSource = new ObservableCollection<Product>(ProductConstants.Products);
        }

        Product selecteditem; //  selected item
        public Product SelectedItem
        {
            get { return selecteditem; }
            set
            {
                selecteditem = value;
                RaisePropertyChanged();
            }
        }

        public Command Close => new Command((obj) =>  // close button command
       {
            SelectedItem = null;		  
            ItemSelectedChanged.Invoke();
            PopupNavigation.PopAsync(true);
       });

		public Command ItemSelected => new Command((object obj) =>  // item viewcell tap command execute
		{
            SelectedItem = obj as Product;
            ItemSelectedChanged.Invoke();
			PopupNavigation.PopAsync(true);
		});


        ObservableCollection<Product> itemsource;  // Product list
        public ObservableCollection<Product> ItemSource
        {
            get { return itemsource; }
            set
            {
                itemsource = value;
                RaisePropertyChanged();
            }
        }

		void RaisePropertyChanged([CallerMemberName] string name = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
    }
}
