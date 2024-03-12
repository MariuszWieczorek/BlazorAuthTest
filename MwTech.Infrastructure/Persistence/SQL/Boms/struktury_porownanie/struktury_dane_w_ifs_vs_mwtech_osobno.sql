DECLARE @x as VARCHAR(30) 
SET @x = 'DAP6009TR87FI45KB-BUT'

select * from dbo.mwtech_bom_ifs
WHERE PART_NO = @x

select * from dbo.ifs_struktury
WHERE PART_NO = @x


select ifs.PART_NO, ifs.part_status,  pr.IsActive
from dbo.ifs_struktury as ifs
inner join dbo.Products as pr
on trim(pr.ProductNumber) = trim(ifs.PART_NO)
and ifs.part_status != 'A'
and pr.IsActive = 1

