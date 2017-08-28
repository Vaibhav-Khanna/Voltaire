﻿using System;
using FreshMvvm;
using voltaire.Controls.Items;
using voltaire.PageModels;
using Xamarin.Forms;

namespace voltaire.Pages.Base
{
    public class BaseViewPagerPage : ContentView
    {

        private TTab Context;

        public IPageModelCoreMethods NavigationService;      //  Navigation service


        public BaseViewPagerPage()
        {
			
			BackgroundColor = Color.White;

        }

        protected override void OnBindingContextChanged()
        {
			base.OnBindingContextChanged();

            var context = BindingContext as TTab;

            if (context == null)
                return;

            Context = context;

            NavigationService = context.Navigation;

            if (context.ViewBindingContext == null || BindingContext != context.ViewBindingContext) //Init Viewmodels and attach with correspoding views
			{
                switch (context.View.ToString().Substring(context.View.ToString().LastIndexOf(".",StringComparison.CurrentCulture) + 1))
				{
					case "MapTabPage":
						{
							context.ViewBindingContext = new MapPageModel();
                            BindingContext = context.ViewBindingContext;
                            BindingContextSet();
							break;
						}
                    case "ContactDetailTabPage":
                        {
                            context.ViewBindingContext = Context.Parent;
                            BindingContext = Context.Parent;
							BindingContextSet();
                            break;
                        }
				}
            }
              		
        }

        virtual protected void BindingContextSet()
        {
            
        }

        public bool MoveToNextTab()
        {
            var page = Context.Parent as ContactDetailPageModel;

            if (page == null)
                return false;

            if( page.SelectedIndex+1 < page.Tab.Count)
            page.SelectedIndex = 1;

            return true;
        }

        public bool MoveToBackTab()
        {
            var page = Context.Parent as ContactDetailPageModel;

            if (page == null)
                return false;

			if (page.SelectedIndex > 0)
				page.SelectedIndex -= 1;

            return true;
        }

    }
}

