
select ca.CategoryNumber+'-'+trim(cast(pr.TechCardNumber as varchar(10)))+'-'+m.MachineNumber+'-'+trim(cast(n.AlternativeNo as varchar(10)))+'.'+trim(cast(n.ProductSettingVersionNumber as varchar(10)))
from dbo.ProductSettingVersions as n
inner join dbo.Products as pr
on pr.Id = n.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.Machines as m
on m.Id = n.MachineId
where ca.CategoryNumber = 'OKA'

UPDATE n
set 
Name = ca.CategoryNumber+'-'+trim(cast(pr.TechCardNumber as varchar(10)))+'-'+m.MachineNumber+'-'+trim(cast(n.AlternativeNo as varchar(10)))+'.'+trim(cast(n.ProductSettingVersionNumber as varchar(10)))
, Accepted01ByUserId = '9c3f2472-379b-48ad-b690-b8afaedd4262'
, Accepted02ByUserId = '9c3f2472-379b-48ad-b690-b8afaedd4262'
, Accepted03ByUserId = '9c3f2472-379b-48ad-b690-b8afaedd4262'
, IsAccepted01 = 1
, IsAccepted02 = 1
, IsAccepted03 = 1
, Accepted01Date = GETDATE()
, Accepted02Date = GETDATE()
, Accepted03Date = GETDATE()
, Rev = Rev + 1
from dbo.ProductSettingVersions as n
inner join dbo.Products as pr
on pr.Id = n.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.Machines as m
on m.Id = n.MachineId
where ca.CategoryNumber = 'OKA'

/*
Delete p
from dbo.ProductSettingVersions as n
inner join dbo.ProductSettingVersionPositions as p
on p.ProductSettingVersionId = n.Id
inner join dbo.Products as pr
on pr.Id = n.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where ca.CategoryNumber = 'ODR'

Delete n
from dbo.ProductSettingVersions as n
inner join dbo.Products as pr
on pr.Id = n.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where ca.CategoryNumber = 'ODR'

*/