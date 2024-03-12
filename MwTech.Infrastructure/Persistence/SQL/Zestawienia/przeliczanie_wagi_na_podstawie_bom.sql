select dbo.getProductWeight(12)
select * from dbo.mwtech_bom_cte(12,default) where HowManyParts = 0  and PartDoesNotIncludeInWeight = 0
select SUM(FinalPartProductWeight) from dbo.mwtech_bom_cte(12,default) where HowManyParts = 0 and PartDoesNotIncludeInWeight = 0
select SUM(FinalPartProductQty) from dbo.mwtech_bom_cte(12,default) where HowManyParts = 0   and PartDoesNotIncludeInWeight = 0

-- Uwaga aby dobrze wylicza�a si� waga, ka�dy sk�adnik musi mie� dodan� wersj� domy�ln�
-- MIE.PBK60-2-F mia� sumaryczn� wag� sk�adnik�w 154,81
-- MIE.PBK60-2-F-NAWROT w ilo�ci 4kg nie mia� �adnej wersji BOM
-- przez co te 4kg nie wlicza�y si� i algorytm zwraca� 150,81 zamiast 154,81