select * 
from dbo.ProductProperties as pp
inner join dbo.Properties as p
on pp.PropertyId = p.Id
where p.PropertyNumber = 'dst_prad_laczenia'

update pp
set MaxValue = null, Value = null
from dbo.ProductProperties as pp
inner join dbo.Properties as p
on pp.PropertyId = p.Id
where p.PropertyNumber = 'dst_prad_laczenia'