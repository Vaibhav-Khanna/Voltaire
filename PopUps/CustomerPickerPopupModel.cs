﻿using System;
using System.Collections.ObjectModel;
using Rg.Plugins.Popup.Services;
using voltaire.Models;
using Xamarin.Forms;

namespace voltaire.PopUps
{
    public class CustomerPickerPopupModel : BaseModel
    {


		public CustomerPickerPopupModel() 
		{
			
		}


		public delegate void EventHandler();

		public event EventHandler ItemSelectedChanged; //  event handler when a item is selected


        Customer selecteditem; //  selected item
        public Customer SelectedItem
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
            SelectedItem = obj as Customer;
			ItemSelectedChanged.Invoke();
			PopupNavigation.PopAsync(true);
		});


        ObservableCollection<Customer> itemsource;  // Product list
        public ObservableCollection<Customer> ItemSource
		{
			get { return itemsource; }
			set
			{
				itemsource = value;
                RaisePropertyChanged();
			}
		}
    }
}
