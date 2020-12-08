using Project.Core;
using Project.Core.Caching;
using Project.Core.Data;
using Project.Core.Domain.Catalog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Project.Service.Catalog
{
    public class MeasurementService : IMeasurementService
    {
        #region Constants

        private const string MEASUREMENTS_ALL_KEY = "project.measurement.all";
        private const string MEASUREMENT_REMOVE_PATTERN = "project.measurement";

        #endregion

        #region Fields

        private readonly IRepository<Measurement> _measurementRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Constructor

        public MeasurementService(IRepository<Measurement> measurementRepository, ICacheManager cacheManager)
        {
            _measurementRepository = measurementRepository;
            _cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public virtual void DeleteMeasurement(Measurement measurement)
        {
            if (measurement is null)
                throw new ArgumentException("measurement");

            _measurementRepository.Delete(measurement);
            _cacheManager.RemoveByPattern(MEASUREMENT_REMOVE_PATTERN);
        }

        public IPagedList<Measurement> GetAllMeasurements(string name = "", string shortName = "",
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _measurementRepository.Table;
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(m => m.Name.Contains(name));
            if (!string.IsNullOrWhiteSpace(shortName))
                query = query.Where(m => m.ShortName.Contains(shortName));

            query = query.OrderByDescending(m => m.CreatedOnUtc);
            return new PagedList<Measurement>(list: query, pageIndex: pageIndex, pageSize: pageSize);
        }

        public virtual IList<Measurement> GetMesurementsList(bool showHidden = false)
        {
            string key = string.Format(MEASUREMENTS_ALL_KEY);
            var list = _cacheManager.Get(key, () =>
             {
                 return (from m in _measurementRepository.Table
                         where !showHidden
                         select m).ToList();
             });
            return list;
        }

        public virtual Measurement GetMeasurement(int Id)
        {
            if (Id == 0)
                return default;

           return _measurementRepository.GetById(Id);
        }

        public virtual void InsertMeasurement(Measurement measurement)
        {
            if (measurement is null)
                throw new ArgumentException("measurement");

            _measurementRepository.Insert(measurement);
            _cacheManager.RemoveByPattern(MEASUREMENT_REMOVE_PATTERN);
        }

        public virtual void UpdateMeasurement(Measurement measurement)
        {
            if (measurement is null)
                throw new ArgumentException("measurement");

            _measurementRepository.Update(measurement);
            _cacheManager.RemoveByPattern(MEASUREMENT_REMOVE_PATTERN);
        }

        #endregion
    }
}
