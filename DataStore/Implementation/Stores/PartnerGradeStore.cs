using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class PartnerGradeStore: BaseStore<PartnerGrade>, IPartnerGradeStore
    {
        public override string Identifier => "PartnerGrade";
    }
}
