using Reactive.Bindings;
using ReactiveApp.Views.Users;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using Xamarin.Forms;

namespace ReactiveApp.ViewModels
{
    public class LoginPageViewModel: BaseViewModel
    {
        #region Properties
        public ReactiveProperty<string> UserName { get; private set; }
        public ReactiveProperty<string> Password { get; private set; }
        public ReactiveCommand SubmitCommand { get; private set; }
        #endregion

        #region Constructor
        public LoginPageViewModel()
        {
            //Declaring reactive properties
            UserName = new ReactiveProperty<string>(initialValue: null);
            Password = new ReactiveProperty<string>(initialValue: null);
            
            //Combining reactive properties and validate to execute
            SubmitCommand = UserName
                .CombineLatest(Password, (u, p) => !string.IsNullOrEmpty(u) && !string.IsNullOrEmpty(p))
                .ToReactiveCommand();
                
            //Subscribing the command
            SubmitCommand.Subscribe(x =>
            {
                if (UserName.Value == "Admin" && Password.Value == "Admin123!")
                    App.Current.MainPage.Navigation.PushAsync(new UserListPage());
                else
                    App.Current.MainPage.DisplayAlert("Login Fail", "The credentials aren't correct, try again!", "Ok");
            });
        }

        #endregion

    }
}
