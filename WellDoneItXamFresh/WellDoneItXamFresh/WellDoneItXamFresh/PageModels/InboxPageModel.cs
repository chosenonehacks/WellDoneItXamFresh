using FreshMvvm;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellDoneIt.Services;

namespace WellDoneItXamFresh.PageModels
{
    [ImplementPropertyChanged]
    class InboxPageModel : FreshBasePageModel
    {
        private readonly IWellDoneItMobileService _wellDoneItMobileService;

        public InboxPageModel(IWellDoneItMobileService wellDoneItMobileService)
        {
            if (wellDoneItMobileService == null) throw new ArgumentNullException("wellDoneItMobileService");
            _wellDoneItMobileService = wellDoneItMobileService;

        }

        public override void Init(object initData)
        {
            
            //Activities = new ObservableCollection<Activity>(_activityService.GetActivities());
        }

        // Methods are automatically wired up to page
        protected override void ViewIsAppearing(object sender, System.EventArgs e)
        {
            //CoreMethods.DisplayAlert ("Page is appearing", "", "Ok");
            base.ViewIsAppearing(sender, e);
        }

        protected override void ViewIsDisappearing(object sender, System.EventArgs e)
        {
            //CoreMethods.DisplayAlert("Page is disappearing", "", "Ok");
            base.ViewIsDisappearing(sender, e);
        }

        // This is called when a page id pop'd
        public override void ReverseInit(object value)
        {
            //var activity = value as Activity;

            //if (Activities.Contains(activity))
            //{
            //    Activities[Activities.IndexOf(activity)] = activity;
            //}
        }
    }
}
