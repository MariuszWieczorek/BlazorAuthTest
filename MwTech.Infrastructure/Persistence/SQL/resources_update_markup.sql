SELECT Id, Name, ResourceNumber, Cost, Markup
-- update r SET Markup = 170
  FROM [MwTech].[dbo].[Resources] as r
  where cost != 0
  and [ResourceNumber] in 
  ('PC.PD.WYT'
,'PC.PDKON55'
,'PC.PDKON75'
,'PC.PDKON45'
,'PC.PDWUL45'
,'PC.PDWUL55'
,'PC.PDWUL75'
,'PC.PD.KJ'
,'KP.MA.PAK'
,'PC.PD.MAL')
