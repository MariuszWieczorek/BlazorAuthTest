declare @dt as datetime
declare @userId  as varchar(100)
set @dt = getdate()
set @userId = 'fbcf1e3b-8936-445d-95ca-47af1674afd5'


update v
set CreatedByUserId = @userId, CreatedDate = @dt
  FROM [MwTech].[dbo].[RecipeVersions] as v
  where CreatedByUserId is null
  
  -- 