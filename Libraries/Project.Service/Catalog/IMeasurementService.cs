using Project.Core;
using Project.Core.Domain.Catalog;
using System.Collections.Generic;

namespace Project.Service.Catalog
{
    public interface IMeasurementService
    {
        IPagedList<Measurement> GetAllMeasurements(string name="",string shortName="",
            int pageIndex=0,int pageSize=int.MaxValue);
       IList<Measurement> GetMesurementsList(bool showHidden = false);
        Measurement GetMeasurement(int Id);
        void InsertMeasurement(Measurement measurement);
        void UpdateMeasurement(Measurement measurement);
        void DeleteMeasurement(Measurement measurement);
    }
}
