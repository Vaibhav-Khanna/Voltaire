using Xamarin.Forms;

namespace voltaire.Renderers
{
    public class CustomProgressBar : ProgressBar
    {
        public string ProgressColor { get; private set; }

        public CustomProgressBar(string progressColor)
        {
            ProgressColor = progressColor;
        }
        public CustomProgressBar()
        {

        }
    }
}
