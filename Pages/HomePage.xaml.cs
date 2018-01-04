using System;
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


        public void ChangeToContacts(object sender, EventArgs e)
        {
            changeCurrentView("contact");
        }
        public void ChangeToMap(object sender, EventArgs e)
        {
            //changeCurrentView("map");
        }
        public void ChangeToTodo(object sender, EventArgs e)
        {
            //changeCurrentView("todo");
        }
        public void ChangeToAgenda(object sender, EventArgs e)
        {
            //changeCurrentView("agenda");
        }
        public void ChangeToReport(object sender, EventArgs e)
        {
            //changeCurrentView("report");
        }
        public void ChangeToQuotations(object sender, EventArgs e)
        {
            //changeCurrentView("quotation");
        }
        public void ChangeToContract(object sender, EventArgs e)
        {
            //changeCurrentView("contract");
        }
        public void ChangeToGoals(object sender, EventArgs e)
        {
            //changeCurrentView("goals");
        }

    }
}