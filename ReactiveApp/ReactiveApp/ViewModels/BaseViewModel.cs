using ReactiveApp.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;

namespace ReactiveApp.ViewModels
{
    public class BaseViewModel
    {
        #region General Properties

        public readonly HttpClient _httpClient = new HttpClient() { BaseAddress = new Uri(Constants.BaseUrl) };

        #endregion
    }
}
