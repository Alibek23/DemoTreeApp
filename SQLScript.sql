select client_id, client_name, SUM(order_sum) Total from Clients 
join Orders ON Orders.client_id = Clients.client_id
where order_sum > 50
group by client_id, client_name, order_sum
having SUM(order_sum) > 100