using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using GameStore.Models;
using Microsoft.JSInterop;

namespace GameStore.Services
{
    public class CartService
    {
        private readonly IJSRuntime _js;

        public List<Game> Items { get; } = new();

        public decimal Total => Items.Sum(i => i.Price);

        public event Action? OnChange;

        public CartService(IJSRuntime js)
        {
            _js = js;
        }

        public void Add(Game g)
        {
            Items.Add(g);
            OnChange?.Invoke();
            _ = SaveAsync();
        }

        public void Remove(Game g)
        {
            Items.Remove(g);
            OnChange?.Invoke();
            _ = SaveAsync();
        }

        public void Clear()
        {
            Items.Clear();
            OnChange?.Invoke();
            _ = SaveAsync();
        }

        public async Task InitializeAsync()
        {
            try
            {
                var json = await _js.InvokeAsync<string>("localStorage.getItem", "gameStoreCart");
                if (!string.IsNullOrEmpty(json))
                {
                    var list = JsonSerializer.Deserialize<List<Game>>(json);
                    if (list != null)
                    {
                        Items.Clear();
                        Items.AddRange(list);
                        OnChange?.Invoke();
                    }
                }
            }
            catch
            {
                // ignore JS errors
            }
        }

        public async Task SaveAsync()
        {
            try
            {
                var json = JsonSerializer.Serialize(Items);
                await _js.InvokeVoidAsync("localStorage.setItem", "gameStoreCart", json);
            }
            catch
            {
                // ignore failures
            }
        }
    }
}