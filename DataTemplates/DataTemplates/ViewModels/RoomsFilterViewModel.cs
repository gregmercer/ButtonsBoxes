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

        public TimeSpan StartTime
        {
            get
            {
                var dateTime = DateTime.Now;
                return dateTime.TimeOfDay;
            }
            set
            {
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
