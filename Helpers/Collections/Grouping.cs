using System.Collections.ObjectModel;
using System.Linq;
namespace voltaire.Helpers.Collections
{
    public class ObservableGroupCollection<S, T> : ObservableCollection<T>
    {
        public ObservableGroupCollection(IGrouping<S, T> group)
            : base(group)
        {
            Key = group.Key;
        }

        public S Key { get; }
    }
}
