using Project.Core;
using Project.Core.Caching;
using Project.Core.Data;
using Project.Core.Domain.Directory;
using System;
using System.Linq;

namespace Project.Service.Directory
{
    public class StateService : IStateService
    {
        #region Constants

        private string STATES_ALL_KEY = "project.states.all";
        private string STATES_BY_ID_KEY = "project.states.{0}";
        private string STATES_REMOVE_PATTERN = "project.states";

        #endregion

        #region Fields

        private readonly IRepository<State> _stateRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Constructor
        public StateService(IRepository<State> stateRepository, ICacheManager cacheManager)
        {
            _stateRepository = stateRepository;
            _cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public void DeleteState(State state)
        {
            if (state is null)
                throw new ArgumentException("state");

            _stateRepository.Delete(state);
            _cacheManager.RemoveByPattern(STATES_REMOVE_PATTERN);
        }

        public IPagedList<State> GetAllStates(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var list = _cacheManager.Get(STATES_ALL_KEY, () =>
             {
                 var query = _stateRepository.GetAll();
                 return query.ToList();
             });
            var orderedList = list.OrderBy(s => s.CreatedDate).ThenBy(s => s.DisplayOrder);
            return new PagedList<State>(orderedList, pageIndex, pageSize);
        }

        public State GetStateById(int Id)
        {
            if (Id == 0)
                return default;

            string key = string.Format(STATES_BY_ID_KEY, Id);
            return _cacheManager.Get(key, () =>
              {
                  return _stateRepository.GetById(Id);
              });
        }

        public void InsertState(State state)
        {
            if (state is null)
                throw new ArgumentException("state");

            _stateRepository.Insert(state);
            _cacheManager.RemoveByPattern(STATES_REMOVE_PATTERN);
        }

        public void UpdateState(State state)
        {
            if (state is null)
                throw new ArgumentException("state");

            _stateRepository.Update(state);
            _cacheManager.RemoveByPattern(STATES_REMOVE_PATTERN);
        }

        #endregion
    }
}
