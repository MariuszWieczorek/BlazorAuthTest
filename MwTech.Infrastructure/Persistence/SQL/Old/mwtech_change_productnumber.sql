select ProductNumber,
REPLACE(ProductNumber,'-','') as Pr2,
REPLACE(REPLACE(ProductNumber,'-',''),'TE','-TE') as Pr3
from dbo.Products where ProductNumber like 'OSU%'


update dbo.Products 
set ProductNumber = REPLACE(REPLACE(ProductNumber,'-',''),'TE','-TE')
where ProductNumber like 'OSU%'