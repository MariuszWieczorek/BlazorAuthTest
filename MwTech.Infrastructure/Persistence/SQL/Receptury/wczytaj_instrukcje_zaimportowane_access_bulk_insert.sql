use MwTech;

declare @opis_wersji_from as varchar(50);
declare @opis_wersji_to as varchar(50);

set @opis_wersji_from = 'AMR75-02A-LM1/LM4'
set @opis_wersji_to   = 'AMR75-02A-LM1/LM4'


select 
( select id from dbo.RecipeVersions as v where v.Name = @opis_wersji_to ) as RecipeVersionId
,i.cykl
,( select s.id from dbo.RecipeStages as s inner join dbo.RecipeVersions as v on v.Id = s.RecipeVersionId and v.Name = @opis_wersji_to where s.StageNo = isnull(i.cykl,1) ) as RecipeStageId
, isnull(i.lp,0)
, i.description
, i.textval
, i.mieszanka
from dbo.access_mieszanki_instrukcje as i
where 1 = 1
and mieszanka like '%OLP60%'

select mieszanka
from dbo.access_mieszanki_instrukcje as i
where 1 = 1
and mieszanka like '%OLP60%'
group by mieszanka


select 
( select id from dbo.RecipeVersions as v where v.Name = @opis_wersji_to ) as RecipeVersionId
,( select s.id from dbo.RecipeStages as s inner join dbo.RecipeVersions as v on v.Id = s.RecipeVersionId and v.Name = @opis_wersji_to where s.StageNo = isnull(i.cykl,1) ) as RecipeStageId
, isnull(i.lp,0)
, i.description
, i.textval
, i.mieszanka
from dbo.access_mieszanki_instrukcje as i
where 1 = 1
and mieszanka = @opis_wersji_from


delete from dbo.RecipeManuals
where recipeVersionId = ( select id from dbo.RecipeVersions as v where v.Name = @opis_wersji_to )

insert into dbo.RecipeManuals
( RecipeVersionId, RecipeStageId, PositionNo, Description, TextValue )
(
select 
( select id from dbo.RecipeVersions as v where v.Name = @opis_wersji_to ) as RecipeVersionId
,( select s.id from dbo.RecipeStages as s inner join dbo.RecipeVersions as v on v.Id = s.RecipeVersionId and v.Name = @opis_wersji_to where s.StageNo = isnull(i.cykl,1) ) as RecipeStageId
, isnull(i.lp,0)
, i.description
, i.textval
from dbo.access_mieszanki_instrukcje as i
where 1 = 1
and mieszanka = @opis_wersji_from
and i.description is not null)
