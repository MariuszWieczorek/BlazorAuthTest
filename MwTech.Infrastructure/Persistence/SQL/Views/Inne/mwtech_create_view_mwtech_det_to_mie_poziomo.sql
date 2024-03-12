/* Export operacji marszrut do IFS */
CREATE OR ALTER VIEW mwtech_det_to_mie_poziomo
AS
(
select 
ca.CategoryNumber as c0,
pr.ProductNumber as p0,
v.VersionNumber as v0,
--
ca1.CategoryNumber as c1,
pr1.ProductNumber as p1 ,
v1.VersionNumber as v1,
--
ca2.CategoryNumber as c2,
pr2.ProductNumber as p2 ,
v2.VersionNumber as v2,
--
ca3.CategoryNumber as c3,
pr3.ProductNumber as p3 ,
v3.VersionNumber as v3,
--
ca4.CategoryNumber as c4,
pr4.ProductNumber as p4 ,
v4.VersionNumber as v4,
--
ca5.CategoryNumber as c5,
pr5.ProductNumber as p5 ,
v5.VersionNumber as v5,
--
ca6.CategoryNumber as c6,
pr6.ProductNumber as p6 ,
v6.VersionNumber as v6,
--
ca7.CategoryNumber as c7,
pr7.ProductNumber as p7 ,
v7.VersionNumber as v7
--
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.ProductVersions as v
on v.ProductId = pr.id
inner join dbo.Boms as b
on b.SetId = pr.Id and b.SetVersionId = v.id
--
inner join dbo.Products as pr1
on pr1.Id = b.PartId
inner join dbo.ProductCategories as ca1
on ca1.Id = pr1.ProductCategoryId
inner join dbo.ProductVersions as v1
on v1.ProductId = pr1.id
inner join dbo.Boms as b1
on b1.SetId = pr1.Id and b1.SetVersionId = v1.id
--
inner join dbo.Products as pr2
on pr2.Id = b1.PartId
inner join dbo.ProductCategories as ca2
on ca2.Id = pr2.ProductCategoryId
inner join dbo.ProductVersions as v2
on v2.ProductId = pr2.id
inner join dbo.Boms as b2
on b2.SetId = pr2.Id and b2.SetVersionId = v2.id
--
inner join dbo.Products as pr3
on pr3.Id = b2.PartId
inner join dbo.ProductCategories as ca3
on ca3.Id = pr3.ProductCategoryId
inner join dbo.ProductVersions as v3
on v3.ProductId = pr3.id
inner join dbo.Boms as b3
on b3.SetId = pr3.Id and b3.SetVersionId = v3.id
--
inner join dbo.Products as pr4
on pr4.Id = b3.PartId
inner join dbo.ProductCategories as ca4
on ca4.Id = pr4.ProductCategoryId
inner join dbo.ProductVersions as v4
on v4.ProductId = pr4.id
inner join dbo.Boms as b4
on b4.SetId = pr4.Id and b4.SetVersionId = v4.id
--
inner join dbo.Products as pr5
on pr5.Id = b4.PartId
inner join dbo.ProductCategories as ca5
on ca5.Id = pr5.ProductCategoryId
inner join dbo.ProductVersions as v5
on v5.ProductId = pr5.id
inner join dbo.Boms as b5
on b5.SetId = pr5.Id and b5.SetVersionId = v5.id
--
inner join dbo.Products as pr6
on pr6.Id = b5.PartId
inner join dbo.ProductCategories as ca6
on ca6.Id = pr6.ProductCategoryId
inner join dbo.ProductVersions as v6
on v6.ProductId = pr6.id
inner join dbo.Boms as b6
on b6.SetId = pr6.Id and b6.SetVersionId = v6.id
--
inner join dbo.Products as pr7
on pr7.Id = b6.PartId
inner join dbo.ProductCategories as ca7
on ca7.Id = pr7.ProductCategoryId
inner join dbo.ProductVersions as v7
on v7.ProductId = pr7.id
inner join dbo.Boms as b7
on b7.SetId = pr7.Id and b7.SetVersionId = v7.id
--
where ca.CategoryNumber = 'DET' 
and ca1.CategoryNumber = 'DKJ'
and ca2.CategoryNumber = 'DWU'
and ca3.CategoryNumber = 'DST'
and ca4.CategoryNumber = 'DAP'
and ca5.CategoryNumber = 'DOB'
and ca6.CategoryNumber = 'DWY'
and ca7.CategoryNumber = 'MIE'
)

-- select * from mwtech_route_ifs order by SetCategory, nr_pozycji, numer_operacji
-- select * from mwtech_bom_ifs order by SetCategory, nr_pozycji_nadrzednej, numer_pozycji_w_linii


