

select pr.ProductNumber, v.VersionNumber, v.AlternativeNo, COUNT(*)
from dbo.ProductVersions as v
inner join dbo.Products as pr
on pr.Id = v.ProductId 
where v.IsActive = 1
group by pr.ProductNumber, v.VersionNumber, v.AlternativeNo
having COUNT(*) > 1
order by pr.ProductNumber, v.VersionNumber, v.AlternativeNo
