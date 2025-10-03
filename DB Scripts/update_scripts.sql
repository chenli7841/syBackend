update transport_order o
join pick_up_location p on o.pick_up_location_id=p.id
set o.DistrictAdditionalCost = o.WeightKg * p.district_additional_cost
where o.DistrictAdditionalCost = 0 and p.district_additional_cost > 0 and o.WeightKg > 0 and o.Id > 0