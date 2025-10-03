using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Persistence.Data;
using Microsoft.EntityFrameworkCore;

public class StatService : IStatService
{
    private readonly EplusDbContext _context;
    public StatService(EplusDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<PickUpLocationStatistics>> GetPickUpLocationStatistics(int numberOfMonths)
    {
        var stats = new List<PickUpLocationStatistics>();
        using (var conn = _context.Database.GetDbConnection())
        {
            conn.Open();
            using (var command = conn.CreateCommand())
            {
                command.CommandText = @"
select t1.id, t1.userCount, t2.weight from 
(select p.id, count(1) userCount from pick_up_location p
join user u on p.id=u.pick_up_location_id
group by p.id) t1
join
(select p.id, sum(t.WeightKg) weight from pick_up_location p
join transport_order t on p.id=t.pick_up_location_id
where t.DateCreated > @from
group by p.id) t2
on t1.id=t2.id
                ";
                var param1 = command.CreateParameter();
                param1.ParameterName = "@from";
                param1.Value = DateTime.UtcNow.AddMonths(-numberOfMonths);
                param1.DbType = System.Data.DbType.DateTime;
                command.Parameters.Add(param1);
                var result = await command.ExecuteReaderAsync();
                while (result.Read())
                {
                    if (result[0] != DBNull.Value)
                    {
                        stats.Add(new PickUpLocationStatistics
                        {
                            LocationId = result.GetInt32(0),
                            NumberOfUsers = result.GetInt32(1),
                            RecentItemTotalWeightKg = result.GetDecimal(2)
                        });
                    }
                }
            }
            conn.Close();
        }
        return stats;
        

    }
}