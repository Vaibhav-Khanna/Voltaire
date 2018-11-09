using System;
using voltaire.PageModels.Base;
using System.Threading.Tasks;
using voltaire.Models;
using System.Linq;
using voltaire.Helpers.Extensions;
using System.Collections.Generic;

namespace voltaire.PageModels
{
    public class PodiumsPageModel : BasePageModel
    {

        PodiumModel _1;
        public PodiumModel SaleMonth1 { get { return _1; } set { _1 = value; RaisePropertyChanged(); } }

        PodiumModel _2;
        public PodiumModel SaleMonth2 { get { return _2; } set { _2 = value; RaisePropertyChanged(); } }
       
        PodiumModel _3;
        public PodiumModel SaleMonth3 { get { return _3; } set { _3 = value; RaisePropertyChanged(); } }


        PodiumModel _c1;
        public PodiumModel cMonth1 { get { return _c1; } set { _c1 = value; RaisePropertyChanged(); } }

        PodiumModel _c2;
        public PodiumModel cMonth2 { get { return _c2; } set { _c2 = value; RaisePropertyChanged(); } }

        PodiumModel _c3;
        public PodiumModel cMonth3 { get { return _c3; } set { _c3 = value; RaisePropertyChanged(); } }



        public override void Init(object initData)
        {
            base.Init(initData);

            GetData();
        }

        async void GetData()
        {

            SaleMonth1 = new PodiumModel() { Title = DateTime.Now.ToMonthName() };
            SaleMonth2 = new PodiumModel() { Title = DateTime.Now.AddMonths(-1).ToMonthName() };
            SaleMonth3 = new PodiumModel() { Title = DateTime.Now.AddMonths(-2).ToMonthName() };

            cMonth1 = new PodiumModel() { Title = DateTime.Now.ToMonthName() };
            cMonth2 = new PodiumModel() { Title = DateTime.Now.AddMonths(-1).ToMonthName() };
            cMonth3 = new PodiumModel() { Title = DateTime.Now.AddMonths(-2).ToMonthName() };


            IsLoading = true;

            var currentUser = await StoreManager.UserStore.GetCurrentUserAsync();

            // Get sales data for 3 Months

            var CurrentMonthSales = await StoreManager.UserStore.GetSalesForMonth(DateTime.Now.Month, DateTime.Now.Year, true);

            var PreviousMonthSales = await StoreManager.UserStore.GetSalesForMonth(DateTime.Now.AddMonths(-1).Month, DateTime.Now.AddMonths(-1).Year, true);

            var OldMonthSales = await StoreManager.UserStore.GetSalesForMonth(DateTime.Now.AddMonths(-2).Month, DateTime.Now.AddMonths(-2).Year, true);

            //


            // Get checkin data for 3 Months

            var CurrentMonthcheckin = await StoreManager.UserStore.GetSalesForMonth(DateTime.Now.Month, DateTime.Now.Year, false);

            var PreviousMonthcheckin = await StoreManager.UserStore.GetSalesForMonth(DateTime.Now.AddMonths(-1).Month, DateTime.Now.AddMonths(-1).Year, false);

            var OldMonthcheckin = await StoreManager.UserStore.GetSalesForMonth(DateTime.Now.AddMonths(-2).Month, DateTime.Now.AddMonths(-2).Year, false);

            //

            // Process Data

            CurrentMonthSales = RankTheUsers(CurrentMonthSales);
            PreviousMonthSales = RankTheUsers(PreviousMonthSales);
            OldMonthSales = RankTheUsers(OldMonthSales);

            CurrentMonthcheckin = RankTheUsers(CurrentMonthcheckin);
            PreviousMonthcheckin = RankTheUsers(PreviousMonthcheckin);
            OldMonthcheckin = RankTheUsers(OldMonthcheckin);
            //

            // DIsplay Data For Sales
            if (currentUser != null && CurrentMonthSales != null && CurrentMonthSales.Any())
            { 
                var userData = CurrentMonthSales.First(arg => arg.Id == currentUser.Id);

                if (userData != null)
                {
                    CurrentMonthSales.Remove(userData);
                    CurrentMonthSales.Insert(0, userData);
                }

                if (CurrentMonthSales.Count >= 1)
                    SaleMonth1.First = new UserPodiumModel(CurrentMonthSales.ElementAt(0));

                if (CurrentMonthSales.Count >= 2)
                    SaleMonth1.Second = new UserPodiumModel(CurrentMonthSales.ElementAt(1));

                if (CurrentMonthSales.Count >= 3)
                    SaleMonth1.Third = new UserPodiumModel(CurrentMonthSales.ElementAt(2));
            }


            if (currentUser != null && PreviousMonthSales != null && PreviousMonthSales.Any())
            {
                var userData = PreviousMonthSales.First(arg => arg.Id == currentUser.Id);

                if (userData != null)
                {
                    PreviousMonthSales.Remove(userData);
                    PreviousMonthSales.Insert(0, userData);
                }

                if (PreviousMonthSales.Count >= 1)
                    SaleMonth2.First = new UserPodiumModel(PreviousMonthSales.ElementAt(0));

                if (PreviousMonthSales.Count >= 2)
                    SaleMonth2.Second = new UserPodiumModel(PreviousMonthSales.ElementAt(1));

                if (PreviousMonthSales.Count >= 3)
                    SaleMonth2.Third = new UserPodiumModel(PreviousMonthSales.ElementAt(2));
            }

            if (currentUser != null && OldMonthSales != null && OldMonthSales.Any())
            { 
                var userData = OldMonthSales.First(arg => arg.Id == currentUser.Id);

                if (userData != null)
                {
                    OldMonthSales.Remove(userData);
                    OldMonthSales.Insert(0, userData);
                }

                if (OldMonthSales.Count >= 1)
                    SaleMonth3.First = new UserPodiumModel(OldMonthSales.ElementAt(0));

                if (OldMonthSales.Count >= 2)
                    SaleMonth3.Second = new UserPodiumModel(OldMonthSales.ElementAt(1));

                if (OldMonthSales.Count >= 3)
                    SaleMonth3.Third = new UserPodiumModel(OldMonthSales.ElementAt(2));
            }

            //

            //Display Data for Checkins
            if (currentUser != null && CurrentMonthcheckin != null && CurrentMonthcheckin.Any())
            {
                var userData = CurrentMonthcheckin.First(arg => arg.Id == currentUser.Id);

                if (userData != null)
                {
                    CurrentMonthcheckin.Remove(userData);
                    CurrentMonthcheckin.Insert(0, userData);
                }

                if (CurrentMonthcheckin.Count >= 1)
                    cMonth1.First = new UserPodiumModel(CurrentMonthcheckin.ElementAt(0));

                if (CurrentMonthcheckin.Count >= 2)
                    cMonth1.Second = new UserPodiumModel(CurrentMonthcheckin.ElementAt(1));

                if (CurrentMonthcheckin.Count >= 3)
                    cMonth1.Third = new UserPodiumModel(CurrentMonthcheckin.ElementAt(2));
            }


            if (currentUser != null && PreviousMonthcheckin != null && PreviousMonthcheckin.Any())
            {
                var userData = PreviousMonthcheckin.First(arg => arg.Id == currentUser.Id);

                if (userData != null)
                {
                    PreviousMonthcheckin.Remove(userData);
                    PreviousMonthcheckin.Insert(0, userData);
                }

                if (PreviousMonthcheckin.Count >= 1)
                    cMonth2.First = new UserPodiumModel(PreviousMonthcheckin.ElementAt(0));

                if (PreviousMonthcheckin.Count >= 2)
                    cMonth2.Second = new UserPodiumModel(PreviousMonthcheckin.ElementAt(1));

                if (PreviousMonthcheckin.Count >= 3)
                    cMonth2.Third = new UserPodiumModel(PreviousMonthcheckin.ElementAt(2));
            }

            if (currentUser != null && OldMonthcheckin != null && OldMonthcheckin.Any())
            {
                var userData = OldMonthcheckin.First(arg => arg.Id == currentUser.Id);

                if (userData != null)
                {
                    OldMonthcheckin.Remove(userData);
                    OldMonthcheckin.Insert(0, userData);
                }

                if (OldMonthcheckin.Count >= 1)
                    cMonth3.First = new UserPodiumModel(OldMonthcheckin.ElementAt(0));

                if (OldMonthcheckin.Count >= 2)
                    cMonth3.Second = new UserPodiumModel(OldMonthcheckin.ElementAt(1));

                if (OldMonthcheckin.Count >= 3)
                    cMonth3.Third = new UserPodiumModel(OldMonthcheckin.ElementAt(2));
            }
            //


            IsLoading = false;
        }


        List<UserSale> RankTheUsers(List<UserSale> data)
        {
            if (data == null)
                return null;

            int indexRank = 0;
            int PreviousTotal = -1;

            foreach (var item in data)
            {
                if (item.Total != PreviousTotal)
                    indexRank++;
                    
                item.Rank = indexRank;

                PreviousTotal = item.Total;
            }

            return data;
        }

    }
}
