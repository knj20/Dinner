﻿using Dinner.Domain.Common.Models;
using Dinner.Domain.Menu.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinner.Domain.Menu.Entities
{
    public sealed class MenuSection : Entity<MenuSectionId>
    {
        private MenuSection(MenuSectionId id, string name, string description, List<MenuItem> items) : base(id)
        {
            Name = name;
            Description = description;
            _items = items;
        }
        private readonly List<MenuItem> _items = new();
        public string Name { get; }

        public string Description { get; }

        public IReadOnlyList<MenuItem> Items => _items.AsReadOnly();

        public static MenuSection Create(string name, string description, List<MenuItem> items)
        {
            return new MenuSection(MenuSectionId.CreateUnique(), name, description, items);
        }
    }
}
