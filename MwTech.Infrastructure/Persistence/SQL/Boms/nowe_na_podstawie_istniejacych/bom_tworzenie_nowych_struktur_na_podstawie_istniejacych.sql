/* Skrypt tworzy nowy komplet indeksów, bomów i marszrut na podstawie istniej¹cych */
/* Multiple CTE */
/* 2023.05.12 poprawka nie kopiuj¹ce siê marszruty z pust¹ kategori¹ zaszeregowania w przezbrojeniu */
use MwTech;

declare @symb1 as char(2);
set @symb1 = 'EU';
declare @symb2 as char(2);
set @symb2 = 'EG';

declare @name1 as char(150);
set @name1 = 'Eurotube';
declare @name2 as char(150);
set @name2 = 'ESNEAGRI';


with first_level as (
select spr.ProductNumber as pset
,spr.OldProductNumber as SetOldProductNumber
,CAST(REPLACE(spr.ProductNumber,@Symb1,@symb2) as varchar(50)) as new_set
,CAST(REPLACE(REPLACE(spr.Name,@Name1,@Name2),@Symb1,@Symb2) as varchar(150)) as new_set_name
,ppr.ProductNumber as ppart
,CAST(REPLACE(ppr.ProductNumber,@Symb1,@Symb2) as varchar(50)) as newpart
,CAST(REPLACE(REPLACE(ppr.Name,@Name1,@Name2),@Symb1,@Symb2) as varchar(150)) as new_part_name
,b.PartQty
,b.OrdinalNumber
,b.OnProductionOrder
,b.Excess
,su.UnitCode as sUnitCode
,su.Name as sUnitName
,pu.UnitCode as pUnitCode
,pu.Name as pUnitName
,sca.CategoryNumber as set_category
,pca.CategoryNumber as part_category
,spr.idx01 as set_idx01
,spr.Idx02 as set_idx02
,ppr.idx01 as part_idx01
,ppr.idx01 as part_idx02
,sv.VersionNumber, sv.AlternativeNo, sv.Name as VersionName
from dbo.Products as spr
inner join dbo.ProductVersions as sv
on sv.ProductId = spr.Id and sv.DefaultVersion = 1 and sv.IsActive = 1
inner join dbo.Units as su
on su.Id = spr.UnitId
inner join dbo.Boms as b
on b.SetId  = spr.Id and b.SetVersionId = sv.Id
inner join dbo.Products as ppr
on ppr.id = b.PartId
inner join dbo.Units as pu
on pu.Id = ppr.UnitId
inner join dbo.ProductCategories as sca
on sca.Id = spr.ProductCategoryId
inner join dbo.ProductCategories as pca
on pca.Id = ppr.ProductCategoryId
where 1=1 
and sca.CategoryNumber in ('ZAW-M', 'DWY', 'DOB','DAP','DST','DWU','DKJ','DET','DET-LUZ','DET-KBK')
and spr.ProductNumber in 
('DT15700750TR75EUWK'
,'DT15750V3218EUWK'
,'DT15815TR75AEUWK'
,'DT15815V3410EUWK'
,'DC15825V328EUWK'
,'DC15825V345EUWK'
,'DC159001000V328EUWK'
,'DC16700750V3218EUWK'
,'DC16750V328EUWK'
,'DC16825V345EUWK'
,'DC16900V328EUWK'
,'DC16105V328EUWK'
,'DC16105V368EUWK'
,'DC20750V328EUWK'
,'DC20750V363EUWK'
,'DC20825V3210EUWK'
,'DC20900V3211EUWK'
,'DC20900V368EUWK'
,'DC201000V3214EUWK'
,'DC201000V368EUWK'
,'DC201000V325EUWK'
,'DC201100V3214EUWK'
,'DC201100V345EUWK'
,'DC201200V3214EUWK'
,'DC201200V368EUWK'
,'DC781200TR78EUWK'
,'DC201400V368EUWK'
,'DC201400V345EUWK'
,'DC201600V368EUWK'
,'DC241200V368EUWK'
,'DC241300V3214EUWK'
,'DC241300TR177AEUWK'
,'DC241400V345EUWK'
,'DC241400V3422EUWK'
,'DR241600V3422EUWK'
,'DT10145TR13EUWK'
,'DO1313514570TR13EUWK'
,'DO12145155TR13EUWK'
,'DO12155165175TR13EUWK'
,'DO13155TR13EUWK'
,'DO1316570TR13EUWK'
,'DO13165175TR13EUWK'
,'DC14165175V327EUWK'
,'DO14175185TR13EUWK'
,'DO14195205215TR13EUWK'
,'DO15145155TR13EUWK'
,'DO15500TR13EUWK'
,'DO15175TR13EUWK'
,'DO1518570TR13EUWK'
,'DO15195205TR13EUWK'
,'DO16600650TR15EUWK'
,'DO16650TR13EUWK'
,'DO16650V3218EUWK'
,'DO16650TR75EUWK'
,'DC16825V328EUWK'
,'DO16195205TR15EUWK'
,'DO16235245255TR13EUWK'
,'DR1210080TR15EUWK'
,'DR1210080TR13EUWK'
,'DR15500TR15EUWK'
,'DR1511LTR15EUWK'
,'DR15310075TR15EUWK'
,'DR15310075TR218EUWK'
,'DR15311580TR15EUWK'
,'DR15540060TR15EUWK'
,'DR1553507040060TR15EUWK'
,'DR16450TR15EUWK'
,'DR16550600TR218AEUWK'
,'DR16750TR13EUWK'
,'DR16750TR15EUWK'
,'DR16750TR218AEUWK'
,'DR16900TR15EUWK'
,'DR16900TR218EUWK'
,'DR16105TR218EUWK'
,'DR161100TR15EUWK'
,'DR1613065TR15EUWK'
,'DR16510TR15EUWK'
,'DR16512TR15EUWK'
,'DR1715055TR15EUWK'
,'DR1715055TR218EUWK'
,'DR1719045TR218EUWK'
,'DR18500TR13EUWK'
,'DR18750TR15EUWK'
,'DR18750TR218AEUWK'
,'DR1810580TR15EUWK'
,'DR1812580TR15EUWK'
,'DR1813065TR15EUWK'
,'DR19500TR13EUWK'
,'DR19600TR15EUWK'
,'DR19518TR15EUWK'
,'DR20650TR15EUWK'
,'DR20750TR13EUWK'
,'DR20700750TR15EUWK'
,'DR20750TR218EUWK'
,'DR208095TR218AEUWK'
,'DR20112TR218AEUWK'
,'DR2010580TR15EUWK'
,'DR2014580TR15EUWK'
,'DR2016070V345EUWK'
,'DR2016070TR218AEUWK'
,'DR2016070TR15EUWK'
,'DR2020070TR15EUWK'
,'DC22516518TR218AEUWK'
,'DR248395TR218EUWK'
,'DR24112124TR218EUWK'
,'DR241400TR218AEUWK'
,'DR24136149TR218EUWK'
,'DR24149TR218AEUWK'
,'DR241558016585TR218EUWK'
,'DR2416070TR218AEUWK'
,'DR24169TR218EUWK'
,'DR26136149TR218AEUWK'
,'DR26169184TR218EUWK'
,'DR26231TR218EUWK'
,'DR288395TR218AEUWK'
,'DR288395TR218AEUWK'
,'DR28112124TR218EUWK'
,'DR28136TR218AEUWK'
,'DR28136149TR218EUWK'
,'DR28169TR218EUWK'
,'DR30169TR218EUWK'
,'DR30169184TR218EUWK'
,'DR30231TR218EUWK'
,'DR30560055TR218AEUWK'
,'DR328395TR218AEUWK'
,'DR32112124TR218AEUWK'
,'DR32245TR218EUWK'
,'DR32305TR218AEUWK'
,'DR34169184TR218EUWK'
,'DR3460065TR218AEUWK'
,'DR34231TR218AEUWK'
,'DR36400TR15EUWK'
,'DR368395TR218EUWK'
,'DR36124136TR218EUWK'
,'DR388395TR218AEUWK'
,'DR38124TR218EUWK'
,'DR38136149TR218EUWK'
,'DR38155TR218EUWK'
,'DR38169TR218AEUWK'
,'DR38184TR218EUWK'
,'DR38208TR218EUWK'
,'DR3871070TR218AEUWK'
,'DR3871070TR218EUWK'
,'DR408395TR218AEUWK'
,'DR428395TR218AEUWK'
,'DR42112TR218AEUWK'
,'DR42208TR218EUWK'
,'DR4271070TR218EUWK'
,'DR4495TR218AEUWK'
,'DR44112TR218AEUWK'
,'DR46112124TR218AEUWK'
,'DR46136149TR218AEUWK'
,'DR4895TR218AEUWK'
,'DR48112TR218AEUWK'
,'DR48136149TR218AEUWK'
,'DR241400TRJ1175CEUWK'
,'DR25155TRJ40006EUWK'
,'DR25205TRJ40006EUWK'
,'DR252351175CEUWK'
,'DR25235TRJ40006EUWK'
,'DR252551175CEUWK'
,'DR25265TRJ1175EUWK'
,'DT4300TR13EUWK'
,'DT4400TR13EUWK'
,'DT6350400TR13EUWK'
,'DT6400TR87SEUWK'
,'DT615600TR13EUWK'
,'DT8325TR13EUWK'
,'DT835087SEUWK'
,'DT8400TR87EUWK'
,'DT8400TR13EUWK'
,'DT8500TR87EUWK'
,'DT8500TR13EUWK'
,'DT8500JS2EUWK'
,'DT816650TR13EUWK'
,'DT818850950TR13EUWK'
,'DT8187V325EUWK'
,'DT8187TR13EUWK'
,'DT9600TR87EUWK'
,'DT9600TR13EUWK'
,'DT9600TR15EUWK'
,'DT9600JS2EUWK'
,'DT10350400TR13EUWK'
,'DT10400TR13EUWK'
,'DT10500TR15EUWK'
,'DT10650TR87EUWK'
,'DT10650JS2EUWK'
,'DT10239JS2EUWK'
,'DT10239V327EUWK'
,'DT12400TR15EUWK'
,'DO1260600TR218EUWK'
,'DT126580TR218AEUWK'
,'DT12700V3218EUWK'
,'DT12700JS2EUWK'
,'DT12700TR13EUWK'
,'DT1223850TR13EUWK'
,'DT1223850TR15EUWK'
,'DT122710V323EUWK'
,'DT122710JS2EUWK'
,'DO15215V3618EUWK'
,'DT1525016V328EUWK'
,'DT15300V328EUWK'
,'DT15750V328EUWK'
,'DT15815JS2EUWK'
,'DT1527850TR15EUWK'
,'DR22550045TR218EUWK'
,'DR22550060TR218AEUWK'
,'DR22555060TR218AEUWK'
,'DR22560050TR218AEUWK'
,'DR22570045TR218AEUWK'
,'DM21300GP4EUWK')),

second_level as 
(
select spr.ProductNumber as pset
,spr.OldProductNumber as SetOldProductNumber
,CAST(REPLACE(spr.ProductNumber,@Symb1,@Symb2) as varchar(50)) as new_set
,CAST(REPLACE(REPLACE(spr.Name,@Name1,@Name2),@Symb1,@Symb2) as varchar(150)) as new_set_name
,ppr.ProductNumber as ppart
,CAST(REPLACE(ppr.ProductNumber,@Symb1,@Symb2) as varchar(50)) as newpart
,CAST(REPLACE(REPLACE(ppr.Name,@Name1,@Name2),@Symb1,@Symb2) as varchar(150)) as new_part_name
,b.PartQty
,b.OrdinalNumber
,b.OnProductionOrder
,b.Excess
,su.UnitCode as sUnitCode
,su.Name as sUnitName
,pu.UnitCode as pUnitCode
,pu.Name as pUnitName
,sca.CategoryNumber as set_category
,pca.CategoryNumber as part_category
,spr.idx01 as set_idx01
,spr.Idx02 as set_idx02
,ppr.idx01 as part_idx01
,ppr.idx01 as part_idx02
,sv.VersionNumber, sv.AlternativeNo, sv.Name as VersionName
from dbo.Products as spr
inner join dbo.ProductVersions as sv
on sv.ProductId = spr.Id and sv.DefaultVersion = 1 and sv.IsActive = 1
inner join dbo.Units as su
on su.Id = spr.UnitId
inner join dbo.Boms as b
on b.SetId  = spr.Id and b.SetVersionId = sv.Id
inner join dbo.Products as ppr
on ppr.id = b.PartId
inner join dbo.Units as pu
on pu.Id = ppr.UnitId
inner join dbo.ProductCategories as sca
on sca.Id = spr.ProductCategoryId
inner join dbo.ProductCategories as pca
on pca.Id = ppr.ProductCategoryId
where 1=1 
and sca.CategoryNumber in ('DWY', 'DOB','DAP','DST','DWU','DKJ','DET','DET-LUZ','DET-KBK')
and spr.ProductNumber in (select ppart from first_level )
),

third_level as 
(
select spr.ProductNumber as pset
,spr.OldProductNumber as SetOldProductNumber
,CAST(REPLACE(spr.ProductNumber,@Symb1,@Symb2) as varchar(50)) as new_set
,CAST(REPLACE(REPLACE(spr.Name,@Name1,@Name2),@Symb1,@Symb2) as varchar(150)) as new_set_name
,ppr.ProductNumber as ppart
,CAST(REPLACE(ppr.ProductNumber,@Symb1,@Symb2) as varchar(50)) as newpart
,CAST(REPLACE(REPLACE(ppr.Name,@Name1,@Name2),@Symb1,@Symb2) as varchar(150)) as new_part_name
,b.PartQty
,b.OrdinalNumber
,b.OnProductionOrder
,b.Excess
,su.UnitCode as sUnitCode
,su.Name as sUnitName
,pu.UnitCode as pUnitCode
,pu.Name as pUnitName
,sca.CategoryNumber as set_category
,pca.CategoryNumber as part_category
,spr.idx01 as set_idx01
,spr.Idx02 as set_idx02
,ppr.idx01 as part_idx01
,ppr.idx01 as part_idx02
,sv.VersionNumber, sv.AlternativeNo, sv.Name as VersionName
from dbo.Products as spr
inner join dbo.ProductVersions as sv
on sv.ProductId = spr.Id and sv.DefaultVersion = 1  and sv.IsActive = 1
inner join dbo.Units as su
on su.Id = spr.UnitId
inner join dbo.Boms as b
on b.SetId  = spr.Id and b.SetVersionId = sv.Id
inner join dbo.Products as ppr
on ppr.id = b.PartId
inner join dbo.Units as pu
on pu.Id = ppr.UnitId
inner join dbo.ProductCategories as sca
on sca.Id = spr.ProductCategoryId
inner join dbo.ProductCategories as pca
on pca.Id = ppr.ProductCategoryId
where 1=1 
and sca.CategoryNumber in ('DWY', 'DOB','DAP','DST','DWU','DKJ','DET','DET-LUZ','DET-KBK')
and spr.ProductNumber in (select ppart from second_level )
),

fourth_level as 
(
select spr.ProductNumber as pset
,spr.OldProductNumber as SetOldProductNumber
,CAST(REPLACE(spr.ProductNumber,@Symb1,@Symb2) as varchar(50)) as new_set
,CAST(REPLACE(REPLACE(spr.Name,@Name1,@Name2),@Symb1,@Symb2) as varchar(150)) as new_set_name
,ppr.ProductNumber as ppart
,CAST(REPLACE(ppr.ProductNumber,@Symb1,@Symb2) as varchar(50)) as newpart
,CAST(REPLACE(REPLACE(ppr.Name,@Name1,@Name2),@Symb1,@Symb2) as varchar(150)) as new_part_name
,b.PartQty
,b.OrdinalNumber
,b.OnProductionOrder
,b.Excess
,su.UnitCode as sUnitCode
,su.Name as sUnitName
,pu.UnitCode as pUnitCode
,pu.Name as pUnitName
,sca.CategoryNumber as set_category
,pca.CategoryNumber as part_category
,spr.idx01 as set_idx01
,spr.Idx02 as set_idx02
,ppr.idx01 as part_idx01
,ppr.idx01 as part_idx02
,sv.VersionNumber, sv.AlternativeNo, sv.Name as VersionName
from dbo.Products as spr
inner join dbo.ProductVersions as sv
on sv.ProductId = spr.Id and sv.DefaultVersion = 1  and sv.IsActive = 1
inner join dbo.Units as su
on su.Id = spr.UnitId
inner join dbo.Boms as b
on b.SetId  = spr.Id and b.SetVersionId = sv.Id
inner join dbo.Products as ppr
on ppr.id = b.PartId
inner join dbo.Units as pu
on pu.Id = ppr.UnitId
inner join dbo.ProductCategories as sca
on sca.Id = spr.ProductCategoryId
inner join dbo.ProductCategories as pca
on pca.Id = ppr.ProductCategoryId
where 1=1 
and sca.CategoryNumber in ('DWY', 'DOB','DAP','DST','DWU','DKJ','DET','DET-LUZ','DET-KBK')
and spr.ProductNumber in (select ppart from third_level )
),

fifth_level as 
(
select spr.ProductNumber as pset
,spr.OldProductNumber as SetOldProductNumber
,CAST(REPLACE(spr.ProductNumber,@Symb1,@Symb2) as varchar(50)) as new_set
,CAST(REPLACE(REPLACE(spr.Name,@Name1,@Name2),@Symb1,@Symb2) as varchar(150)) as new_set_name
,ppr.ProductNumber as ppart
,CAST(REPLACE(ppr.ProductNumber,@Symb1,@Symb2) as varchar(50)) as newpart
,CAST(REPLACE(REPLACE(ppr.Name,@Name1,@Name2),@Symb1,@Symb2) as varchar(150)) as new_part_name
,b.PartQty
,b.OrdinalNumber
,b.OnProductionOrder
,b.Excess
,su.UnitCode as sUnitCode
,su.Name as sUnitName
,pu.UnitCode as pUnitCode
,pu.Name as pUnitName
,sca.CategoryNumber as set_category
,pca.CategoryNumber as part_category
,spr.idx01 as set_idx01
,spr.Idx02 as set_idx02
,ppr.idx01 as part_idx01
,ppr.idx01 as part_idx02
,sv.VersionNumber, sv.AlternativeNo, sv.Name as VersionName
from dbo.Products as spr
inner join dbo.ProductVersions as sv
on sv.ProductId = spr.Id and sv.DefaultVersion = 1  and sv.IsActive = 1
inner join dbo.Units as su
on su.Id = spr.UnitId
inner join dbo.Boms as b
on b.SetId  = spr.Id and b.SetVersionId = sv.Id
inner join dbo.Products as ppr
on ppr.id = b.PartId
inner join dbo.Units as pu
on pu.Id = ppr.UnitId
inner join dbo.ProductCategories as sca
on sca.Id = spr.ProductCategoryId
inner join dbo.ProductCategories as pca
on pca.Id = ppr.ProductCategoryId
where 1=1 
and sca.CategoryNumber in ('DWY', 'DOB','DAP','DST','DWU','DKJ','DET','DET-LUZ','DET-KBK')
and spr.ProductNumber in (select ppart from fourth_level )
),

sixth_level as 
(
select spr.ProductNumber as pset
,spr.OldProductNumber as SetOldProductNumber
,CAST(REPLACE(spr.ProductNumber,@Symb1,@Symb2) as varchar(50)) as new_set
,CAST(REPLACE(REPLACE(spr.Name,@Name1,@Name2),@Symb1,@Symb2) as varchar(150)) as new_set_name
,ppr.ProductNumber as ppart
,CAST(REPLACE(ppr.ProductNumber,@Symb1,@Symb2) as varchar(50)) as newpart
,CAST(REPLACE(REPLACE(ppr.Name,@Name1,@Name2),@Symb1,@Symb2) as varchar(150)) as new_part_name
,b.PartQty
,b.OrdinalNumber
,b.OnProductionOrder
,b.Excess
,su.UnitCode as sUnitCode
,su.Name as sUnitName
,pu.UnitCode as pUnitCode
,pu.Name as pUnitName
,sca.CategoryNumber as set_category
,pca.CategoryNumber as part_category
,spr.idx01 as set_idx01
,spr.Idx02 as set_idx02
,ppr.idx01 as part_idx01
,ppr.idx01 as part_idx02
,sv.VersionNumber, sv.AlternativeNo, sv.Name as VersionName
from dbo.Products as spr
inner join dbo.ProductVersions as sv
on sv.ProductId = spr.Id and sv.DefaultVersion = 1  and sv.IsActive = 1
inner join dbo.Units as su
on su.Id = spr.UnitId
inner join dbo.Boms as b
on b.SetId  = spr.Id and b.SetVersionId = sv.Id
inner join dbo.Products as ppr
on ppr.id = b.PartId
inner join dbo.Units as pu
on pu.Id = ppr.UnitId
inner join dbo.ProductCategories as sca
on sca.Id = spr.ProductCategoryId
inner join dbo.ProductCategories as pca
on pca.Id = ppr.ProductCategoryId
where 1=1 
and sca.CategoryNumber in ('DWY', 'DOB','DAP','DST','DWU','DKJ','DET','DET-LUZ','DET-KBK')
and spr.ProductNumber in (select ppart from fifth_level )
),

seventh_level as 
(
select spr.ProductNumber as pset
,spr.OldProductNumber as SetOldProductNumber
,CAST(REPLACE(spr.ProductNumber,@Symb1,@Symb2) as varchar(50)) as new_set
,CAST(REPLACE(REPLACE(spr.Name,@Name1,@Name2),@Symb1,@Symb2) as varchar(150)) as new_set_name
,ppr.ProductNumber as ppart
,CAST(REPLACE(ppr.ProductNumber,@Symb1,@Symb2) as varchar(50)) as newpart
,CAST(REPLACE(REPLACE(ppr.Name,@Name1,@Name2),@Symb1,@Symb2) as varchar(150)) as new_part_name
,b.PartQty
,b.OrdinalNumber
,b.OnProductionOrder
,b.Excess
,su.UnitCode as sUnitCode
,su.Name as sUnitName
,pu.UnitCode as pUnitCode
,pu.Name as pUnitName
,sca.CategoryNumber as set_category
,pca.CategoryNumber as part_category
,spr.idx01 as set_idx01
,spr.Idx02 as set_idx02
,ppr.idx01 as part_idx01
,ppr.idx01 as part_idx02
,sv.VersionNumber, sv.AlternativeNo, sv.Name as VersionName
from dbo.Products as spr
inner join dbo.ProductVersions as sv
on sv.ProductId = spr.Id and sv.DefaultVersion = 1  and sv.IsActive = 1
inner join dbo.Units as su
on su.Id = spr.UnitId
inner join dbo.Boms as b
on b.SetId  = spr.Id and b.SetVersionId = sv.Id
inner join dbo.Products as ppr
on ppr.id = b.PartId
inner join dbo.Units as pu
on pu.Id = ppr.UnitId
inner join dbo.ProductCategories as sca
on sca.Id = spr.ProductCategoryId
inner join dbo.ProductCategories as pca
on pca.Id = ppr.ProductCategoryId
where 1=1 
and sca.CategoryNumber in ('DWY', 'DOB','DAP','DST','DWU','DKJ','DET','DET-LUZ','DET-KBK')
and spr.ProductNumber in (select ppart from sixth_level )
),

AllBomsToAdd as (
Select * 
from (
select *
from first_level
union all
select *
from second_level
union all
select *
from third_level
union all
select *
from fourth_level
union all
select *
from fifth_level
union all
select *
from sixth_level
union all
select *
from seventh_level
) as x
),


ProductsToAdd as (
select x.new_set as ProductNumber
, x.new_set_name as ProductName
, x.sUnitCode as Unit
, x.set_category as Categorty
, x.SetOldProductNumber as SetOldProductNumber
, x.set_idx01 as matka
, x.set_idx02 as waz
, x.pset as ProductNumberToCopy
from AllBomsToAdd as x
group by x.new_set, x.new_set_name, x.sUnitCode, x.set_category, set_idx01, set_idx02, x.pset, x.SetOldProductNumber 
)

-- Koniec definicji DTO Expression
/*
select ProductNumber
, ProductName
, Unit
, Categorty
, SetOldProductNumber
, matka
, waz
from ProductsToAdd
order by Categorty, ProductNumber
*/

/*
select 'KT1' as kt
, x.new_set as SetProductNumber
,'' as Pusty
,x.VersionName
,x.AlternativeNo
,x.VersionNumber
,1 as IsActive
,x.OrdinalNumber
,x.newpart as PartPRoductNumber
,x.PartQty
,x.Excess
,x.OnProductionOrder
,'A' as Method
,0 as layer
from AllBomsToAdd as x
order by x.pset, x.OrdinalNumber

*/



select 'KT1' as kt
, a.ProductNumber as NewProductNumber
, '' as empty1
,rv.Name as Description
,rv.AlternativeNo
,rv.VersionNumber
,rv.IsActive
,r.OrdinalNumber
,op.OperationNumber
,wc.ResourceNumber as WorkCenter
,replace(r.ChangeOverMachineConsumption, '.', ',') as ChangeOverMachineConsumption
,replace(r.ChangeOverLabourConsumption, '.', ',') as ChangeOverLabourConsumption
,ch.ResourceNumber as ChangeOverResource
,replace(r.ChangeOverNumberOfEmployee, '.', ',') as ChangeOverNumberOfEmployee
,replace(r.OperationMachineConsumption, '.', ',') as OperationMachineConsumption
,replace(r.OperationLabourConsumption, '.', ',') as OperationLabourConsumption
,u.UnitCode -- Jedn	ostka
,res.ResourceNumber as Kategoria
,replace(r.ResourceQty, '.', ',') as ResourceQty
,replace(r.MoveTime, '.', ',') as MoveTime
,replace(r.Overlap, '.', ',') as Overlap
,rv.IfsDefaultVersion
from dbo.RouteVersions as rv
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = rv.Id 
inner join dbo.Products as pr
on pr.Id = rv.ProductId
inner join dbo.Resources as wc
on wc.Id = r.WorkCenterId
inner join dbo.Resources as res
on res.Id = r.ResourceId
left join dbo.Resources as ch
on ch.Id = r.ChangeOverResourceId
left join dbo.Operations as op
on op.Id = r.OperationId
left join dbo.Units as u
on u.Id = op.UnitId
inner join ProductsToAdd as a
on a.ProductNumberToCopy = pr.ProductNumber
--
Where rv.IsActive = 1																																																						
Order by a.ProductNumber,rv.VersionNumber,rv.AlternativeNo,r.OrdinalNumber 



