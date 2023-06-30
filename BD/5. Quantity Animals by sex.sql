
--for test 
update Animal set SexId = 2, Price=1500 where AnimalId > 4000;

select Sex.Name, count(*) as Quantity 
from 
Animal
join Sex on Sex.SexId = Animal.SexId
group by Sex.Name
