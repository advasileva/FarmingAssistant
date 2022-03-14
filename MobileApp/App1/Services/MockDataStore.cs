using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using App.Models;

namespace App.Services
{
    public class MockDataStore : IDataStore<Account>
    {
        private readonly List<Account> _accounts;

        public MockDataStore()
        {
            _accounts = new() { new Account() };
        }

        public async Task<bool> AddItemAsync(Account account)
        {
            _accounts.Add(account);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Account account)
        {
            var oldAccount = _accounts.Where(item => item.Username == account.Username).FirstOrDefault();
            _accounts.Remove(oldAccount);
            _accounts.Add(account);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string username)
        {
            var oldAccount = _accounts.Where((Account item) => item.Username == username).FirstOrDefault();
            _accounts.Remove(oldAccount);
            return await Task.FromResult(true);
        }

        public async Task<Account> GetItemAsync(string username)
        {
            return await Task.FromResult(_accounts.FirstOrDefault(item => item.Username == username));
        }

        public async Task<IEnumerable<Account>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(_accounts);
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                string data = JsonSerializer.Serialize(_accounts);
                App1.App.Current.Properties.TryAdd("AccountData", data);
                App1.App.Current.Properties["AccountData"] = data;
                return await Task.FromResult(true);
            }
            catch { }
            return await Task.FromResult(false);
        }

        public async Task<bool> LoadAsync()
        {
            try
            {
                string data = App1.App.Current.Properties["AccountData"].ToString();
                var accounts = JsonSerializer.Deserialize<List<Account>>(data);
                if (accounts.Count > 0)
                {
                    _accounts.Clear();
                    foreach (var account in accounts)
                    {
                        _accounts.Add(account);
                    }
                    return await Task.FromResult(true);
                }
            }
            catch { }
            return await Task.FromResult(false);
        }
    }
}