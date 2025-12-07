using System;
using System.Linq;
using System.Collections.Generic;

namespace Da_game_store.Services
{
    public class CartService
    {
        public List<Game> Items { get; } = new();

        public decimal Total => Items.Sum(i => i.Price);

        public event Action? OnChange;

        public void Add(Game g)
        {
            Items.Add(g);
            OnChange?.Invoke();
        }

        public void Remove(Game g)
        {
            Items.Remove(g);
            OnChange?.Invoke();
        }

        public void Clear()
        {
            Items.Clear();
            OnChange?.Invoke();
        }
    }
}