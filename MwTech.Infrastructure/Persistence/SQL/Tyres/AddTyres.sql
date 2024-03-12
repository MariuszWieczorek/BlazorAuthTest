use MwTech;

insert into dbo.Tyres
(
tyreNumber
,name
,description
,createdbyuserid
,createddate
)
(
select pr.ProductNumber, pr.Name, pr.Description, pr.CreatedByUserId, pr.CreatedDate
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where ca.CategoryNumber = 'OWU'
)