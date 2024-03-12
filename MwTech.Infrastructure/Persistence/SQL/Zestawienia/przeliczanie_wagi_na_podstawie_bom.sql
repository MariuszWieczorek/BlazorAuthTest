select dbo.getProductWeight(12)
select * from dbo.mwtech_bom_cte(12,default) where HowManyParts = 0  and PartDoesNotIncludeInWeight = 0
select SUM(FinalPartProductWeight) from dbo.mwtech_bom_cte(12,default) where HowManyParts = 0 and PartDoesNotIncludeInWeight = 0
select SUM(FinalPartProductQty) from dbo.mwtech_bom_cte(12,default) where HowManyParts = 0   and PartDoesNotIncludeInWeight = 0

-- Uwaga aby dobrze wylicza³a siê waga, ka¿dy sk³adnik musi mieæ dodan¹ wersjê domyœln¹
-- MIE.PBK60-2-F mia³ sumaryczn¹ wagê sk³adników 154,81
-- MIE.PBK60-2-F-NAWROT w iloœci 4kg nie mia³ ¿adnej wersji BOM
-- przez co te 4kg nie wlicza³y siê i algorytm zwraca³ 150,81 zamiast 154,81