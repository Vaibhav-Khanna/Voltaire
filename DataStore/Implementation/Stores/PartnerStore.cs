using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models;

namespace voltaire.DataStore.Implementation.Stores
{
    public class PartnerStore : BaseStore<Partner>, IPartnerStore
    {

        public override string Identifier => "Partner";


        public virtual async Task<Partner> GetCustomerByMessageAuthorIdAsync(string messageAuthorId)
        {
            await InitializeStore().ConfigureAwait(false);
            await PullLatestAsync().ConfigureAwait(false);

            // var item = await Table.LookupAsync(messageAuthorId);
            var items = await Table.Where(s => s.ExternalId.ToString() == messageAuthorId).ToListAsync().ConfigureAwait(false);

            if (items == null || items.Count == 0)
                return null;

            return items[0];
            // return item;
        }

        public override async Task<IEnumerable<Partner>> GetItemsAsync(bool forceRefresh = false, bool AllItems = false)
        {
            await InitializeStore().ConfigureAwait(false);

            if (forceRefresh)
                await PullLatestAsync().ConfigureAwait(false);

            if (AllItems)
                return await Table.OrderBy(x => x.Name).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            else
                return await Table.OrderBy(x => x.Name).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Partner>> GetItemsAsync(int? Weight, long? Grade, bool forceRefresh = false)
        {
            await InitializeStore().ConfigureAwait(false);

            if (forceRefresh)
                await PullLatestAsync().ConfigureAwait(false);

            if (Weight != null && Grade == null)
            {
                return await Table.Where(x => x.PartnerWeight == Weight).OrderBy(x => x.Name).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
            else if (Weight != null && Grade != null)
            {
                return await Table.Where(x => x.PartnerWeight == Weight && x.GradeId == Grade).OrderBy(x => x.Name).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
            else if (Weight == null && Grade != null)
            {
                return await Table.Where(x => x.GradeId == Grade).OrderBy(x => x.Name).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
            else
            {
                return await Table.OrderBy(x => x.Name ).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
        }


        public async Task<IEnumerable<Partner>> GetItemsWithValidCordinates()
        {
            await InitializeStore().ConfigureAwait(false);

            try
            {
                return await Table.ToEnumerableAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                return null;
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

        public async Task<IEnumerable<Partner>> GetNextItemsAsync(int currentitemCount, int? Weight, long? Grade, bool forceRefresh = false)
        {
            await InitializeStore().ConfigureAwait(false);

            try
            {
                if (Weight != null && Grade == null)
                {
                    return await Table.Where(x => x.PartnerWeight == Weight).OrderBy(x => x.Name).Skip(currentitemCount).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
                }
                else if (Weight != null && Grade != null)
                {
                    return await Table.Where(x => x.PartnerWeight == Weight && x.GradeId == Grade).OrderBy(x => x.Name).Skip(currentitemCount).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
                }
                else if (Weight == null && Grade != null)
                {
                    return await Table.Where(x => x.GradeId == Grade).OrderBy(x => x.Name).Skip(currentitemCount).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
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

        public async Task<IEnumerable<Partner>> Search(string QueryText, int? Weight, long? Grade,int CurrentItems = 0)
        {
            await InitializeStore().ConfigureAwait(false);

            IEnumerable<Partner> items;

            if (Weight != null && Grade == null)
            {
                items = await Table.Where(x => x.PartnerWeight == Weight).Where(s => s.Name.Contains(QueryText) || s.ParentName.Contains(QueryText)).OrderBy(x => x.Name).Skip(CurrentItems).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
            else if (Weight != null && Grade != null)
            {
                items = await Table.Where(x => x.PartnerWeight == Weight && x.GradeId == Grade).Where(s => s.Name.Contains(QueryText) || s.ParentName.Contains(QueryText)).OrderBy(x => x.Name).Skip(CurrentItems).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
            else if (Weight == null && Grade != null)
            {
                items = await Table.Where(x => x.GradeId == Grade).Where(s => s.Name.Contains(QueryText) || s.ParentName.Contains(QueryText)).OrderBy(x => x.Name).Skip(CurrentItems).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
            else
            {
                items = await Table.Where(s => s.Name.Contains(QueryText) || s.ParentName.Contains(QueryText)).OrderBy(x => x.Name).Skip(CurrentItems).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }

            if (items == null)
                return null;

            return items;
        }

    }
}
