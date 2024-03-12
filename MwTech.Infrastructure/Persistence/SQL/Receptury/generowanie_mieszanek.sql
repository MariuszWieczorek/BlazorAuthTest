use MwTech

declare @ProductNumber as varchar(50);
set @ProductNumber = 'MIE.BKR65-1';
declare @ver as int;
set @ver = 1;
declare @alt as int;
set @alt = 1;
declare @ProductId as int;
set @ProductId = (select id from dbo.Products where ProductNumber = @ProductNumber);
declare @ProductVersionId as int;
set @ProductVersionId = (select id from dbo.ProductVersions  as v where v.ProductId = @ProductId  and v.VersionNumber = @ver and v.AlternativeNo = @alt);

/*
select *
from dbo.mwtech_bom_cte(@ProductId,@ProductVersionId)  as cte
*/


select pr.ProductNumber, v.VersionNumber, v.AlternativeNo, v.Name as AlternativeName
from dbo.Products as pr
inner join dbo.ProductVersions as v
on v.ProductId = pr.Id
--
where 1 = 1
and pr.ProductNumber = @ProductNumber
and v.VersionNumber = @ver and v.AlternativeNo = @alt
																																																																																																																																																																																																																																

select pr.ProductNumber, v.VersionNumber, v.AlternativeNo, v.Name as AlternativeName
,b.PartId,part.ProductNumber,b.PartQty
from dbo.Products as pr
inner join dbo.ProductVersions as v
on v.ProductId = pr.Id
--
inner join dbo.Boms as b
on b.SetVersionId = v.Id
inner join dbo.Products as part
on part.Id = b.PartId
--
where 1 = 1
and pr.ProductNumber = @ProductNumber
and v.VersionNumber = @ver and v.AlternativeNo = @alt




