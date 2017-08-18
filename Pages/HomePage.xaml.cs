using voltaire.Pages.Base;
namespace voltaire.Pages
{

    public partial class HomePage : BasePage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetMenu(MenuLayout, 0);
        }


        /*  public void ChangeToContacts()
          {
              changeCurrentView("contact");
          }
          public void ChangeToMap()
          {
              changeCurrentView("map");
          }
          public void ChangeToTodo()
          {
              changeCurrentView("todo");
          }
          public void ChangeToAgenda()
          {
              changeCurrentView("agenda");
          }
          public void ChangeToReport()
          {
              changeCurrentView("report");
          }
          public void ChangeToQuotations()
          {
              changeCurrentView("quotations");
          }
          public void ChangeToContract()
          {
              changeCurrentView("contract");
          }
          public void ChangeToGoals()
          {
              changeCurrentView("goals");
          }
          public void ChangeToPodiums()
          {
              changeCurrentView("podiums");
          }
          */
    }
}