using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
public interface IStatService
{
    public Task<IEnumerable<PickUpLocationStatistics>> GetPickUpLocationStatistics(int numberOfMonths);
}