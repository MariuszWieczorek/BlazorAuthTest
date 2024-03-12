use MwTech;

-- wszystkim wariantom nr 1  (*) ustawiam gniazdo na PL03
-- wszystkim wariantom nr 2 ustawiam gniazdo na PL01
-- deaktywuje wszystkie warianty nr 3 - niezale¿nie od tego jakie maj¹ gniazdo


select pr.ProductNumber, v.VersionNumber, v.AlternativeNo,  wc.ResourceNumber, v.Name, v.DefaultVersion, v.IsActive 
--update v
-- set WorkCenterId = (SELECT Id from dbo.Resources where ResourceNumber = 'PL01')
--set Name = 'PL02'
from dbo.Products as pr
inner join dbo. ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.RouteVersions as v
on v.ProductId = pr.Id
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.Id
inner join dbo.Resources as wc
on wc.Id = r.WorkCenterId
where ca.CategoryNumber = 'OKC'
and v.IsActive = 0
and v.AlternativeNo = 3

/*
select pr.ProductNumber, COUNT(*) as ile
from dbo.Products as pr
inner join dbo. ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.RouteVersions as v
on v.ProductId = pr.Id
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.Id
inner join dbo.Resources as wc
on wc.Id = r.WorkCenterId
where ca.CategoryNumber = 'OKC'
and v.IsActive = 1
group by pr.ProductNumber

*/