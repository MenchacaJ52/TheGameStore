using Microsoft.JSInterop;

namespace GameStore.Services
{
    // Simple scoped service to hold the currently-logged-in Account Id for this circuit.
    public class CurrentUserService
    {
        private readonly IJSRuntime _js;

        public CurrentUserService(IJSRuntime js)
        {
            _js = js;
        }

        public int? AccountId { get; private set; }

        public event Action? OnChange;

        public async Task InitializeAsync()
        {
            try
            {
                var idStr = await _js.InvokeAsync<string>("appStorage.get", "accountId");
                if (!string.IsNullOrEmpty(idStr) && int.TryParse(idStr, out var id))
                {
                    AccountId = id;
                }
            }
            catch
            {
                // ignore storage errors
            }

            OnChange?.Invoke();
        }

        public async Task SignInAsync(int accountId)
        {
            AccountId = accountId;
            try
            {
                await _js.InvokeVoidAsync("appStorage.set", "accountId", accountId.ToString());
            }
            catch
            {
                // ignore storage errors
            }
            OnChange?.Invoke();
        }

        public async Task SignOutAsync()
        {
            AccountId = null;
            try
            {
                await _js.InvokeVoidAsync("appStorage.remove", "accountId");
            }
            catch
            {
                // ignore storage errors
            }
            OnChange?.Invoke();
        }
    }
}
