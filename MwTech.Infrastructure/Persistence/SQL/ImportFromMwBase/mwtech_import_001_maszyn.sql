/****** Script for SelectTopNRows command from SSMS  ******/

/*
delete from [MwTech].[dbo].[Machines]
DBCC CHECKIDENT('[MwTech].[dbo].[Machines]', RESEED, 1)
*/


  insert into [MwTech].[dbo].[Machines]
  (
  MachineNumber,
  ReferenceNumber,
  Name,
  MachineCategoryId
  )
  (
 -- prasy
select 
m.symbol2,
m.symbol,
m.nazwa,
1 as MachineCategoryId
from mwbase.prdkabat.maszyny as m
where typMaszyny = 1
and not exists(select * from [MwTech].[dbo].[Machines] as x where MachineNumber = m.symbol2)
-- konfekcyjne
union all
select 
m.symbol2,
m.symbol,
m.nazwa,
4 as MachineCategoryId
from mwbase.prdkabat.maszyny as m
where typMaszyny = 2
and not exists(select * from [MwTech].[dbo].[Machines] as x where MachineNumber = m.symbol2)

-- kalander
union all
select 
m.symbol2,
m.symbol,
m.nazwa,
3 as MachineCategoryId
from mwbase.prdkabat.maszyny as m
where typMaszyny = 5
and not exists(select * from [MwTech].[dbo].[Machines] as x where MachineNumber = m.symbol2)

-- lwb
union all
select 
m.symbol2,
m.symbol,
m.nazwa,
5 as MachineCategoryId
from mwbase.prdkabat.maszyny as m
where typMaszyny = 4
and not exists(select * from [MwTech].[dbo].[Machines] as x where MachineNumber = m.symbol2)



-- dru
union all
select 
m.symbol2,
m.symbol,
m.nazwa,
6 as MachineCategoryId
from mwbase.prdkabat.maszyny as m
where typMaszyny = 6
and not exists(select * from [MwTech].[dbo].[Machines] as x where MachineNumber = m.symbol2)


)
 



