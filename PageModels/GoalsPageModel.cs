using System;
using System.Collections.ObjectModel;
using System.Linq;
using voltaire.Helpers.Collections;
using voltaire.Models;
using voltaire.PageModels.Base;

namespace voltaire.PageModels
{
    public class GoalsPageModel : BasePageModel
    {

        public ObservableCollection<Salesman> salesmens { get; set; }

        public string SalesmensCount { get; set; }

        public ObservableCollection<ObservableGroupCollection<string, SalesmanModel>> SalesmensItems { get; set; }


        //INIT data form page  freshmvvm
        public override void Init(object initData)
        {
            salesmens = new ObservableCollection<Salesman>
            {
                new Salesman {
                    FirstName= "Aaron",
                    LastName="Robertson"},
                new Salesman {
                    FirstName= "Ade",
                    LastName="Flowers"},
                new Salesman {
                    FirstName= "Albert",
                    LastName="Oyegun"},
                new Salesman {
                    FirstName= "Alex",
                    LastName="Badgio"},
                new Salesman {
                    FirstName= "Alex",
                    LastName="Rodriguez"},
                new Salesman {
                    FirstName= "Anais",
                    LastName="Vespa"},
                new Salesman {
                    FirstName= "Antoine",
                    LastName="Thierry"},
                new Salesman {
                    FirstName= "Aurélie",
                    LastName="Laoste"},
                new Salesman {
                    FirstName= "Ayana",
                    LastName="Limboa"},
                new Salesman {
                    FirstName= "Azan",
                    LastName="Lombok"},
                new Salesman {
                    FirstName= "Antoine",
                    LastName="Blacklisle"},
                new Salesman {
                    FirstName= "Jean-Michel",
                    LastName="Toto"},
                new Salesman {
                    FirstName= "Francis",
                    LastName="Lalane"},
                new Salesman {
                    FirstName= "Johny",
                    LastName="Begood"},
                new Salesman {
                    FirstName= "Zinedine",
                    LastName="Zidane"},
                new Salesman {
                    FirstName= "Jean",
                    LastName="Bon"},
                new Salesman {
                    FirstName= "Mickael",
                    LastName="Jackson"},
                new Salesman {
                    FirstName= "Mickael",
                    LastName="Jordan"},
            };

            if (salesmens.Count > 1) SalesmensCount = salesmens.Count + " " + Resources.AppResources.Salesmens;
            else SalesmensCount = salesmens.Count + " " + Resources.AppResources.Salesman;


            var models = salesmens.Select(i => new SalesmanModel(i) { navigation = CoreMethods }).ToList();

            var groupedData =
                models.OrderBy(p => p.Salesman.LastName)
                    .GroupBy(p => p.NameSort)
                    .Select(p => new ObservableGroupCollection<string, SalesmanModel>(p))
                    .ToList();

            SalesmensItems = new ObservableCollection<ObservableGroupCollection<string, SalesmanModel>>(groupedData);


        }
    }
}
