use mwbase;

declare @zestaw INT = 554;

with CTE AS
(
SELECT 
	 0 AS Level
	,CAST(ROW_NUMBER() over(order by bom.zestawPozId) as numeric(10) )as rownr

	-- zestaw
	,bom.zestawMatId as zestaw_id
	,mz.indeks as zestaw_indeks
	,zg3.nazwa as zestaw_grupa
	,bom.zestawIlosc
	,bom.zestawJm

	-- składnik
	,bom.skladnMatId as skladn_id
	,cast( coalesce(ms.indeks,'')  as varchar(50)) as skladn_indeks
	,sg3.nazwa as skladn_grupa
	,bom.skladnIlosc
	,CAST(bom.skladnIlosc * bom.zestawIlosc as numeric(10,2)) as zasob_ilosc
	,bom.skladnJm

 FROM prdkabat.bomy as bom
		inner join gm.materialy_grupa3 as sg3
		on sg3.pozid = bom.skladnGrupa3
		  
		inner join gm.materialy_grupa3 as zg3
		on zg3.pozid = bom.zestawGrupa3
		  
		inner join gm.materialy as mz
		on mz.matid = bom.zestawMatId

		inner join gm.materialy as ms
		on ms.matid = bom.skladnMatId

 		WHERE bom.skladnMatID is not null
		and bom.zestawMatId = @zestaw 

UNION ALL 

SELECT 
     c.Level + 1  as level
	,CAST(ROW_NUMBER() over(order by bom.zestawPozId) as numeric(10) ) as rownr
	
	-- zestaw
	,bom.zestawMatId as zestaw_id
	,mz.indeks as zestaw_indeks
	,zg3.nazwa as zestaw_grupa
	,bom.zestawIlosc
	,bom.zestawJm

	-- składnik
	,bom.skladnMatId as skladn_id
	,cast( coalesce(ms.indeks,'')  as varchar(50)) as skladn_indeks 
	,sg3.nazwa as skladn_grupa
	,bom.skladnIlosc
	,CAST((bom.skladnIlosc / bom.zestawIlosc) * c.zasob_ilosc as numeric(10,2)) as zasob_ilosc
	,bom.skladnJm
  
	FROM prdkabat.bomy as bom
	inner join gm.materialy_grupa3 as sg3
	on sg3.pozid = bom.skladnGrupa3
	  
	inner join gm.materialy_grupa3 as zg3
	on zg3.pozid = bom.zestawGrupa3
			  
	inner join gm.materialy as mz
	on mz.matid = bom.zestawMatId

	inner join gm.materialy as ms
	on ms.matid = bom.skladnMatId

	inner join CTE as c	
	on bom.zestawMatId = c.skladn_id
 
	where bom.skladnMatID is not null
  )


 select c.*
  ,(select count(*) as ile from CTE as x where x.zestaw_id = c.skladn_id) as how_many_kids
  from CTE as c 
 
  OPTION (MAXRECURSION 100); -- bez ograniczenia poziomu rekursji

