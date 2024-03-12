/* 
ustawienie domyslenj wersji tam gdzie jej nie ma a jest tylko jedna wersja tak, �e nie ma 
zagro�enia �e si� ustawi wi�cej ni� jedn� wersj� jako domy�ln� 
*/

update vv
set DefaultVersion = 1
from dbo.ProductVersions as vv
inner join 
(
select p.Id, p.ProductNumber, SUM(iif(v.defaultversion=1,1,0)) as ile_def, count(*) as ile
from dbo.ProductVersions as v
inner join dbo.Products as p
on p.Id = v.ProductId
group by p.ProductNumber, p.id
having SUM(iif(v.defaultversion=1,1,0)) = 0 and count(*) = 1
) as x
on x.Id = vv.ProductId
