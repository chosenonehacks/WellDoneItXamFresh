using FreshMvvm;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellDoneItXamFresh.PageModels
{
    [ImplementPropertyChanged]
    class TodayPageModel : FreshBasePageModel
    {
        public TodayPageModel()
        {

        }

        public override void Init(object initData)
        {
            //Activities = new ObservableCollection<Activity>(_activityService.GetActivities());
        }

        // Methods are automatically wired up to page
        protected override void ViewIsAppearing(object sender, System.EventArgs e)
        {
            CoreMethods.DisplayAlert("Page is appearing", "", "Ok");
            base.ViewIsAppearing(sender, e);
        }

        protected override void ViewIsDisappearing(object sender, System.EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
        }

        // This is called when a page id pop'd
        public override void ReverseInit(object value)
        {
            
        }
    }
}
