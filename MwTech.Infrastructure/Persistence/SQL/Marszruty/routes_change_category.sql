-- select ca.CategoryNumber, ca.Name as CategoryName, o.Name as Operation, c.ResourceNumber, pr.ProductNumber
/*
update r
set ResourceId = (select Id from dbo.Resources where ResourceNumber = 'PC.PO.PKON')
from dbo.Products as pr
inner join dbo.RouteVersions as v
on v.ProductId = pr.Id
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.Id
inner join dbo.Resources as c
on c.Id = r.ResourceId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.Operations as o
on o.Id = r.OperationId
where c.ResourceNumber like 'PC.PO%'
and pr.IsActive = 1
and v.IsActive = 1
and o.name = 'CIÊCIE NA IMESIE' and ca.CategoryNumber = 'MIE-PAF'
*/

-- OBK		Opona - Bie¿niki Komplet	KOMPLETACJA BIE¯NIKÓW		PC.PO.PP-0	PC.PO.SZYK	v
-- OBK		Opona - Bie¿niki Komplet	SMAROWANIE KLEJEM			PC.PO.PP-0	PC.PO.SZYK	v
-- OBK-TE	Opona - Bie¿niki Komplet	KOMPLETACJA BIE¯NIKÓW		PC.PO.PP-0	PC.PO.SZYK	v
--	
-- OSU		Opona Surowa				ZBIJANIE £¥CZEÑ				PC.PO.PP-0	PC.PO.PKON	v
-- OSU-TE	Opona Surowa				ZBIJANIE £¥CZEÑ				PC.PO.PP-0	PC.PO.PKON	v
-- OTT		Opona - Tkanina Ochronna	CIÊCIE NA IMESIE			PC.PO.PP-0	PC.PO.PKON	v
--
-- ODA		Opona - Drutówka z Apexem	£¥CZENIE APEXU Z DRUTÓWK¥	PC.PO.PP-0	PC.PO.DRUT

-- ODR		Opona - Drutówka			MONTA¯ OWIJKI				PC.PO.PP	PC.PO.DRUT	v
-- ODR		Opona - Drutówka			WYKONANIE DRUTÓWKI			PC.PO.PP	PC.PO.DRUT	v
--
-- OKC		Opona - Kord Gumowany Ciêty	CIÊCIE I NAWIJANIE			PC.PO.PP	PC.PO.PLA	v
-- OKD		Opona - doklejka			CIÊCIE NA IMESIE			PC.PO.PP	PC.PO.PKON
-- OSM		Opona Malowana				MALOWANIE					PC.PO.PP	PC.PO.MAL	v
-- OSM-TE	Opona Malowana				MALOWANIE					PC.PO.PP	PC.PO.MAL	v

-- OKG		Opona - Kord Gumowany		KALANDROWANIE TKANIN		PC.PO.KAL	PC.PO.WYT
-- OKG-TE	Opona - Kord Gumowany		KALANDROWANIE TKANIN		PC.PO.KAL	PC.PO.WYT


select ca.CategoryNumber, ca.Name as CategoryName, o.Name as Operation, c.ResourceNumber
from dbo.Products as pr
inner join dbo.RouteVersions as v
on v.ProductId = pr.Id
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.Id
inner join dbo.Resources as c
on c.Id = r.ResourceId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.Operations as o
on o.Id = r.OperationId
where c.ResourceNumber like 'PC.PO.PP-0%'
-- and pr.IsActive = 1
-- and v.IsActive = 1
group by ca.CategoryNumber, c.ResourceNumber, ca.name, o.Name
order by ca.CategoryNumber, c.ResourceNumber
