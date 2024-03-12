select * 
from dbo.ProductProperties as pp
inner join dbo.Properties as pr
on pr.id = pp.PropertyId
where pr.PropertyNumber = 'dst_dlugosc_po_styknieciu'

delete dbo.ProductProperties
from dbo.ProductProperties as pp
inner join dbo.Properties as pr
on pr.id = pp.PropertyId
where pr.PropertyNumber = 'dst_dlugosc_po_styknieciu'