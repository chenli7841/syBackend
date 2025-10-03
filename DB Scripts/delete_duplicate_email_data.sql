select * from email_data_in_warehouse where DateSentEmail is null and OrderId in
(
  select OrderId from email_data_in_warehouse where DateSentEmail is not null
)


select * from email_data_in_warehouse where Id in
(
  select e1.Id from email_data_in_warehouse e1 join
  (
    SELECT OrderId, max(DateCreated) DateCreated, count(1) c FROM
    (select * from email_data_in_warehouse where DateSentEmail is null) t
    group by OrderId
    having count(1) >= 2
  ) t on e1.OrderId=t.OrderId and e1.DateCreated=t.DateCreated
)