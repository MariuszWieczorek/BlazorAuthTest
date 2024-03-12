use mwbase;

declare @zestaw INT = 554;

with CTE AS
(
SELECT 
	 0 AS Level
	,CAST(ROW_NUMBER() over(order by bom.zestawPozId) as numeric(10) )as rownr

	-- zestaw
	,bom.zestawMatId as zestaw_id
	,bom.zestawIlosc
	,bom.zestawJm

	-- składnik
	,bom.skladnMatId as skladn_id
	,bom.skladnIlosc
	,CAST(bom.skladnIlosc * bom.zestawIlosc as numeric(10,2)) as zasob_ilosc
	,bom.skladnJm

 FROM prdkabat.bomy as bom
 		WHERE bom.zestawMatId = @zestaw 
		and bom.skladnMatID is not null

UNION ALL 

SELECT 
     c.Level + 1  as level
	,CAST(ROW_NUMBER() over(order by bom.zestawPozId) as numeric(10) ) as rownr
	
	-- zestaw
	,bom.zestawMatId as zestaw_id
	,bom.zestawIlosc
	,bom.zestawJm

	-- składnik
	,bom.skladnMatId as skladn_id
	,bom.skladnIlosc
	,CAST((bom.skladnIlosc / bom.zestawIlosc) * c.zasob_ilosc as numeric(10,2)) as zasob_ilosc
	,bom.skladnJm
  
	FROM prdkabat.bomy as bom
	inner join CTE as c	
	on bom.zestawMatId = c.skladn_id
	where bom.skladnMatID is not null

  )


 select c.*
  ,(select count(*) as ile from CTE as x where x.zestaw_id = c.skladn_id) as how_many_kids
  from CTE as c 
 
  OPTION (MAXRECURSION 100); -- bez ograniczenia poziomu rekursji

