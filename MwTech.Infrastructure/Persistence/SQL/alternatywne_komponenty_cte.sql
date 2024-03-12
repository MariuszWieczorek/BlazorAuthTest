/* zweryfikowany 2023.09.21 */
use MwTech;

-- pa   - part (komponent)
-- paca - part kategoria
-- se	- set
-- seca - set kategoria

WITH my_cte AS (
Select paca.CategoryNumber as paca,
pa.ProductNumber as pa_ProductNumber,
pa.Idx01 as pa_matka,
seca.CategoryNumber as seca,
se.ProductNumber as se_ProductNumber,
se.Idx01 as se_matka
from dbo.Boms as b
inner join dbo.ProductVersions as v
on v.Id = b.SetVersionId and v.ProductId = b.SetId and v.DefaultVersion = 1 and v.IsActive = 1
-- biorê tylko sk³adniki bom z aktywnej i domyœlnej wersji
inner join dbo.Products as pa
on pa.Id = b.PartId and pa.IsActive = 1
-- komponent musi byæ aktywnym indeksem
inner join dbo.ProductCategories as paca
on paca.Id = pa.ProductCategoryId
inner join dbo.Products as se
on se.Id = b.SetId and se.IsActive = 1
-- zestaw musi byæ aktywnym indeksem
inner join dbo.ProductCategories as seca
on seca.Id = se.ProductCategoryId
--inner join dbo.ifs_inventory_part_in_stock as st
--on trim(st.ProductNo) = trim(se.ProductNumber)
where 1 = 1
and paca.CategoryNumber in ('DKJ','DWU') -- sk³adnik
and seca.CategoryNumber not in ('DKJ')   -- komponent		
)




select x.pa_ProductNumber,x.pa_matka,y.se_ProductNumber
FROM my_cte as x
inner join my_cte as y
on y.pa_matka = x.pa_matka
where 1 = 1
-- and y.pa_matka in ('82520V')
group by x.pa_ProductNumber,x.pa_matka,y.se_ProductNumber
order by x.pa_ProductNumber,x.pa_matka,y.se_ProductNumber
OFFSET 0 ROWS
FETCH NEXT 1000000 ROWS ONLY


