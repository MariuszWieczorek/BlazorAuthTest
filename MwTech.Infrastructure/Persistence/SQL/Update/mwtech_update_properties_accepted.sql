select pr.Id, pr.ProductNumber,
v.Accepted01Date,v.Accepted02Date, v.IsAccepted01, v.IsAccepted02,
u1.FirstName + ' ' + u1.LastName as u1,
u2.FirstName + ' ' + u2.LastName as u2
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.ProductPropertyVersions as v
on v.ProductId = pr.Id
inner join dbo.AspNetUsers AS u1
on u1.Id = v.Accepted01ByUserId
inner join dbo.AspNetUsers AS u2
on u2.Id = v.Accepted02ByUserId
where ca.CategoryNumber in ('OWU','OSU','OSM','OBC','OBB','OBK','ODR','OKA','OKG','OKC','OTT','OKD','OAP','ODA')
-- 7f194520-56b2-4684-b304-4fc98884c35b


UPDATE v
set IsAccepted01 = 1,
IsAccepted02 = 1,
Accepted01Date = GETDATE(),
Accepted02Date = GETDATE(),
Accepted01ByUserId = '7f194520-56b2-4684-b304-4fc98884c35b',
Accepted02ByUserId = '7f194520-56b2-4684-b304-4fc98884c35b'
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.ProductPropertyVersions as v
on v.ProductId = pr.Id
where ca.CategoryNumber in ('OWU','OSU','OSM','OBC','OBB','OBK','ODR','OKA','OKG','OKC','OTT','OKD','OAP','ODA')

