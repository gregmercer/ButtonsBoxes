using System;

using Xamarin.Forms;

using SlideOverKit;

namespace DataTemplates.ViewModels
{
    public class RoomsFilterViewModel
    {
        public RoomsFilterViewModel()
        {
        }

        private SlideMenuView slideMenu = null;
        public SlideMenuView SlideMenu
        {
            get
            {
                return this.slideMenu;
            }
            set
            {
                this.slideMenu = value;
            }
        }

        private DateTime onDate = DateTime.Now;
        public DateTime OnDate
        {
            get
            {
                return this.onDate;
            }
            set
            {
                this.onDate = value;
            }
        }

        private TimeSpan startTime;
        public TimeSpan StartTime
        {
            get
            {
                if (this.startTime.ToString() == "00:00:00") 
                {
                    var dateTime = DateTime.Now;
                    return dateTime.TimeOfDay;
                }
                else
                {
                    return this.startTime;
                }
            }
            set
            {
                this.startTime = value;
            }
        }

        public Command DoOK {
            get {
                return new Command (() => {
                    MenuContainerPage menuContainerPage = this.slideMenu.Parent as MenuContainerPage;
                    menuContainerPage.HideMenu();
                });
            }
        }

        public Command DoCancel {
            get {
                return new Command (() => {
                    MenuContainerPage menuContainerPage = this.slideMenu.Parent as MenuContainerPage;
                    menuContainerPage.HideMenu();
                });
            }
        }
    }
}
