using System;

namespace voltaire.Models
{
	public class UserPodiumModel : BaseModel
    {

        public UserPodiumModel(UserSale userSale)
        {
            Name = userSale.Name;
            Id = userSale.Id;
            TotalSales = userSale.Total;
            Rank = userSale.Rank;
        }

        public string Name { get; set; }

        public string Id { get; set; }

        public int TotalSales { get; set; }

        public int Rank { get; set; }

    }
}
