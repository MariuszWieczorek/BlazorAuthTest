
use mwtech;

with cc as
(
select pr.ProductNumber, pr.OldProductNumber
from dbo.Products as pr
where pr.ProductNumber IN 
('DC15825PREMV328KBWK'
,'DC16750VKBWK'
,'DC181200V3211KBWK'
,'DC201100VKBWK'
,'DC201200PREMV368KBWK'
,'DM10350V1091KBWK'
,'DM12300V1091KBWK'
,'DM16300HDV1091KBWK'
,'DM16300V1091KBWK'
,'DM16325V1091KBWK'
,'DM16350HDHV1091KBWK'
,'DM16350HDNV1091KBWK'
,'DM16350V1091KBWK'
,'DM16450V1091KBWK'
,'DM17350V1091KBWK'
,'DM17400HDV1091KBWK'
,'DM17400V1091KBWK'
,'DM17450HDDV1091KBWK'
,'DM17450HDEV1091KBWK'
,'DM17450HDKV1091KBWK'
,'DM17450HDLV1091KBWK'
,'DM18300HDV1091KBWK'
,'DM18300V1091KBWK'
,'DM18350HDKV1091KBWK'
,'DM18350HDNV1091KBWK'
,'DM18350V1091KBWK'
,'DM18400450V1091KBWK'
,'DM18400HDV1091KBWK'
,'DM18400V1091KBWK'
,'DM18450V1091KBWK'
,'DM18475HDKV1091KBWK'
,'DM18475HDV1091KBWK'
,'DM18475V1091KBWK'
,'DM19300HDV1091KBWK'
,'DM19300V1091KBWK'
,'DM19325V1091KBWK'
,'DM19350400V1091KBWK'
,'DM19350HDKV1091KBWK'
,'DM19350HDTR13KBWK'
,'DM19350HDV1091KBWK'
,'DM19350V1091KBWK'
,'DM19375V1091KBWK'
,'DM19400V1091KBWK'
,'DM19450V1091KBWK'
,'DM19500HDV1091KBWK'
,'DM21275HDV1091KBWK'
,'DM21275V1091KBWK'
,'DM21300HDIV1091KBWK'
,'DM21300HDKV1091KBWK'
,'DM21300HDNV1091KBWK'
,'DM21300V1091KBWK'
,'DM21325HDV1091KBWK'
,'DM21325V1091KBWK'
,'DM22275HDV1091KBWK'
,'DM22275V1091KBWK'
,'DM23275HDV1091KBWK'
,'DM23275V1091KBWK'
,'DM25300V1091KBWK'
,'DM27300V1091KBWK'
,'DMO14275V1061KBWK'
,'DMO17250V1061KBWK'
,'DMO17300TR4KBWK'
,'DMO18275TR4KBWK'
,'DMO18575GP4KBWK'
,'DMO925TR87SKBWK'
,'DO1324570HDTR13KBWK'
,'DO1326570HDTR13KBWK'
,'DO14155165V1091KBWK'
,'DO15165175V1091KBWK'
,'DO22283V1091KBWK'
,'DR18600TR75KBWK'
,'DR208095PREMTR218AKBWK'
,'DR22560050PREMTR218AKBWK'
,'DR24200TRJ1175CKBWK'
,'DR25205TRJ1175CKBWK'
,'DR25235TRJ1175CKBWK'
,'DR25255TRJ1175CKBWK'
,'DR331800TRJ1175CKBWK'
,'DR3871070HDMTR218AKBWK'
,'DR42208PREMTR218KBWK'
,'DT1020800V1091KBWK'
,'DT8187V1091KBWK'
,'DT8219V1091KBWK'
,'DT8350TR87SKBWK'
,'DT8500V1091KBWK'
,'DMO17275GP4KBWK'

) 
)

select 
  cc.OldProductNumber
, cc.ProductNumber as kbprod
, replace(cc.ProductNumber,'KBWK','') as kbwk2
, Pr.ProductNumber as ntprod
,replace(Pr.ProductNumber,'NTWK','') as ntwk2

from cc as cc
left join dbo.Products as pr
on pr.OldProductNumber = cc.OldProductNumber
and pr.ProductNumber like '%NTWK'

--order by OldProductNumber
--group by OldProductNumber
--having count(*) > 1