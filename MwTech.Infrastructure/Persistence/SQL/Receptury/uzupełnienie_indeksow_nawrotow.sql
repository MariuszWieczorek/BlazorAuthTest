DECLARE @RecipeId int;
DECLARE @ScrapNumber varchar(100);
DECLARE @RecipeNumber varchar(100);
DECLARE @ProductNumber varchar(100);

DECLARE @tmp table 
(
 id int identity(1, 1)
,ScrapNumber varchar(100)
,RecipeNumber varchar(100)
,ProductNumber varchar(100)
);

INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.BB55','MIE.BB55-2-F','MIE.BB55-2-F-NAWROT')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.BBR50','MIE.BBR50-2-F','MIE.BBR50-2-F-NAWROT')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.BKR65','MIE.BKR65-2-F','MIE.BKR65-2-F-NAWROT')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.BMO65','MIE.BMO65-2-F','MIE.BMO65-2-F-NAWROT')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.BOB60','MIE.BOB60-1-F','MIE.BOB60-1-F-NAWROT')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.BOH65','MIE.BOH65-2-F','MIE.BOH65-2-F-NAWROT')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.BUTYL50','MIE.BUTYL50-1','MIE.BUTYL50-1-NAWROT')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.DS55','MIE.DS55-2-F','MIE.DS55-2-F-NAWROT')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.DS55','MIE.DS55-2-F','MIE.DS55-2-F-SCINKI')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.DSO55','MIE.DSO55-2-F','MIE.DSO55-2-F-NAWROT')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.EP45','MIE.EP45-2-F','MIE.EP45-2-F-NAWROT')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.EPW45','MIE.EPW45-2-F','MIE.EPW45-2-F-NAWROT')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.FABR88','MIE.FABR88-1','MIE.FABR88-NAWROT')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.GDK85','MIE.GDK85-2-F','MIE.GDK85-2-F-NAWROT')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.GK55','MIE.GK55-2-F','MIE.GK55-2-F-NAWROT')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.HARD07','MIE.HARD07-1','MIE.HARD07-NAWROT')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.HARD21','MIE.HARD21-1','MIE.HARD21-NAWROT')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.ITT','MIE.ITT-2-F','MIE.ITT-2-F-NAWROT')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.K','MIE.K-1-F','MIE.K-1-F-NAWROT')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.KNR55','MIE.KNR55-2-F','MIE.KNR55-2-F-NAWROT')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.MOB65','MIE.MOB65-1-F','MIE.MOB65-1-F-NAWROT')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.OST','MIE.OST-2-F','MIE.OST-2-F-NAWROT')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.PBK60','MIE.PBK60-2-F','MIE.PBK60-2-F-NAWROT')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.SIDE62','MIE.SIDE62-1','MIE.SIDE62-NAWROT')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.SIDE74','MIE.SIDE74-1','MIE.SIDE74-NAWROT')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.SL4/922','MIE.SL4/922-2-F','MIE.SL4/922-2-F-NAWROT')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.WPK85','MIE.WPK85-1-F','MIE.WPK85-1-F-NAWROT')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.BBR50','MIE.BBR50-2-F','NT.M.FILT.BBR50-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.BKR65','MIE.BKR65-2-F','NT.M.FILT.BKR65-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.BMO65','MIE.BMO65-2-F','NT.M.FILT.BMO65-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.BOH65','MIE.BOH65-2-F','NT.M.FILT.BOH65-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.BUTYL50','MIE.BUTYL50-2-F','NT.M.FILT.BUTYL50-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.BUTYL65','MIE.BUTYL65-2-F','NT.M.FILT.BUTYL65-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.DDK50','MIE.DDK50-2-F','NT.M.FILT.DDK50-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.DS55','MIE.DS55-2-F','NT.M.FILT.DS55-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.GDK85','MIE.GDK85-2-F','NT.M.FILT.GDK85-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.PBK60','MIE.PBK60-2-F','NT.M.FILT.PBK65-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.SL4/922','MIE.SL4/922-2-F','NT.M.FILT.SL4/922-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.WPK85','MIE.WPK85-1-F','NT.M.FILT.WPK85-1-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.BKR65','MIE.BKR65-2-F','NT.M.PLYT.BKR65-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.BMO65','MIE.BMO65-2-F','NT.M.PLYT.BMO65-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.DS55','MIE.DS55-2-F','NT.M.PLYT.DS55-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.DDK50','MIE.DDK50-2-F','NT.M.PLYT.DSO55-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.GDK85','MIE.GDK85-2-F','NT.M.PLYT.GDK85-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.GK55','MIE.GK55-2-F','NT.M.PLYT.GK55-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.ITT','MIE.ITT-2-F','NT.M.PLYT.ITT-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.KNR55','MIE.KNR55-2-F','NT.M.PLYT.KNR55-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.OST','MIE.OST-2-F','NT.M.PLYT.OST-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.PBK60','MIE.PBK60-2-F','NT.M.PLYT.PBK65-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.WPK85','MIE.WPK85-1-F','NT.M.PLYT.WPK85-1-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.BBR50','MIE.BBR50-2-F','NT.M.REC.BBR50-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.BKR65','MIE.BKR65-2-F','NT.M.REC.BKR65-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.BMO65','MIE.BMO65-2-F','NT.M.REC.BMO65-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.BUTYL50','MIE.BUTYL50-2-F','NT.M.REC.BUTYL50-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.BUTYL65','MIE.BUTYL65-2-F','NT.M.REC.BUTYL65-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.DDK50','MIE.DDK50-2-F','NT.M.REC.DDK50-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.DS55','MIE.DS55-2-F','NT.M.REC.DS55-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.GDK85','MIE.GDK85-2-F','NT.M.REC.GDK85-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.MOB65','MIE.MOB65-2-F','NT.M.REC.MOB65-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.PBK65','MIE.PBK65-2-F','NT.M.REC.PBK65-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.SL4/922','MIE.SL4/922-2-F','NT.M.REC.SL4/922-2-F')
INSERT INTO @tmp (RecipeNumber,ProductNumber,ScrapNumber) VALUES ('MIE.WPK85','MIE.WPK85-1-F','NT.M.REC.WPK85-1-F')


declare c cursor fast_forward for
select RecipeNumber,ProductNumber,ScrapNumber from @tmp

open c;
fetch next from c into @RecipeNumber,@ProductNumber,@ScrapNumber;

   while @@fetch_status = 0
   begin
    --
	SET @RecipeId = (SELECT Id from dbo.Recipes WHERE RecipeNumber = @RecipeNumber)
	--
	if  @RecipeId is not null
	BEGIN
		print @RecipeNumber;
		update dbo.Recipes SET ScrapNumber = @ScrapNumber WHERE id = @RecipeId
		--insert into dbo.RecipeVersions (RecipeId, VersionNumber, AlternativeNo, Name, IsActive, DefaultVersion, IsAccepted01, IsAccepted02) values (@RecipeId,@VersionNumber,@AlternativeNo, @VersionName, 1, 1, 0, 0)
	END
	ELSE
	BEGIN
		print 'Brak  ' + @RecipeNumber
	END

	-- UPDATE r
	

	--
   	fetch next from c into  @RecipeNumber,@ProductNumber,@ScrapNumber;
   end

   close c;
   deallocate c;