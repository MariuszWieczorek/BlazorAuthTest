
select r.RecipeNumber, r.Name as RecipeName
, ISNULL(v.VersionNumber, 1) as VersionNumber
, ISNULL(v.AlternativeNo, 1) as AlternativeNo
, ISNULL(v.Name, '') as VersionName
from dbo.Recipes as r
left join dbo.RecipeVersions as v
on v.RecipeId = r.id
order by r.RecipeNumber, v.VersionNumber, v.AlternativeNo


select * from dbo.Recipes where recipenumber like 'MIE.X%'

update dbo.Recipes set RecipeCategoryId = 13 where recipenumber like 'MIE.X%'