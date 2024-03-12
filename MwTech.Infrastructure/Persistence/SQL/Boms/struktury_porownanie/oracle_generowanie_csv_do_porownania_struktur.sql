/*GENEROWANIE PLIKU CSV DO PORÓWNANIA STRUKTUR PRODUKTOWYCH */

-- INVENTORY_PART_API.Get_Part_Status(h.CONTRACT,h.PART_NO) = 'A' Aktywny Indeks

SELECT 
  h.CONTRACT
, h.PART_NO
-- , INVENTORY_PART_API.Get_Part_Status(h.CONTRACT,h.PART_NO) as PART_STATUS
-- , INVENTORY_PART_API.Get_Description(s.Contract,s.PART_NO) as PART_NAME
, h.ENG_CHG_LEVEL as ENG_CHG_LEVEL
, PART_REVISION_API.Get_Revision_Text(h.contract,h.part_no,h.ENG_CHG_LEVEL) as RevisionName
, v.ALTERNATIVE_NO
, v.ALTERNATIVE_DESCRIPTION
, MANUF_STRUCT_ALTERNATE_API.Get_State(h.contract,h.part_no,h.eng_chg_level,h.bom_type,v.alternative_no) as ALTERNATIVE_STATE
, s.LINE_ITEM_NO
, s.LINE_SEQUENCE
, s.COMPONENT_PART
, trim(to_char(s.QTY_PER_ASSEMBLY,'999990.99999')) as QTY_PER_ASSEMBLY
, s.CONSUMPTION_ITEM_DB
, s.PRINT_UNIT
, trim(to_char(s.EFF_PHASE_IN_DATE,'YYYYMMdd')) as EFF_PHASE_IN_DATE
, trim(to_char(s.EFF_PHASE_OUT_DATE,'YYYYMMdd')) as EFF_PHASE_OUT_DATE
, trim(to_char(s.COMPONENT_SCRAP,'999990.99999')) as COMPONENT_SCRAP
, trim(to_char(s.SHRINKAGE_FACTOR,'999990.99999')) as SHRINKAGE_FACTOR
, INVENTORY_PART_API.Get_Part_Status(h.CONTRACT,h.PART_NO) as PART_STATUS
--
FROM PROD_STRUCTURE_HEAD  h
INNER JOIN PROD_STRUCT_ALTERNATE v
on  v.part_no = h.part_no 
and v.eng_chg_level = h.eng_chg_level
and v.contract = h.contract
and v.state IN ('Buildable')  -- ,'Tentative'      
--
INNER JOIN PROD_STRUCTURE s
on  s.part_no = v.part_no 
and s.alternative_no = v.alternative_no
and s.eng_chg_level = v.eng_chg_level
and s.contract = v.contract
--
where 1 = 1
and  s.EFF_PHASE_OUT_DATE is null
and INVENTORY_PART_API.Get_Part_Status(h.CONTRACT,h.PART_NO) = 'A'


	

