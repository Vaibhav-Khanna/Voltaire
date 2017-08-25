using System;
using voltaire.Models;

namespace voltaire.Controls.Items
{
    public interface ILeftMenuItem
    {
        event EventHandler<MenuLeftItem> ItemClicked;
    }
}
