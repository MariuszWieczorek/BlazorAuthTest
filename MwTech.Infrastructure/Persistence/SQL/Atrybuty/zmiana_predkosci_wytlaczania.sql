declare @id as int = (select Id from dbo.Products where ProductNumber = 'MIE.SL4/922-2-F')

select pr.ProductNumber, pr.Idx02 , s.PropertyNumber, p.MinValue, p.Value, p.MaxValue
-- update p
-- set Value = Value - 2, MinValue = MinValue - 2, MaxValue = MaxValue - 2
from dbo.Boms as b
inner join dbo.Products as pr
on pr.Id = b.SetId
inner join dbo.Products as prr
on prr.ProductNumber = pr.Idx02
inner join dbo.ProductPropertyVersions as v
on v.ProductId = prr.Id
inner join dbo.ProductProperties as p
on p.ProductPropertiesVersionId = v.Id
inner join dbo.Properties as s
on s.Id = p.PropertyId
where b.PartId = @id
and s.PropertyNumber = 'dwy_predkosc_wytlaczania'