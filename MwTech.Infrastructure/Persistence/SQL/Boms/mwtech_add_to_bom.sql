
select
 b.SetId
,b.PartId
,-0.05 as PartQty
,2 as OrdinalNumber
,b.SetVersionId
,0 as Excess
,0 as OnProductionOrder
,0 as Layer
,'zmiana 2022-12-30-A' as Description
,1 as DoNotExportToIfs
,1 as DoNotIncludeInTkw
,0 as DoNotIncludeInWeight
,prs.ProductNumber
from dbo.Boms as b
inner join dbo.Products as prs
on prs.Id = b.SetId
inner join dbo.ProductCategories as cas
on cas.Id = prs.ProductCategoryId
inner join dbo.Products as prp
on prp.Id = b.PartId
inner join dbo.ProductCategories as cap
on cap.Id = prp.ProductCategoryId
where cas.CategoryNumber in ('WF-AKC','WF-AKC-POZ','WF-AKC-B') and cap.CategoryNumber IN ('MIR','MIP','MIF')

/*
insert into dbo.Boms
(
 SetId
,PartId
,PartQty
,OrdinalNumber
,SetVersionId
,Excess
,OnProductionOrder
,Layer
,Description
,DoNotExportToIfs
,DoNotIncludeInTkw
,DoNotIncludeInWeight
)
(
select
 b.SetId
,b.PartId
,-0.05 as PartQty
,2 as OrdinalNumber
,b.SetVersionId
,0 as Excess
,0 as OnProductionOrder
,0 as Layer
,'zmiana 2022-12-30-A' as Description
,1 as DoNotExportToIfs
,1 as DoNotIncludeInTkw
,0 as DoNotIncludeInWeight
--,prs.ProductNumber
from dbo.Boms as b
inner join dbo.Products as prs
on prs.Id = b.SetId
inner join dbo.ProductCategories as cas
on cas.Id = prs.ProductCategoryId
inner join dbo.Products as prp
on prp.Id = b.PartId
inner join dbo.ProductCategories as cap
on cap.Id = prp.ProductCategoryId
where cas.CategoryNumber in ('WF-AKC','WF-AKC-POZ','WF-AKC-B') and cap.CategoryNumber IN ('MIR','MIP','MIF')
)
*/



