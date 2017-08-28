﻿using System;
using voltaire.iOS.Renderers;
using voltaire.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(RoundedBoxView),typeof(RoundedBoxViewRenderer))]
namespace voltaire.iOS.Renderers
{
    public class RoundedBoxViewRenderer : BoxRenderer
    {
        
        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {
            base.OnElementChanged(e);

            if(e.OldElement==null && e.NewElement!=null)
            {
                ApplyEffect();
            }

        }

        void ApplyEffect()
        {
			var boxview = Element as BoxView;

			if (this == null)
				return;

			this.Layer.MasksToBounds = true;
			this.Layer.CornerRadius = (nfloat)boxview.HeightRequest / 2;
			this.Layer.BorderWidth = 0;
        }

    }
}
