using Dinner.Domain.Menu;


namespace Dinner.Application.Common.Interfaces.Persistance
{
    public interface IMenuRepository
    {
        void Add(Menu menu);
    }
}
