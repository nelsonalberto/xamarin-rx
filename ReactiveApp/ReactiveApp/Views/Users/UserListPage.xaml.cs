using ReactiveApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReactiveApp.Views.Users
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserListPage : ContentPage
    {
        #region Observable Events
        public IObservable<ItemTappedEventArgs> WhenTappedList
        {
            get
            {
                return Observable
                    .FromEventPattern<ItemTappedEventArgs>(
                        h => this.ListView.ItemTapped += h,
                        h => this.ListView.ItemTapped -= h)
                    .Select(x => x.EventArgs);
            }
        }
        #endregion

        public UserListPage()
        {
            InitializeComponent();
            BindingContext = new UserListPageViewModel(this);
        }
    }
}