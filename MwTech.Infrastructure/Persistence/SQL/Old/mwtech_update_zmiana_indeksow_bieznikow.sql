select p.ProductNumber,
REPLACE(REPLACE(p.ProductNumber,'-B-','-'),'OBI','OBB')
from dbo.Products as p
where p.ProductNumber like 'OBI%-B-%'

select p.ProductNumber,
REPLACE(REPLACE(p.ProductNumber,'-C-','-'),'OBI','OBC')
from dbo.Products as p
where p.ProductNumber like 'OBI%-C-%'

select p.ProductNumber,
REPLACE(REPLACE(p.ProductNumber,'-K-','-'),'OBI','OBK')
from dbo.Products as p
where p.ProductNumber like 'OBI%-K-%'


  UPDATE p
       set p.ProductNumber = REPLACE(REPLACE(p.ProductNumber,'-K-','-'),'OBI','OBK')
from dbo.Products as p
where p.ProductNumber like 'OBI%-K-%'

