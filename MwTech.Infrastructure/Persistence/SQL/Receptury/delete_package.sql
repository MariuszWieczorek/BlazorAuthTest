  delete p
  FROM [MwTech].[dbo].[RecipeStages] as s
  inner join dbo.RecipeVersions as v
  on s.RecipeVersionId = v.Id
  inner join dbo.Recipes as r
  on r.Id = v.RecipeId
  inner join dbo.RecipePositionsPackages as p
  on p.RecipeStageId = s.id
  where RecipeNumber like '%MW15%' and p.PackageNumber = 3
  
