/****** Script for SelectTopNRows command from SSMS  ******/



select y.*
FROM [MwTech].[dbo].[ProductSettingVersions] as y
inner join 
(
SELECT Id, ProductId, AlternativeNo, ProductSettingVersionNumber,
  (select MAX(x.ProductSettingVersionNumber) FROM [MwTech].[dbo].[ProductSettingVersions] as x where x.ProductId = v.ProductId and x.AlternativeNo = v.AlternativeNo ) as mx
  FROM [MwTech].[dbo].[ProductSettingVersions] as v
  where MachineCategoryId = 1
) as x
on x.Id = y.id

UPDATE y
set IsActive = iif(y.ProductSettingVersionNumber = x.mx,1,0),
DefaultVersion = iif(y.ProductSettingVersionNumber = x.mx,1,0)
FROM [MwTech].[dbo].[ProductSettingVersions] as y
inner join 
(
SELECT Id, ProductId, AlternativeNo, ProductSettingVersionNumber,
  (select MAX(x.ProductSettingVersionNumber) FROM [MwTech].[dbo].[ProductSettingVersions] as x where x.ProductId = v.ProductId and x.AlternativeNo = v.AlternativeNo ) as mx
  FROM [MwTech].[dbo].[ProductSettingVersions] as v
  where MachineCategoryId = 1
) as x
on x.Id = y.id