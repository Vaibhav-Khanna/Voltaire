using System;
using Xamarin.Forms;

namespace voltaire.Controls.Items
{
    public interface ILeftMenuItem
    {
        event EventHandler<MenuItem> ItemClicked;
    }
}
