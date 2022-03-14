using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }
}

namespace App.Models
{
    public class Account : IAsyncAccount
    {
        private readonly RequestSender _requestSender;
        private readonly RecommendationStore _recommendationStore;
        private string _username;
        private CustomerInfo _customerInfo;

        public Account()
        {
            _requestSender = new RequestSender();
            _recommendationStore = new();
            CustomerInfo = new CustomerInfo();
        }

        [JsonConstructor]
        public Account(string username, CustomerInfo customerInfo, string authToken) : this()
        {
            Username = username;
            CustomerInfo = customerInfo;
            AuthToken = authToken;
        }

        public event Action OnUsernameChanged;
        public event Action OnCustomerInfoChanged;

        [JsonIgnore]
        public bool IsAuthorized => Username is not null;
        public string Username
        {
            get => _username;
            protected set
            {
                _username = value;
                OnUsernameChanged?.Invoke();
            }

        }

        public CustomerInfo CustomerInfo
        {
            get => _customerInfo;
            protected set
            {
                _customerInfo = value;
                OnCustomerInfoChanged?.Invoke();
            }
        }

        public string AuthToken
        {
            get => _requestSender.AuthToken;
            init => _requestSender.AuthToken = value;
        }

        public static bool UsernameIsCorrect(string username) => username != null && 6 <= username.Length
                && username.Length <= 20 && !new Regex(@"[^a-z0-9]").IsMatch(username);
        public static bool PasswordIsCorrect(string password) => password != null && 8 <= password.Length
                && password.Length <= 40;

        public async Task<string[]> SignUpAsync(string username, string password) =>
            await Task.Run(() => SignUp(username, password));
        public async Task<string[]> SignInAsync(string username, string password) =>
            await Task.Run(() => SignIn(username, password));
        public async Task<string[]> UpdateCustomerInfoAsync() =>
            await Task.Run(() => UpdateCustomerInfo());
        public async Task<string[]> LoadRecommendationsAsync(Field field) =>
            await Task.Run(() => LoadRecommendations(field));
        public async Task<string[]> LoadAllRecommendationsAsync() =>
            await Task.Run(() => LoadRecommendations(CustomerInfo.Fields.ToArray()));

        public Recommendation[] GetRecommendations(Field field)
                => _recommendationStore.GetRecommendations(field);

        private string[] SignUp(string username, string password)
        {
            string[] signUpErrors = _requestSender.SignUp(username, password);
            if (signUpErrors.Length == 0)
            {
                Username = username;
                string[] updateInfoErrors = UpdateCustomerInfo();
                if (updateInfoErrors.Length != 0)
                {
                    CustomerInfo = new();
                }
                return updateInfoErrors;
            }
            return signUpErrors;
        }

        private string[] SignIn(string username, string password)
        {
            string[] signInErrors = _requestSender.SignIn(username, password);
            if (signInErrors.Length == 0)
            {
                string[] getInfoErrors = _requestSender.GetCustomerInfo(out CustomerInfo customerInfo);
                if (getInfoErrors.Length == 0)
                {
                    Username = username;
                    CustomerInfo = customerInfo;
                }
                return getInfoErrors;
            }
            return signInErrors;
        }

        private string[] UpdateCustomerInfo() => _requestSender.UpdateCustomerInfo(CustomerInfo);

        private string[] LoadRecommendations(Field[] fields)
        {
            List<Field> loadFieldsList = new();
            lock (fields)
            {
                foreach (var field in fields)
                {
                    if (_recommendationStore.ShouldLoad(field))
                    {
                        loadFieldsList.Add(field);
                    }
                }
            }
            if (loadFieldsList.Count > 0)
            {
                string[] getRecommendationsErrors = _requestSender.GetRecommendations(
                        loadFieldsList.ToArray(), out Recommendation[][] newRecommendations);
                if (getRecommendationsErrors.Length == 0)
                {
                    for (int n = 0; n < newRecommendations.Length; n++)
                    {
                        _recommendationStore.AddRecommendations(loadFieldsList[n], newRecommendations[n]);
                    }
                }
                return getRecommendationsErrors;
            }
            return Array.Empty<string>();
        }

        public string[] LoadRecommendations(Field field)
                => LoadRecommendations(new Field[] { field });
    }
}
