using Newtonsoft.Json;
using Reactive.Bindings;
using ReactiveApp.Helpers;
using ReactiveApp.Models.API.Response;
using ReactiveApp.Views.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using Xamarin.Forms;

namespace ReactiveApp.ViewModels
{
    public class UserListPageViewModel: BaseViewModel
    {
        #region Properties
        public ReactiveProperty<ObservableCollection<UserResponse>> UserListing { get; set; }
        #endregion

        #region Constructor
        public UserListPageViewModel(UserListPage page) 
        {
            UserListing = new ReactiveProperty<ObservableCollection<UserResponse>>();

            Observable
                .FromAsync(() => _httpClient.GetAsync(Constants.UserUrl))
                .SubscribeOn(TaskPoolScheduler.Default)
                .Do(x => Debug.WriteLine($"Is Request succesfull? :{x.IsSuccessStatusCode}"))
                .SelectMany(async x =>
                {
                    var result = await x.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<UserResponse>>(result);
                })
                .Subscribe(x => 
                {
                    UserListing.Value = new ObservableCollection<UserResponse>(x); 
                });


            page.WhenTappedList.Subscribe(x => 
            {
                var item = x.Item as UserResponse;

                App.Current.MainPage.DisplayAlert("User Selected", $"Name: {item.Name}\nEmail: {item.Email}", "Ok");
            });
        }
        #endregion
    }
}
