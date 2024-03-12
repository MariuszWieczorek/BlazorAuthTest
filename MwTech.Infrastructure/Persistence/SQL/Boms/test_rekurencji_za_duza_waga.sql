select dbo.getProductWeight(6)
select * from dbo.mwtech_bom_cte(6,null) where HowManyParts = 0 and PartOnProductionOrder = 1 order by FinalPartProductQty desc
select sum(FinalPartProductQty) from dbo.mwtech_bom_cte(6,null) where HowManyParts = 0 and PartOnProductionOrder = 1