using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;
using Volvo.LAT.POLineDomain.DomainLayer.RepositoryInterfaces;
using Volvo.NVS.Persistence.NHibernate.Repositories;

namespace Volvo.LAT.PartDomain.InfrastructureLayer.Repositories
{
    public class CostListRepository : GenericRepository<CostList>, ICostListRepositrory
    {
        public CostList FindCostListById(string number)
        {
            var test = this.Session.QueryOver<CostList>().Where(x=>x.CostListId.ToString() == number).List();
            return test.FirstOrDefault();
        }

        public IEnumerable<CostList> GetAllCostList()
        {
            var test = this.Session.QueryOver<CostList>().List();
            return test;
        }

        public IEnumerable<CostList> GetAllCostListByDate(DateTime date)
        {
            var test = this.Session.QueryOver<CostList>().Where(x=>x.Date.Month == date.Month && x.Date.Year == date.Year).List();
            return test;
        }

        public IEnumerable<CostList> GetAllCostListByPolineId(string poLineId)
        {
            var test = this.Session.QueryOver<CostList>().Where(x=>x.PoLineId == poLineId).List();
            return test;
        }

        public IEnumerable<CostList> GetAllCostListByPolineNumberAndId(string poLineId, string poNumber)
        {
            var existingRecords = this.Session.QueryOver<CostList>().Where(x => x.PoNumber == poNumber && x.PoLineId == poLineId).List();
            return existingRecords;
        }

        public bool SaveCostList(List<List<CostList>> listOfCostListList)
        {

            try
            {
                foreach (var costListList in listOfCostListList)
                {
                    using (var transaction = this.Session.BeginTransaction())
                    {
                        foreach (var innerList in costListList)
                        {
                            this.Session.Save("CostList", innerList);
                        }
                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex) { return false; }
            return true;
        }

        public bool DeleteAndUpdateCostList(List<CostList> listOfCostListList, string poNumber, string poLineId)
        {
            try
            {
                var existingRecords = this.Session.QueryOver<CostList>().Where(x => x.PoNumber == poNumber && x.PoLineId == poLineId).List();
                foreach (var toBeDeletedRecords in existingRecords)
                {
                    using (var transaction = this.Session.BeginTransaction())
                    {
                        this.Session.Delete("CostList", toBeDeletedRecords);
                        transaction.Commit();
                    }
                }

                foreach (var costListList in listOfCostListList)
                {
                    using (var transaction = this.Session.BeginTransaction())
                    {
                            this.Session.Save("CostList", costListList);
                            transaction.Commit();
                    }
                }
            }
            catch (Exception ex) { return false; }
            return true;
        }

    }
}
