select * FROM [MwTech].[dbo].[ProductVersions]
where ProductId in (
SELECT [ProductId]
FROM [MwTech].[dbo].[ProductVersions]
where DefaultVersion = 1
group by ProductId
having COUNT(*) > 1)

-- Delete FROM [MwTech].[dbo].[ProductVersions] where  Id = 2041