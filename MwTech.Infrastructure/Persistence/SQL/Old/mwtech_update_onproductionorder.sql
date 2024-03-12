select * 
from Boms as b
inner join Products as p
on p.Id = b.PartId
where p.ProductNumber  IN ('MPO008','MPO011','MPO029','MPD002','MPD003','MPD013','WOK.KLEJ-KLE-19','008-004-0001')


UPDATE b
set b.OnProductionOrder = 0
from Boms as b
inner join Products as p
on p.Id = b.PartId
where p.ProductNumber  IN ('MPO008','MPO011','MPO029','MPD002','MPD003','MPD013','WOK.KLEJ-KLE-19','008-004-0001')