declare @desc as varchar(100)
set @desc = 'WULKANIZACJA'+CHAR(13)+CHAR(10)+'1.USZCZELNIACZE WULKANIZOWA� Z U�YCIEM RAMEK' 

select * from dbo.Products where Description like '%USZCZELNIACZE WULKANIZOWA� Z U�YCIEM RAMEK%'
select @desc
update dbo.Products SET Description = @desc where Description like '%USZCZELNIACZE WULKANIZOWA� Z U�YCIEM RAMEK%'