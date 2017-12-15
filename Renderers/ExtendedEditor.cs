using Xamarin.Forms;

namespace voltaire.Renderers
{
    public class ExtendedEditor : Editor
    {
        public ExtendedEditor()
        {
            TextChanged += EditText;
        }

        private void EditText(object sender, TextChangedEventArgs e)
        {
            var extendedEditor = sender as ExtendedEditor;
            InvalidateMeasure();
            if (extendedEditor != null)
            {

                var val = extendedEditor.Text;

                if (string.IsNullOrEmpty(val))
                    return;


                if (MaxLength > 0 && val.Length > MaxLength)
                {
                    val = val.Remove(val.Length - 1);
                }
                extendedEditor.Text = val;
            }
        }
        public static readonly BindableProperty MaxLengthProperty =
            BindableProperty.Create(
                nameof(MaxLength),
                typeof(int),
                typeof(ExtendedEditor),
                default(int));

        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        public static readonly BindableProperty CharacterSpacingProperty = BindableProperty.Create(nameof(CharacterSpacing), typeof(double), typeof(ExtendedEditor), default(double));

        public double CharacterSpacing
        {
            get { return (double)GetValue(CharacterSpacingProperty); }
            set { SetValue(CharacterSpacingProperty, value); }
        }

        public static readonly BindableProperty LineSpacingProperty = BindableProperty.Create(nameof(LineSpacing), typeof(double), typeof(ExtendedEditor), default(double));

        public double LineSpacing
        {
            get { return (double)GetValue(LineSpacingProperty); }
            set { SetValue(LineSpacingProperty, value); }
        }

        /// <summary>
        /// The BorderColor property
        /// </summary>
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create("BorderColor", typeof(Color), typeof(ExtendedEditor), Color.Transparent);

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        /// <summary>
        /// The BorderWidth property
        /// </summary>
        public static readonly BindableProperty BorderWidthProperty =
            BindableProperty.Create("BorderWidth", typeof(float), typeof(ExtendedEditor), 1.0f);

        /// <summary>
        /// Set Border Width
        /// </summary>
        public float BorderWidth
        {
            get { return (float)GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }

        /// <summary>
        /// The BorderWidth property
        /// </summary>
        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create("CornerRadius", typeof(float), typeof(ExtendedEditor), 1.0f);

        /// <summary>
        /// Set Border Width
        /// </summary>
        public float CornerRadius
        {
            get { return (float)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly BindableProperty PaddingRightProperty = BindableProperty.Create("PaddingRight", typeof(float), typeof(ExtendedEditor), 1.0f);

        public float PaddingRight
        {
            get { return (float)GetValue(PaddingRightProperty); }
            set { SetValue(PaddingRightProperty, value); }
        }

        public static readonly BindableProperty PaddingLeftProperty = BindableProperty.Create("PaddingLeft", typeof(float), typeof(ExtendedEditor), 1.0f);

        public float PaddingLeft
        {
            get { return (float)GetValue(PaddingLeftProperty); }
            set { SetValue(PaddingLeftProperty, value); }
        }
        public static readonly BindableProperty PaddingTopProperty = BindableProperty.Create("PaddingTop", typeof(float), typeof(ExtendedEditor), 1.0f);

        public float PaddingTop
        {
            get { return (float)GetValue(PaddingTopProperty); }
            set { SetValue(PaddingTopProperty, value); }
        }
        public static readonly BindableProperty PaddingBottomProperty = BindableProperty.Create("PaddingBottom", typeof(float), typeof(ExtendedEditor), 1.0f);

        public float PaddingBottom
        {
            get { return (float)GetValue(PaddingBottomProperty); }
            set { SetValue(PaddingBottomProperty, value); }
        }

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create("Placeholder", typeof(string), typeof(ExtendedEditor), default(string));

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create("PlaceholderColor", typeof(Color), typeof(ExtendedEditor), default(Color));

        public Color PlaceholderColor
        {
            get { return (Color)GetValue(PlaceholderColorProperty); }
            set { SetValue(PlaceholderColorProperty, value); }
        }
    }
}
