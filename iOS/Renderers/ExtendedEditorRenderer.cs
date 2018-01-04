using System.ComponentModel;
using Foundation;
using voltaire.Renderers;
using voltaire.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedEditor), typeof(ExtendedEditorRenderer))]
namespace voltaire.iOS.Renderers
{
    public class ExtendedEditorRenderer : EditorRenderer
    {
        private UILabel PlaceholderLabel { get; set; }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                Control.ScrollEnabled = false;
                Control.TintColor = UIColor.FromRGB(199, 175, 149);
                //   Control.TextColor = editor.TextColor.ToUIColor();
                SetCharacterSpacing(e.NewElement as ExtendedEditor);
                SetColor(Element as ExtendedEditor);
                SetDefaultSettingColor(Element as ExtendedEditor);
            }
            if (PlaceholderLabel != null) return;

            var element = Element as ExtendedEditor;

            PlaceholderLabel = new PaddedLabel(element.PaddingTop, element.PaddingLeft, element.PaddingRight, element.PaddingBottom);
            PlaceholderLabel.Text = element?.Placeholder;
            PlaceholderLabel.TextColor = element?.PlaceholderColor.ToUIColor();
            PlaceholderLabel.BackgroundColor = UIColor.Clear;

            var edgeInsets = Control.TextContainerInset;
            var lineFragmentPadding = Control.TextContainer.LineFragmentPadding;

            Control.AddSubview(PlaceholderLabel);

            var vConstraints = NSLayoutConstraint.FromVisualFormat(
                "V:|-" + edgeInsets.Top + "-[PlaceholderLabel]-" + edgeInsets.Bottom + "-|", 0, new NSDictionary(),
                NSDictionary.FromObjectsAndKeys(
                    new NSObject[] { PlaceholderLabel }, new NSObject[] { new NSString("PlaceholderLabel") })
            );

            var hConstraints = NSLayoutConstraint.FromVisualFormat(
                "H:|-" + lineFragmentPadding + "-[PlaceholderLabel]-" + lineFragmentPadding + "-|",
                0, new NSDictionary(),
                NSDictionary.FromObjectsAndKeys(
                    new NSObject[] { PlaceholderLabel }, new NSObject[] { new NSString("PlaceholderLabel") })
            );

            PlaceholderLabel.TranslatesAutoresizingMaskIntoConstraints = false;

            Control.AddConstraints(hConstraints);
            Control.AddConstraints(vConstraints);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == nameof(ExtendedEditor.Text) || e.PropertyName == nameof(ExtendedEditor.TextColor) ||
                e.PropertyName == nameof(ExtendedEditor.BorderWidth) || e.PropertyName == nameof(ExtendedEditor.BorderColor) || e.PropertyName == nameof(ExtendedEditor.CornerRadius))
            {
                SetCharacterSpacing(Element as ExtendedEditor);
                SetColor(Element as ExtendedEditor);
                SetDefaultSettingColor(Element as ExtendedEditor);
            }
            if (e.PropertyName == "Text")
            {
                PlaceholderLabel.Hidden = !string.IsNullOrEmpty(Control.Text);
            }
        }

        private void SetColor(ExtendedEditor element)
        {
            Control.TextColor = element.TextColor.ToUIColor();
        }

        private void SetCharacterSpacing(ExtendedEditor element)
        {
            var stringAttributes = new UIStringAttributes
            {
                Font = Control.Font,
                KerningAdjustment = (float)element.CharacterSpacing,
                ParagraphStyle = new NSMutableParagraphStyle()
                {
                    LineSpacing = (float)element.LineSpacing,
                    Alignment = Control.TextAlignment
                }
            };
            if (!string.IsNullOrEmpty(element.Text))
            {
                var attributedText = new NSMutableAttributedString(element.Text);
                attributedText.AddAttributes(stringAttributes, new NSRange(0, element.Text.Length));
                Control.AttributedText = attributedText;
            }
        }

        private void SetDefaultSettingColor(ExtendedEditor extendedEditor)
        {
            Control.Layer.CornerRadius = extendedEditor.CornerRadius;
            Control.Layer.BorderColor = extendedEditor.BorderColor.ToCGColor();
            Control.Layer.BorderWidth = extendedEditor.BorderWidth;
            Control.Layer.MasksToBounds = true;
            Control.TextContainerInset = new UIEdgeInsets(extendedEditor.PaddingTop, extendedEditor.PaddingLeft, extendedEditor.PaddingBottom, extendedEditor.PaddingRight);
            Control.ClipsToBounds = true;
        }
    }

    public sealed class PaddedLabel : UILabel
    {
        private float paddingTop;
        private float paddingLeft;
        private float paddingRight;
        private float paddingBottom;

        private UIEdgeInsets EdgeInsets { get; set; }

        public PaddedLabel(float paddingTop, float paddingLeft, float paddingRight, float paddingBottom)
        {
            this.paddingTop = paddingTop;
            this.paddingLeft = paddingLeft;
            this.paddingRight = paddingRight;
            this.paddingBottom = paddingBottom;
            EdgeInsets = new UIEdgeInsets(paddingTop, paddingLeft, paddingBottom, paddingRight);
        }

        public override void DrawText(CoreGraphics.CGRect rect)
        {
            base.DrawText(EdgeInsets.InsetRect(rect));
        }
    }
}
