 DECLARE @id int
DECLARE @RecipeNumber varchar(20)
DECLARE @RecipeId int
DECLARE @RecipeName varchar(100)
DECLARE @VersionNumber int 
DECLARE @AlternativeNo int 
DECLARE @VersionName varchar(100)

DECLARE @tmp table 
(
 id int identity(1, 1)
,RecipeNumber varchar(20)
,RecipeName varchar(100)
,VersionNumber int 
,AlternativeNo int 
,VersionName varchar(100)
);


INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.AH85','Mieszanka gumowa AH 85',1,1,'AH85-08_M1/BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.ALL','Mieszanka dla firmy Wegmann',1,1,'ALL-01_LM2/LM4')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.AM70','Mieszanka NR o twardo�ci 70 ShA',1,1,'AMG73-01_BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.AMG73','Mieszanka NR 73 ShA',1,1,'AMG73-01_BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.AMR75','Mieszanka na amortyzator rolniczy',1,1,'AMR75-02A-LM1/LM4')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.AMR80','Mieszanka na amortyzator rolniczy',1,1,'AMR80-PR02_M2/BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.ANR85','Mieszanka na obojniki wewn�trzne miech�w resor�w 85 ShA',1,1,'ANR85-01_LM1/LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.AS75','Mieszanka gumowa AS 75',1,1,'AS75-04_GTS')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.BB55','Mieszanka na boki opon',1,1,'BB55-78_LM1/LM4')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.BBR50','Mieszanka bromobutylowa',1,1,'BBR50-04A-LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.BCE65','Mieszanka bie�nikowa opon ci�arowych ekonomiczna',1,1,'BCE65-01_GTS')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.BIN','MIESZANKA BINDENGUMA',1,1,'BIN-06-LM1/LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.BKR65','Mieszanka BKR65',1,1,'BKR65-32A_LM2/LM4')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.BKR65','Mieszanka BKR65',1,2,'BKR65-32A_LM2/LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.BKR65','Mieszanka BKR65',2,1,'BKR65-32B_LM2/LM4')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.BKR65','Mieszanka BKR65',2,2,'BKR65-32B_LM2/LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.BKR65','Mieszanka BKR65',3,1,'BKR65-33_LM2/LM4')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.BKR65','Mieszanka BKR65',3,2,'BKR65-33_LM2/LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.BM60','Mieszanka na bie�nik osobowy BM 60',1,1,'BM60-62_M1/BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.BMD65','Mieszanka bie�nikowa grupy T4',1,1,'BMD65-PR25-LM1/LM4')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.BMO65','Mieszanka bie�nikowa BMO65',1,1,'07E-LM1/LM4')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.BNO55','mieszanka gumowa naprawcza',1,1,'BNO55-17-LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.BO40','Mieszanka bie�nikowa 40 ShA',1,1,'BO40-00_LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.BO50','Mieszanka na bie�nik BO 50',1,1,'BO50-19_BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.BO60','Mieszanka na bie�nik BO 60',1,1,'BO60-15_BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.BO65','Mieszanka na bie�nik opon ci�arowych BO 65',1,1,'BO65-31-LM1/LM4 - kalkulacja')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.BO70','Mieszanka na bie�nik opon ci�arowych BO 70',1,1,'BO70-75B_M1/BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.BO75','Mieszanka gumowa BO 75',1,1,'BO75-36_M2/BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.BOB60','Mieszanka bie�nikowa 60-66 ShA',1,1,'BOB60-109_HIM')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.BOH65','Mieszanka na bie�nik/kap� opon HIG',1,1,'BOH65-PR03-LM2/LM4')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.BOT65','Mieszanka na bie�nik opon nak�adanych na orbitread',1,1,'BOT65-PR02_LM2/LM4')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.BSE65','Mieszanka bie�nikowa ekonomiczna',1,1,'BSE65-PR21_GTS')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.BUTYL50','Mieszanka na d�tki Continental',1,1,'BUTYL50-PR21_M1/BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.BUTYL55','Mieszanka na d�tki butylowe',1,1,'BUTYL55-PR17A-LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.BUTYL60','mieszanka butylowa',1,1,'BUTYL60-08_BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.BUTYL65','MIESZANKA BUTYLOWA',1,1,'BUTYL65-33-LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.DDK50','mieszanka na detki do dekatyzacji',1,1,'DDK50-08-LM2/LM4')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.DS55','Mieszanka na d�tki samochodowe DS55',1,1,'DS55-384_LM1/LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.DS55','Mieszanka na d�tki samochodowe DS55',1,1,'DS55-384_LM1/LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.DS55','Mieszanka na d�tki samochodowe DS55',2,1,'DS55-385-LM1/LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.DSO55','Mieszanka na warstw� wewn�trzn� opon bezd�tkowych',1,1,'DSO55-01_LM1/LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.DW60','Mieszanka na dywaniki DW60',1,1,'DW60-53_BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.EN45','Mieszanka HIGH EPDM 40-50 ShA',1,1,'06-LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.EN45','Mieszanka HIGH EPDM 40-50 ShA',1,1,'EN70 PR01')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.EN55','Mieszanka EPDM - opony na place zabaw',1,1,'EN55-PR01-LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.EP45','mieszanka EPDM  40-50 Sha',1,1,'EP45-06C-LM2/LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.EP50','Mieszanka EPDM 50 ShA',1,1,'EP 50-10')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.EPA70','mieszanka do wulkanizacji w autoklawie',1,1,'EPA70-06-LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.EPB50','Mieszanka EPDM 50 ShA',1,1,'EPB50-00-LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.EPG45','Mieszanka na kauczuku etylenowo-propylenowym o twardo�ci 45_copy',1,1,'EPG45-PR02-LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.EPG70','Mieszanka EPDM 70Sha na wtryskarki',1,1,'EPG70-01-LM2/LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.EPL60','mieszanka na profile',1,1,'EPL60-15-LM2/LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.EPL70','mieszanka na profile 70 ShA',1,1,'EPL70-13-LM2/LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.EPN47','Mieszanka EPDM 40-50 ShA',1,1,'EPN47-01_M2/BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.EPN70','Mieszanka EPDM 65-75 ShA',1,1,'EPN70-01_LM2/LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.EPO70','Mieszanka na profile 70 ShA',1,1,'EPO70-03A-LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.EPT70','mieszanka EPDM70',1,1,'EPT70-18_BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.EPU50','mieszanka gumowa',1,1,'EPU50-02A-LM2/LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.EPU60','mieszanka na profile',1,1,'EPU60-21-LM2/LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.EPU70','mieszanka profilowa produkcja w�asna',1,1,'EPU70-30A-LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.EPU80','mieszanka na kauczuku etylenowo-propylenowym',1,1,'EPU80-09-LM2/LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.EPW45','Mieszanka na bazie kauczuku EPDM 40-50 ShA',1,1,'EPW45-02C_LM2/LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.EPW70','MIeszanka EPDM 65-72 ShA',1,1,'EPW70-01B_M2/BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.EPX60','Mieszanka EPDM 60 ShA',1,1,'EPX60-04-LM2/LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.EPX70','Mieszanka EPDM 70 ShA',1,1,'EPX70-03-LM2/LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.EPX75','Mieszana na profile 75 ShA',1,1,'PR46')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.EPX75','Mieszana na profile 75 ShA',1,1,'PR43')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.EPX75','Mieszana na profile 75 ShA',1,1,'PR43')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.EPX85','Mieszanka na profile 85ShA, ci�g solny',1,1,'')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.EST60','mieszanka na styk�wk� do profili',1,1,'EST60-03_WAL')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.FABR88','Mieszanka FABR88',1,1,'FABR88-KAB1_M2')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.FEM80','mieszanka do �ywno�ci',1,1,'wersja 1')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.FNB','Mieszanka NBR do kontaktu z �ywno�ci� ',1,1,'PR01')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.GDK85','Mieszanka na gumowanie drut�wki',1,1,'05-LM1/LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.GF60','mieszanka metal-guma 60 Sh`A',1,1,'GF60-12_LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.GK55','Mieszanka do gumowania kordu',1,1,'GK55-16-LM2/LM4')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.GP75','mieszanka do gumowania p�askownik�w',1,1,'GP75-02_M1/BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.HARD07','mieszanka HARD07',1,1,'HARD07-KAB4B-LM2')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.HARD13','Mieszanka HARD13',1,1,'HARD13-KAB1-LM1')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.HARD21','Mieszanka HARD21',1,1,'HARD21-KAB8_LM1')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.IBZ','Mieszanka na opony pe�ne- solid-bieznik',1,1,'IBZ-15A_LM1/LM4')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.ISR65','Mieszanka na opony pe�ne -solid',1,1,'ISR65-03C_LM1/LM4')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.ITT','Mieszanka na kap� opon grupy I2',1,1,'ITT-02A_LM1/LM4')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.K','Mieszanka bie�nikowa',1,1,'ITT-02A_LM1/LM4')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.K55','mieszanka elastyczna twardos� 55 Sha',1,1,'K55-11-LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.K60','mieszanka elastyczna twardos� 60 Sha',1,1,'K60-08_BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.K65','Mieszanka elastyczna 65ShA',1,1,'K65-00_BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.KL50','Mieszanka klejowa KL 50',1,1,'KL50-13C_M1/BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.KM50','Mieszanka na kap�',1,1,'KM50-09_BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.KNR55','Mieszanka do budowy miech�w resor�w pneumatycznych-do kalandrowania.',1,1,'KNR55-10A_LM2/LM4')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.KOR75','Mieszanka bie�nikowa',1,1,'KOR75-04_GTS')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.KOR80','Mieszanka-wyroby formowe-stopka',1,1,'KOR80-01_GTS50')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.LM25','Mieszanka o twardo�ci ok 25 ShA NBR/CR',1,1,'LM25-07_BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.LM55','MIESZANKA TRUDNO�CIERALNA',1,1,'LM55-19-LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.LM65','Mieszanka trudno�cieralna o twardo�ci 65 ShA',1,1,'LM65-08_BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.LM9','Mieszanka EPDM sieciowana nadtlenkami',1,1,'LM9-04_BUZ/Walcarka')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.LM9-PR01-kalkulacja','Mieszanka EPDM sieciowana nadtlenkami',1,1,'LM9-PR01-kalkulacja')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.LM9-PR02-kalkulacja','Mieszanka EPDM sieciowana nadtlenkami_copy',1,1,'LM9-PR02-kalkulacja')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.MCZ','Mieszanka czyszcz�ca MCZ',1,1,'01-LM1')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.MGS50','Guma strzykowa',1,1,'MGS50-08-LM1/LM4')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.ML65','Mieszanka na lemiesz',1,1,'ML65-17-LM5')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.MOB65','Mieszanka na mat� oborow�',1,1,'MOB65-35_GTS')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.MS55','Mieszanka na strzyki gumowe',1,1,'MS55-08_BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.MTC','Mieszanka trudnopalna cobra',1,1,'PR01')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.MU60','Mieszanka na uszczelki SBR',1,1,'MU 60-04')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.NBO70','NBO70 - kalkulacja dla opon bez WWA',1,1,'NBO 70-01')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.NK50','mieszanka NR o twardo�ci 50 Sha',1,1,'NK50-11_BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.NK60','Mieszanka NR o twardo�ci 60 ShA',1,1,'NK60-05_M2/BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.NK70','Mieszanka o twardo�ci 70ShA- �ozyska mostowe',1,1,'NK70-03_BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.NR70','mieszanka NR o twardo�ci 70 ShA',1,1,'NR70-01A_ LM1/LM1/LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.NR80','mieszanka NR o twardo�ci 80',1,1,'NR 80-01')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.NU60','mieszanka NR 60 na profile',1,1,'NU60-06-LM2/LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.OBL65','Mieszanka NR/BR 65 na obudowy lamp samochodowych',1,1,'OBL65_PR08_M1/BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.OL60','Mieszanka olejoodporna o twardo�ci 60',1,1,'OL60-16-LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.OL70','Mieszanka olejoodporna OL 70',1,1,'OL70-39_LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.OL80','Mieszanka olejoodporna OL 80',1,1,'OL80-13-LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.OLP60','Mieszanka na uszczelki knott ',1,1,'PR17')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.OLP70','mieszanka na profile NBR  70 Sha',1,1,'OLP60-11-LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.OS70','Mieszanka na ochraniacz samochodowy',1,1,'OS70-110_LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.OSB70','Mieszanka na ochraniacz samochodowy - BIZON',1,1,'OSB70-03_LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.OST','Mieszanka ochronna dla stopki',1,1,'OST-02-LM2/LM4')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.P40','mieszanka trudno�cieralna 40 ShA',1,1,'P40-PR01-LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.PB55','Mieszanka na podk�ad bie�nika',1,1,'PB55-12_BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.PBK60','Mieszanka podk�adowa pod bie�nik opon Kabat',1,1,'2')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.PO50','mieszanka przeznaczenia og�lnego',1,1,'PO50-09_M1')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.PO60','MIESZANKA PRZEZNACZENIE OG�LNE',1,1,'PO60-24_LM1/LM4')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.PO75','mieszanka og�lnego pezeznaczenia',1,1,'PO75-15_BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.PO85','MIESZANKA GUMOWA PO85',1,1,'PO85-20_M1/BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.PUCBUTYL','mieszanka czyszcz�ca po butylu I',1,1,'mieszanka czyszcz�ca po butylu')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.PUCGUMA','mieszanka czyszcz�ca',1,1,'mieszanka czyszcz�ca')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.QTAN6090','MIeszanka Contitech W�gry',1,1,'0')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.QTDZ6090','MIeszanka Contitech W�gry',1,1,'0')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.RGK55','Mieszanka na gumowanie kordu dla firmy ROLGUM',1,1,'RGK55-01_M2/M1')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.SB40','Mieszanka gumowa SB 40',1,1,'SB40-06_BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.SBR40','Mieszanka SBR 40 ShA',1,1,'SBR40-PR01_M1/M1')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.SIDE62','Mieszanka SIDE62',1,1,'SIDE62-KAB10_LM1')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.SIDE74','Mieszanka SIDE74',1,1,'SIDE74-KAB5_M2')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.SL4/922','Mieszanka do firmy Heidenau',1,1,'SL4/922-01B-LM1/LM4')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.SLHC','Mieszanka na d�tki Heavy Cross',1,1,'SLHC-01_M2/BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.SPU60','mieszanka na profille twardo�� 60',1,1,'SPU60-13-LM2/LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.SPU70','mieszanka na profile twardo�� 70',1,1,'SPU70-25A-LM2/LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.ST','Mieszanka-EPDM Stagum Eko',1,1,'ST-06_BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.SZ60','Mieszanka na szcz�ki do styk�wek',1,1,'SZ60-03_LM3/LM5')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.TS70','Mieszanka do wzorcowania urz�dze� laboratoryjnych',1,1,'TS70-03_GTS')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.TTPO70','MIeszanka na odbojniki miech�w 70 ShA',1,1,'TTPO70-02_M1/M1/BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.TTPO80','Mieszanka na odbojniki wewn�trzne miech�w resor�w',1,1,'-')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.U1','MIESZANKA NR/BR',1,1,'U1-04_BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.UCR','Uplastycznianie kauczuku chloroprenowego',1,1,'UCR02_WAL')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.UNTD64','Mieszanka UNTD64',1,1,'UNTD64-KAB1-LM2')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.UOL50','mieszanka NBR o twardo�ci 50 ShA',1,1,'UOL50-15-LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.UOL60','mieszanka NBR 60ShA',1,1,'UOL60-12-LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.UOL85','mieszanka NBR 85 ShA',1,1,'UOL85-10-LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.URSS','uplastycznianie kauczuku RSS',1,1,'URSS-01_BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.UTSR','Uplastycznianie kauczuku TSR-10',1,1,'UTSR-05_BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.WC55','Mieszanka na maty a�urowe WC 55',1,1,'WC 55-22')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.WDK50','Mieszanka na d�tki do dekatyzacji',1,1,'1')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.WNR5801','Mieszanka do budowy miech�w resor�w pneumatycznych',1,1,'WNR5801-04-LM1/LM4')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.WOD50','mieszanka do kontaktu z wod� pitn� 50ShA',1,1,'PR07')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.WPT60','Mieszanka na wype�niacz nadrut�wkowy 55-65 ShA Tiptopol',1,1,'WPT60-01-LM1/LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.WPT70','Mieszanka na wype�niacz naddrut�wkowy',1,1,'WPT70-01_M1/BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.XB','Przedmieszka bie�nikowa do K i BOB',1,1,'XB-36-LM1')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.XBIN','Przedmieszka do mieszanki BIN',1,1,'XBIN-02_M1')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.XBKR ','Przedmieszka do mieszanki BKR65',1,1,'XBKR-03A-LM1')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.XBOH65','Przedmieszka do mieszanki BOH65',1,1,'XBOH65-PR01-LM2')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.XGK55','Przedmieszka do mieszanki GK 55',1,1,'XGK55-05B_M2')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.XK1','Przedmieszka do mieszanek zakupowych',1,1,'MIE.XK1-12-LM1-BRI009-KAB682')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.XK2','Przedmieszka do mieszanki recyklingowej BSE65',1,1,'MIE.XK3-01-LM1')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.XK3','Mieszanka czyszcz�ca instalacj� wagi olejowej',1,1,'MIE.XK3-01-LM1')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.XK4','Przedmieszka czyszcz�ca instalacj� wagi sadzowej',1,1,'MIE.XK4-01-LM2-BRI009-KAB682')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.XNBO70','Przedmieszka do mieszanki NBO70 - kalkulacja opony bez WWA',1,1,'XNBO-01-LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.XNBR','Przedmieszka kauczukowo-sadzowa',1,1,'XNBR-01-LM1')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.XNU60','Przedmieszka z kauczuk�w do mieszanki NU60',1,1,'XNU60-05_BUZ')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.XPB','Przedmieszka do mieszanki PB55',1,1,'XPB-00_ Buzuluk')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.XPBK','przedmieszka do mieszanki podk�adowej',1,1,'XPBK-02_M1')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.XS','Przedmieszka na wyroby formowe ',1,1,'XS-49-LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.XS','Przedmieszka na wyroby formowe ',1,2,'XS-49-LM5')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.XZEH','Przedmieszka do mieszanki ZEH',1,1,'XZEH-25-LM1')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.ZA60','Mieszanka do gumowania zawor�w ZA 60',1,1,'ZA60-10_LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.ZB80','Mieszanka o twardo�ci 80 ShA',1,1,'ZB80-09-LM1/LM3')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.ZBR55','Mieszanka do budowy miech�w resor�w pneumatycznych',1,1,'ZBR55-09-LM1/LM4')
INSERT INTO @tmp (RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName) VALUES ('MIE.ZEH65','Mieszanka trudno�cieralna 68 Sh`A',1,1,'ZEH65-21_M1/BUZ')


declare c cursor fast_forward for
select id,RecipeNumber,RecipeName,VersionNumber,AlternativeNo,VersionName from @tmp

open c;
fetch next from c into @id,@RecipeNumber,@RecipeName,@VersionNumber,@AlternativeNo,@VersionName;

   while @@fetch_status = 0
   begin
    --
	SET @RecipeId = (SELECT Id from dbo.Recipes WHERE RecipeNumber = @RecipeNumber)
	--
	if( select count(*) from dbo.RecipeVersions WHERE RecipeId = @RecipeId and VersionNumber = @VersionNumber and AlternativeNo = @AlternativeNo ) =  0
	BEGIN
		print @RecipeNumber;
		insert into dbo.RecipeVersions (RecipeId, VersionNumber, AlternativeNo, Name, IsActive, DefaultVersion, IsAccepted01, IsAccepted02) values (@RecipeId,@VersionNumber,@AlternativeNo, @VersionName, 1, 1, 0, 0)
	END

	-- UPDATE r
	

	--
   	fetch next from c into  @id,@RecipeNumber,@RecipeName,@VersionNumber,@AlternativeNo,@VersionName;
   end

   close c;
   deallocate c;