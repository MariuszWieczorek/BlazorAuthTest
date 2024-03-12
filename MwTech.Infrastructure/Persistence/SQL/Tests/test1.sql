select 
*
from mwtech_bom_cte (15465, null)
where SetProductNumber not like 'MIE%'


select 
level,SetProductId,SetProductVersionId,SetProductNumber 
from mwtech_bom_cte (15465, null)
where  HowManyParts > 0 and SetProductNumber not like 'MIE%'
