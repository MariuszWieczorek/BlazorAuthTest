--  0.0009625  475
--  0.0012975  219
--  0.00166625 508
--  0.0022025  347
--  0.0033325  16
--  0.004405   239
--  0.009755   93

-- 0.0044929 8177
-- 0.006253 5867
-- 0,014011 3279




UPDATE r
 SET
 ResourceQty = 3,
 OperationLabourConsumption  = 0.014011,
 OperationMachineConsumption = 0.014011
 from dbo.RouteVersions as v 
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.Operations as o
on o.Id = r.OperationId
where  1 = 1
and o.OperationNumber = 'PD.080_PAKOWANIE'
and pr.ProductNumber IN
(
 'IndexA'
,'IndexB'
)