﻿using System;
using voltaire.Models;
using voltaire.PageModels;
using voltaire.Pages.Base;
using Xamarin.Forms;
using voltaire.Controls;
using System.Collections.Generic;
using CarouselView.FormsPlugin.Abstractions;
using System.Collections.ObjectModel;
using voltaire.Controls.Items;
using voltaire.Resources;

namespace voltaire.Pages
{
    public class ContactDetailPage : BaseDisposePage
    {
        
		TapGestureRecognizer tap_toolbar = new TapGestureRecognizer();

        TapGestureRecognizer tap_Pop = new TapGestureRecognizer();

        TTabSlider tabslider;
        TToolBar toolbar;
        CarouselViewControl viewpager;
	
        public ContactDetailPage()
        {
            InitView();
        }
       
        void InitView()          // initialize view
		{
            NavigationPage.SetHasNavigationBar(this, false);
			

            var R_toolbaritem = new Label
            {
                Text = AppResources.Modify,
                TextColor = Color.White,
                FontFamily = "Raleway-Regular",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            tap_toolbar.SetBinding(TapGestureRecognizer.CommandProperty, "tap_Toolbar");
            R_toolbaritem.GestureRecognizers.Add(tap_toolbar);


			var L_toolbaritem = new Label
			{
				Text = "Back",
				TextColor = Color.White,
				FontFamily = "Raleway-Regular",
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
			};
			tap_Pop.SetBinding(TapGestureRecognizer.CommandProperty, "tap_Back");
            L_toolbaritem.GestureRecognizers.Add(tap_Pop);

            toolbar = new TToolBar()
            {
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = 80,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            toolbar.RightToolbarItems.Add(R_toolbaritem);
            toolbar.LeftToolbarItems.Add(L_toolbaritem);
            toolbar.SetBinding(TToolBar.TitleProperty,"Title");

            tabslider = new TTabSlider(){ HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.Start };
            tabslider.SetBinding(TTabSlider.TabsProperty,"Tab");
			tabslider.SetBinding(TTabSlider.SelectedIndexProperty,"SelectedIndex");


            viewpager = new CarouselViewControl()
            {
                ShowIndicators = false,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                InterPageSpacing = 5,
                IsSwipingEnabled = true,
                AnimateTransition = true,
                BackgroundColor = Color.White,
                Orientation = CarouselViewOrientation.Horizontal
            };
            viewpager.SetBinding(CarouselViewControl.ItemsSourceProperty,"Tab");
            viewpager.SetBinding(CarouselViewControl.ItemTemplateProperty,"ItemTemplates");
            viewpager.SetDynamicResource(CarouselViewControl.PositionProperty,"SelectedIndex");
    

			tabslider.SelectedIndexChanged += () => 
            {
                viewpager.Position = tabslider.Selected_Index;
            };

            viewpager.PositionSelected += (sender, e) => 
            {
                tabslider.Selected_Index = viewpager.Position; 
            };


            Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 0,
                Children =
                {
                    toolbar, tabslider, viewpager
                }
            };

        }

     
        protected override void OnAppearing()
        {
            base.OnAppearing();
            tabslider.ViewHasAppeared();
        }

        public override void DisposeResources()
        {
            
        }
    }
}

