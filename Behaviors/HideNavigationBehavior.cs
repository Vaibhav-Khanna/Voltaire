using Xamarin.Forms;

namespace voltaire.Behaviors
{
    public class HideNavigationBehavior : Behavior<Page>
    {
        protected override void OnAttachedTo(Page bindable)
        {
            base.OnAttachedTo(bindable);

            NavigationPage.SetHasNavigationBar(bindable, false);
        }
    }
}
