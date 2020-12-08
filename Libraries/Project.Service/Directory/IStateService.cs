using Project.Core;
using Project.Core.Domain.Directory;

namespace Project.Service.Directory
{
    public interface IStateService
    {
        IPagedList<State> GetAllStates(int pageIndex = 0, int pageSize = int.MaxValue);
        State GetStateById(int Id);
        void InsertState(State state);
        void UpdateState(State state);
        void DeleteState(State state);
    }
}
