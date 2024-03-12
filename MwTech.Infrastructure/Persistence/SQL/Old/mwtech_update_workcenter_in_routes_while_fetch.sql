/****** Script for SelectTopNRows command from SSMS  ******/
declare @OperationNo varchar(50);
declare @OperationId int;
declare @ProductNo varchar(50);
declare @WorkCenterId int;
declare @WorkCenterNo varchar(10);
set @OperationNo= 'OWU-WULKANIZACJA';
set @OperationId = (select op.Id from [MwTech].[dbo].[Operations] as op where op.OperationNumber = @OperationNo) 
-- zmiennne

DECLARE @tabConvert table (id int identity(1, 1),
ProductNo varchar(50),
WorkCenterNo varchar(10));

insert into @tabConvert (ProductNo,WorkCenterNo) values
('KOGT1834080121L','H08'),
('KOGT1834080141L','H08'),
('KOGT28169121L','I03'),
('KOGT28169121L-TE','I03'),
('KOGT28169141L','I03'),
('KOGT28169141L-TE','I03'),
('KOIM1040041T','F01'),
('KOIM1210080101L','G01'),
('KOIM1210080101L-TE','G01'),
('KOIM1210080101T','G01'),
('KOIM1210080101T-TE','G01'),
('KOIM121008081L','G01'),
('KOIM121008081L-TE','G01'),
('KOIM1270061T','G09'),
('KOIM1350061T','G01'),
('KOIM15310075101L','G02'),
('KOIM15310075101T','G02'),
('KOIM15310075121I','G02'),
('KOIM15310075121L','G02'),
('KOIM15310075121T','G02'),
('KOIM15310075141I','G02'),
('KOIM15310075141L','G02'),
('KOIM15310075141T','G02'),
('KOIM15311580121T','I08'),
('KOIM15311580141L','I08'),
('KOIM15311580161L','I08'),
('KOIM15311580161T','I08'),
('KOIM15540060141L','H09'),
('KOIM15540060141L-TE','H09'),
('KOIM15540060141T','H09'),
('KOIM1610565121T','H08'),
('KOIM16900141T','I09'),
('KOIM16900141T-TE','I09'),
('KOIM1813065161T','H08'),
('KOIM1813065161T-TE','H08'),
('KOIM61560061L','F04'),
('KOIM61560062L','F05'),
('KOIM61560062T','F04'),
('KOIM81665061L','F03'),
('KOIM81665061T','F07'),
('KOIM81665062L','F07'),
('KOIM81665062T','F03'),
('KOIM81885061L','F07'),
('KOIM81885061T','F03'),
('KOIM81885062L','F04'),
('KOIM81885062T','F06'),
('KOIM81885062T-TE','F06'),
('KOIM840041T','F01'),
('KOIM840061T','F01'),
('KOIM950041T','F08'),
('KONP12700141T','G01'),
('KONP12700141T-TE','G01'),
('KONP13500101T','G01'),
('KONP13500101T-TE','G01'),
('KONP15815141T','G02'),
('KONP15815141T-TE','G02'),
('KONP8500101T','F04'),
('KONP8500101T-TE','F04'),
('KONP9600121T','F02'),
('KONP9600121T-TE','F02'),
('KOSG15540060142L','H09'),
('KOSG15540060142L-TE','H09'),
('KOSG1660061T','G07'),
('KOSG1660081T','G04'),
('KOSG1675061T','G09'),
('KOSG1675081T','G01'),
('KOSG1675081T-TE','G01'),
('KOSG2411281I','I12'),
('KOSG2411281T','I12'),
('KOSG24124101T','I11'),
('KOSG24124121T','I11'),
('KOSG2412481I','I11'),
('KOSG2412481T','I11'),
('KOSG2414981T','H05'),
('KOSG249581T','I12'),
('KOSG249581T-TE','I12'),
('KOSG2811281T','I12'),
('KOSG2812481T','H01'),
('KOSG2812481T-TE','H01'),
('KOSG2814981T','H01'),
('KOSG28169141T','H04'),
('KOSG28169141T-TE','H04'),
('KOSG30169121I','I03'),
('KOSG30169121T','I03'),
('KOSG30169141T','H05'),
('KOSG30169142T','I06'),
('KOSG30169142T-TE','I06'),
('KOSG3016981T','H05'),
('KOSG30184121T','H05'),
('KOSG30184142T','I06'),
('KOSG30184142T-TE','I06'),
('KOSG3212481T','H01'),
('KOSG329561T','I02'),
('KOSG329581T','I02'),
('KOSG34169121T','H07'),
('KOSG34169141I','H07'),
('KOSG34169141T','H07'),
('KOSG34184121T','H06'),
('KOSG34184141I','H06'),
('KOSG34184141T','H06'),
('KOSG34184141T-TE','H06'),
('KOSG3695101T','H05'),
('KOSG369561T','I03'),
('KOSR1660061T','G01'),
('KOSR1660062T','G02'),
('KOSR1660081T','G06'),
('KOSR1660082T','G02'),
('KOSR1675061T','G04'),
('KOSR1675062T','G01'),
('KOSR1675081I','G4,'),
('KOSR1675081T','G04'),
('KOSR1675082T','G08'),
('KOSR1860061T','G10'),
('KOSR1875062T','G08'),
('KOSR1875082T','G08'),
('KOSR1875082T-TE','G08'),
('KOSR2075081T','H08'),
('KOIM15311580141T','I08'),
('KOIM61560061T','F03'),
('KOSG30184141T','h06'),
('KOSG28169101T','h05'),
('KOSG2813681T','h03'),
('KOIM16900121T','H08'),
('KOSG38155101T','I07'),
('KOSG38155101T-TE','I07'),
('KOIM1610565121T-TE','F01');





declare c cursor fast_forward for
select ProductNo,WorkCenterNo from @tabConvert

open c;
fetch next from c into @ProductNo,@WorkCenterNo;

   while @@fetch_status = 0
   begin
    --
	set @WorkCenterId = ( select r.id from [dbo].[Resources] as r where r.ResourceNumber = @WorkCenterNo )
	print @ProductNo;

	UPDATE r
	set r.WorkCenterId = @WorkCenterId
  FROM [MwTech].[dbo].[ManufactoringRoutes] as r
  inner join [MwTech].[dbo].[ProductVersions] as pv
  on pv.Id = r.ProductVersionId
  inner join [MwTech].[dbo].[Products] as pr
  on pr.Id = pv.ProductId
  where r.OperationId = @OperationId 
  and pr.ProductNumber = @ProductNo

	--
   	fetch next from c into  @ProductNo,@WorkCenterNo;
   end

   close c;
   deallocate c;








