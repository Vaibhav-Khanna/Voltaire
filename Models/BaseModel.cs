using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace voltaire.Models
{
    public abstract class BaseModel : INotifyPropertyChanged
    {

        private bool isbusy= true;
        public bool IsBusy 
        {
            get { return isbusy; }
            set
            {
                isbusy = value;
                RaisePropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
