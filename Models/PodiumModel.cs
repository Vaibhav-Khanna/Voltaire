using System;
using System.Collections.Generic;

namespace voltaire.Models
{
    public class PodiumModel : BaseModel
    {
        string title;
        public string Title { get { return title; } set { title = value; RaisePropertyChanged(); } }

        UserPodiumModel first;
        public UserPodiumModel First
        {
            get { return first; }
            set
            {
                first = value;

                if (value != null)
                    IsFirstVisible = true;

                RaisePropertyChanged();
                RaisePropertyChanged("IsFirstVisible");
            }
        }

        UserPodiumModel second;
        public UserPodiumModel Second
        {
            get { return second; }
            set
            {
                second = value; if (value != null)
                    IsSecondVisible = true;

                RaisePropertyChanged();
                RaisePropertyChanged("IsSecondVisible");
            }
        }

        UserPodiumModel third;
        public UserPodiumModel Third
        {
            get { return third; }
            set
            {
                third = value; if (value != null)
                    IsThirdVisible = true; 

                RaisePropertyChanged();
                RaisePropertyChanged("IsThirdVisible");
            }
        }


        public bool IsFirstVisible { get; private set; }

        public bool IsSecondVisible { get; private set; }

        public bool IsThirdVisible { get; private set; }
    }
}
