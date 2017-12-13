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


        public override async Task<IEnumerable<Partner>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeStore().ConfigureAwait(false);

            if (forceRefresh)
                await PullLatestAsync().ConfigureAwait(false);

            return await Table.OrderBy(x => x.Name).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);

        }

        public async Task<IEnumerable<Partner>> GetItemsAsync(int? Weight, string Grade, bool forceRefresh = false)
        {
            await InitializeStore().ConfigureAwait(false);

            if (forceRefresh)
                await PullLatestAsync().ConfigureAwait(false);

            if (Weight != null && string.IsNullOrWhiteSpace(Grade))
            {
                return await Table.Where(x => x.PartnerWeight == Weight).OrderBy(x => x.Name).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
            else if (Weight != null && !string.IsNullOrWhiteSpace(Grade))
            {
                return await Table.Where(x => x.PartnerWeight == Weight && Grade.Equals(x.Grade)).OrderBy(x => x.Name).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
            else if (Weight == null && !string.IsNullOrWhiteSpace(Grade))
            {
                return await Table.Where(x => Grade.Equals(x.Grade)).OrderBy(x => x.Name).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
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

                return await Table.OrderBy(x => x.Name).Skip(currentitemCount).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Partner>> GetNextItemsAsync(int currentitemCount, int? Weight, string Grade, bool forceRefresh = false)
        {
            await InitializeStore().ConfigureAwait(false);

            try
            {
                if (Weight != null && string.IsNullOrWhiteSpace(Grade))
                {
                    return await Table.Where(x => x.PartnerWeight == Weight).OrderBy(x => x.Name).Skip(currentitemCount).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
                }
                else if (Weight != null && !string.IsNullOrWhiteSpace(Grade))
                {
                    return await Table.Where(x => x.PartnerWeight == Weight && Grade.Equals(x.Grade)).OrderBy(x => x.Name).Skip(currentitemCount).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
                }
                else if (Weight == null && !string.IsNullOrWhiteSpace(Grade))
                {
                    return await Table.Where(x => Grade.Equals(x.Grade)).OrderBy(x => x.Name).Skip(currentitemCount).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
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

        public async Task<IEnumerable<Partner>> Search(string QueryText, int? Weight, string Grade)
        {
            await InitializeStore().ConfigureAwait(false);

            IEnumerable<Partner> items;

            if (Weight != null && string.IsNullOrWhiteSpace(Grade))
            {
                items = await Table.Where(x => x.PartnerWeight == Weight).Where(s => s.Name.Contains(QueryText) || s.CompanyName.Contains(QueryText)).OrderBy(x => x.Name).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
            else if (Weight != null && !string.IsNullOrWhiteSpace(Grade))
            {
                items = await Table.Where(x => x.PartnerWeight == Weight && Grade.Equals(x.Grade)).Where(s => s.Name.Contains(QueryText) || s.CompanyName.Contains(QueryText)).OrderBy(x => x.Name).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
            else if (Weight == null && !string.IsNullOrWhiteSpace(Grade))
            {
                items = await Table.Where(x => Grade.Equals(x.Grade)).Where(s => s.Name.Contains(QueryText) || s.CompanyName.Contains(QueryText)).OrderBy(x => x.Name).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
            else
            {
                items = await Table.Where(s => s.Name.Contains(QueryText) || s.CompanyName.Contains(QueryText)).OrderBy(x => x.Name).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }

            if (items == null)
                return null;

            return items;
        }

    }
}
