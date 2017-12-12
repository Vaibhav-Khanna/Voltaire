using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models;

namespace voltaire.DataStore.Implementation.Stores
{
    public class PartnerStore :  BaseStore<Partner>, IPartnerStore
    {
        
        public override string Identifier => "Partner";

        public int? WeightFilter { get; set; }
       
        public string GradeFilter { get; set; }

        public override async Task<IEnumerable<Partner>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeStore().ConfigureAwait(false);

            if (forceRefresh)
                await PullLatestAsync().ConfigureAwait(false);

            if(WeightFilter!=null && string.IsNullOrWhiteSpace(GradeFilter))
            {
                return await Table.Where(x => x.PartnerWeight == WeightFilter).OrderBy(x => x.Name).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
            else if(WeightFilter != null && !string.IsNullOrWhiteSpace(GradeFilter))
            {
                return await Table.Where(x => x.PartnerWeight == WeightFilter && GradeFilter.Equals(x.Grade) ).OrderBy(x => x.Name).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
            else if(WeightFilter == null && !string.IsNullOrWhiteSpace(GradeFilter))
            {
                return await Table.Where(x => GradeFilter.Equals(x.Grade) ).OrderBy(x => x.Name).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
            else
            {
                return await Table.OrderBy(x => x.Name).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
        }

        public override async Task<IEnumerable<Partner>> GetNextItemsAsync(int currentitemCount)
        {
            await InitializeStore().ConfigureAwait(false);

            try
            {
                if (WeightFilter != null && string.IsNullOrWhiteSpace(GradeFilter))
                {
                    return await Table.Where(x => x.PartnerWeight == WeightFilter).OrderBy(x => x.Name).Skip(currentitemCount).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
                }
                else if (WeightFilter != null && !string.IsNullOrWhiteSpace(GradeFilter))
                {
                    return await Table.Where(x => x.PartnerWeight == WeightFilter && GradeFilter.Equals(x.Grade)).OrderBy(x => x.Name).Skip(currentitemCount).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
                }
                else if (WeightFilter == null && !string.IsNullOrWhiteSpace(GradeFilter))
                {
                    return await Table.Where(x => GradeFilter.Equals(x.Grade)).OrderBy(x => x.Name).Skip(currentitemCount).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
                }
                else
                {
                    return await Table.OrderBy(x => x.Name).Skip(currentitemCount).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
                }
               
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Partner>> Search(string QueryText)
        {
            await InitializeStore().ConfigureAwait(false);           
          
            var items = await Table.Where(s => s.Name.Contains(QueryText) || s.CompanyName.Contains(QueryText)).OrderBy( x => x.Name ).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);

            if (items == null)
                return null;

            return items;
        }

    }
}
