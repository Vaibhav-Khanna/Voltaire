using System;
using System.Collections.ObjectModel;
using System.Linq;
using voltaire.Helpers.Collections;
using voltaire.Models;
using voltaire.PageModels.Base;

namespace voltaire.PageModels
{
    public class ReportsPageModel : BasePageModel
    {
        public ObservableCollection<Salesman> salesmens { get; set; }

        public ObservableCollection<ObservableGroupCollection<string, SalesmanModel>> SalesmensItems { get; set; }

        public ObservableCollection<StateColor> ColorItems { get; set; }

        public override void Init(object initData)
        {
            salesmens = new ObservableCollection<Salesman>
            {
                new Salesman {
                    FirstName= "Aaron",
                    LastName="Robertson",
                    CheckIns=120,
                    NewContacts=3,
                    State= new State{Red=4,Orange=220,Green=356,Gray=147}  },
                new Salesman {
                    FirstName= "Ade",
                    LastName="Flowers",
                    CheckIns=120,
                    NewContacts=3,
                    State= new State{Red=4,Orange=220,Green=356,Gray=147}},
                new Salesman {
                    FirstName= "Albert",
                    LastName="Oyegun",
                    CheckIns=120,
                    NewContacts=3,
                    State= new State{Red=4,Orange=220,Green=356,Gray=147}},
                new Salesman {
                    FirstName= "Alex",
                    LastName="Badgio",
                    CheckIns=120,
                    NewContacts=3,
                    State= new State{Red=4,Orange=220,Green=356,Gray=147}},
                new Salesman {
                    FirstName= "Alex",
                    LastName="Rodriguez",
                    CheckIns=120,
                    NewContacts=3,
                    State= new State{Red=4,Orange=220,Green=356,Gray=147}},
                new Salesman {
                    FirstName= "Anais",
                    LastName="Vespa",
                    CheckIns=120,
                    NewContacts=3,
                    State= new State{Red=4,Orange=220,Green=356,Gray=147}},
                new Salesman {
                    FirstName= "Antoine",
                    LastName="Thierry",
                    CheckIns=120,
                    NewContacts=3,
                    State= new State{Red=15,Orange=43,Green=356,Gray=267}},
                new Salesman {
                    FirstName= "Aurélie",
                    LastName="Laoste",
                    CheckIns=120,
                    NewContacts=3,
                    State= new State{Red=4,Orange=23,Green=356,Gray=147}},
                new Salesman {
                    FirstName= "Ayana",
                    LastName="Limboa",
                    CheckIns=120,
                    NewContacts=3,
                    State= new State{Red=4,Orange=220,Green=90,Gray=147}},
                new Salesman {
                    FirstName= "Azan",
                    LastName="Lombok",
                    CheckIns=120,
                    NewContacts=3,
                    State= new State{Red=4,Orange=220,Green=356,Gray=147}},
                new Salesman {
                    FirstName= "Antoine",
                    LastName="Blacklisle",
                    CheckIns=120,
                    NewContacts=3,
                    State= new State{Red=4,Orange=220,Green=356,Gray=147}},
                new Salesman {
                    FirstName= "Jean-Michel",
                    LastName="Toto",
                    CheckIns=120,
                    NewContacts=3,
                    State= new State{Red=4,Orange=220,Green=356,Gray=147}},
                new Salesman {
                    FirstName= "Francis",
                    LastName="Lalane",
                    CheckIns=120,
                    NewContacts=3,
                    State= new State{Red=18,Orange=220,Green=356,Gray=147}},
                new Salesman {
                    FirstName= "Johny",
                    LastName="Begood",
                    CheckIns=120,
                    NewContacts=3,
                    State= new State{Red=90,Orange=220,Green=3687,Gray=147}},
                new Salesman {
                    FirstName= "Zinedine",
                    LastName="Zidane",
                    CheckIns=120,
                    NewContacts=3,
                    State= new State{Red=4,Orange=34,Green=356,Gray=147}},
                new Salesman {
                    FirstName= "Jean",
                    LastName="Bon",
                    CheckIns=120,
                    NewContacts=3,
                    State= new State{Red=4,Orange=220,Green=98,Gray=147}},
                new Salesman {
                    FirstName= "Mickael",
                    LastName="Jackson",
                    CheckIns=120,
                    NewContacts=3,
                    State= new State{Red=4,Orange=220,Green=356,Gray=147}},
                new Salesman {
                    FirstName= "Mickael",
                    LastName="Jordan",
                    CheckIns=120,
                    NewContacts=3,
                    State= new State{Red=4,Orange=220,Green=356,Gray=147}}
            };


            var models = salesmens.Select(i => new SalesmanModel(i) { navigation = CoreMethods }).ToList();

            var groupedData =
                models.OrderBy(p => p.Salesman.LastName)
                    .GroupBy(p => p.NameSort)
                    .Select(p => new ObservableGroupCollection<string, SalesmanModel>(p))
                    .ToList();

            SalesmensItems = new ObservableCollection<ObservableGroupCollection<string, SalesmanModel>>(groupedData);

            // ColorItem Picker
            ColorItems = new ObservableCollection<StateColor> {

                new StateColor{ColorName = ColorEnum.All.ToString(), IsAllItem=true },
                new StateColor{ColorName = ColorEnum.Red.ToString(), IsAllItem=false },
                new StateColor{ColorName = ColorEnum.Orange.ToString(), IsAllItem=false },
                new StateColor{ColorName = ColorEnum.Green.ToString(), IsAllItem=false },
                new StateColor{ColorName = ColorEnum.Gray.ToString(), IsAllItem=false }};


        }
    }
}
