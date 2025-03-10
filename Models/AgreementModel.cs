﻿using System;
using System.ComponentModel;

namespace voltaire.Models
{
    
    public class AgreementModel : BaseModel 
    {
        public AgreementModel(Agreement _agreement)
        {
            Agreement = _agreement;
           
        }

        Agreement agreement;
        public Agreement Agreement 
        { 
            get { return agreement; }
            set
            {
                agreement = value;

				AgreementTitle = agreement.Title;
				IsSelected = agreement.IsSelected;	
             
                RaisePropertyChanged();
            }
        }

        string title;
        public string AgreementTitle 
        { 
            get { return title; }
            set
            {
                title = value;
				RaisePropertyChanged();
            }
        }

        string image;
        public string Image 
        { 
            get { return image; }
            set
            {
                image = value;
				RaisePropertyChanged();
            }
        }


        bool isselected;

        public bool IsSelected 
        { 
            get { return isselected; }
            set
            {
                isselected = value;

                Image = isselected ? "check1.png" : "uncheck.png";
                agreement.IsSelected = isselected;

				RaisePropertyChanged();
            }
        }

    }
}
