DECLARE @SourceId as INT = (select id from dbo.Products where ProductNumber = 'MIE.IBZ-2-F-PASKI');
DECLARE @TargetId as INT = (select id from dbo.Products where ProductNumber = 'MIE.IBZ-2-F-140X10-PPL');

select prp.ProductNumber as SetProductNumber, prs.ProductNumber as PartProductNumber,
@SourceId as SourceId,
@TargetId as TargetId
from dbo.Products as prp
inner join dbo.ProductCategories as ca
on ca.id = prp.ProductCategoryId
inner join dbo.Boms as b
on b.PartId = prp.Id
inner join dbo.Products as prs
on prs.id = b.SetId
where 1 = 1
and prp.Id = @SourceId

/*
update b
SET b.PartId = @TargetId
from dbo.Products as prp
inner join dbo.ProductCategories as ca
on ca.id = prp.ProductCategoryId
inner join dbo.Boms as b
on b.PartId = prp.Id
inner join dbo.Products as prs
on prs.id = b.SetId
where 1 = 1
and prp.Id = @SourceId
*/
