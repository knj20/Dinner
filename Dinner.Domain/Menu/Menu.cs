using Dinner.Domain.Common.Models;
using Dinner.Domain.Dinner.ValueObjects;
using Dinner.Domain.Host.ValueObjects;
using Dinner.Domain.Menu.Entities;
using Dinner.Domain.Menu.ValueObjects;
using Dinner.Domain.MenuReview.ValueObjects;


namespace Dinner.Domain.Menu
{
    public sealed class Menu : AggregateRoot<MenuId>
    {
        private Menu(MenuId id, HostId hostId, string name, string description, List<MenuSection> sections) : base(id)
        {
            HostId = hostId;
            Name = name;
            Description = description;
            _sections = sections;
        }

        private readonly List<MenuSection> _sections = new();
        private readonly List<DinnerId> _dinnerIds = new();
        private readonly List<MenuReviewId> _menuReviewIds = new();
        public string Name { get;}

        public string Description { get;}

        public float? AverageRating { get;}

        public HostId HostId { get; }

        public IReadOnlyList<MenuSection> Sections => _sections.AsReadOnly();
        public IReadOnlyList<DinnerId> DinnerIds => _dinnerIds.AsReadOnly();
        public IReadOnlyList<MenuReviewId> MenuReviewIds => _menuReviewIds.AsReadOnly();

        public DateTime CreatedDateTime { get; set; }

        public DateTime UpdatedDateTime { get; set; }

        public static Menu Create(HostId hostId, string name, string description, List<MenuSection> sections)
        {
            return new Menu(MenuId.CreateUnique(), hostId, name, description, sections);
        }
    }
}
