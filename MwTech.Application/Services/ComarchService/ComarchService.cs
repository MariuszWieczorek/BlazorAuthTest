using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities.Comarch;

namespace MwTech.Application.Services.ComarchService;

public class ComarchService : IComarchService
{
    private readonly IApplicationDbContext _context;
    private readonly IComarchDbContext _comarch;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUser;
    private readonly ILogger<ComarchService> _logger;

    public ComarchService(IApplicationDbContext context,
        IComarchDbContext comarch,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUser,
        ILogger<ComarchService> logger
        )
    {
        _context = context;
        _comarch = comarch;
        _dateTimeService = dateTimeService;
        _currentUser = currentUser;
        _logger = logger;
    }

    public async Task<IEnumerable<ComarchTwrKarta>> ComarchTest()
    {
        var comarchTwrKarty = await _comarch.ComarchTwrKarty
                    .FromSqlInterpolated(
            @$"SELECT twr_kod
                   FROM cdn.twrkarty")
                    .ToListAsync();

        return comarchTwrKarty;
    }

    public async Task<IEnumerable<ComarchBom>> GetAllBoms()
    {
        var comarchBoms = await _comarch.ComarchBoms
                    .FromSqlInterpolated(
            @$"
                 SELECT[Kod towaru] as kod_towaru
                ,[idtowaru] as id_towaru
                ,[Nazwa towaru] as nazwa_towaru
                ,[idreceptury] as id_receptury
                ,[Symbol receptury] as symbol_receptury
                ,[Ilość ewidencyjna] as ilosc_ewidencyjna
                ,[j.m.] as jm
                ,[Koszt ewidencyjny] as koszt_ewidencyjny
                ,[Receptura domyślna] as receptura_domyslna
                ,[idskladnika] as id_skladnika
                ,[Kod składnika] as kod_skladnika
                ,[Nazwa składnika] as nazwa_skladnika
                ,[Ilość składnika] as ilosc_skladnika
                ,[j.m. składnika] as jm_skladnika
                ,[Waga brutto] as waga_brutto
                ,[Waga netto] as waga_netto
                ,[czas mieszania] as czas_mieszania
                ,[czas filtrowania 1kg] as czas_filtrowania_1kg
                ,db_name() as db_name
                ,@@SERVERNAME as server_name
                 FROM[dbo].[PM_KABAT_Rec_i_elem]
                 WHERE 1 = 1
                 AND[Receptura domyślna] = 1")
                .ToListAsync();

        return comarchBoms;
    }

    public IEnumerable<ComarchLoopedIndex> GetLoopedIndexes()
    {
        List<ComarchLoopedIndex> loopedIndexes = new List<ComarchLoopedIndex>
            {
            new ComarchLoopedIndex
            {
                LoopedIndex   = "MIE.BKR65_2_F_NAWRÓT",
                NoLoopedIndex = "MIE.BKR65_2_F"
            },
            new ComarchLoopedIndex
            {
                LoopedIndex   = "MIE.BMO65_2_F_NAWRÓT",
                NoLoopedIndex = "MIE.BMO65_2_F"
            },
            new ComarchLoopedIndex
            {
                LoopedIndex   = "MIE.DS55_2-SCINKIPRZEFILTROWANE",
                NoLoopedIndex = "MIE.DS55_2_F"
            },
            new ComarchLoopedIndex
            {
                LoopedIndex   = "MIE.MGS50_NAWRÓT",
                NoLoopedIndex = "MIE.MGS50_2_F"
            },
            new ComarchLoopedIndex
            {
                LoopedIndex   = "MIE.SIDE62_NAWRÓT",
                NoLoopedIndex = "MIE.SIDE62_1"
            },

            new ComarchLoopedIndex
            {
                LoopedIndex   = "MIE.SIDE74_NAWRÓT",
                NoLoopedIndex = "MIE.SIDE74_1"
            },

            new ComarchLoopedIndex
            {
                LoopedIndex   = "MIE.BBR50 NAWRÓT",
                NoLoopedIndex = "MIE.BBR50_2_F"
            },

            new ComarchLoopedIndex
            {
                LoopedIndex   = "MIE.BUTYL65_1KAPSLE",
                NoLoopedIndex = "MIE.BUTYL65_2_F"
            },
            new ComarchLoopedIndex
            {
                LoopedIndex   = "MIE.GK55_2_F_NAWRÓT",
                NoLoopedIndex = "MIE.GK55_2_F"
            },
            new ComarchLoopedIndex
            {
                LoopedIndex   = "MIE.OST_2_F_NAWRÓT",
                NoLoopedIndex = "MIE.OST_2_F"
            },
            new ComarchLoopedIndex
            {
                LoopedIndex   = "MIE.WPK85_1_F_NAWRÓT",
                NoLoopedIndex = "MIE.WPK85_1_F"
            },
            new ComarchLoopedIndex
            {
                LoopedIndex   = "MIE.SL4/922_2_F_NAWRÓT",
                NoLoopedIndex = "MIE.SL4/922_2_F"
            }

            };

        return loopedIndexes;
    }

    public async Task<IEnumerable<ComarchTwrCost>> GetTwrCost()
    {
        var comarchTwrCost = await _comarch.ComarchTwrCost
            .FromSqlInterpolated(
            @$"
              SELECT 
                 c.ProductNumber
                ,c.Cena as Cost
                ,c.Document  
                FROM [KABAT].[dbo].[KABAT_PM_dost_ceny] AS c
                INNER JOIN (
                SELECT ProductNumber, MAX(Date) as maxdate, MAX(Dst_GidNumer) as maxgidnumer
                FROM [KABAT].[dbo].[KABAT_PM_dost_ceny]
                GROUP BY ProductNumber ) AS x
                ON c.ProductNumber = x.ProductNumber 
                AND c.Date = x.maxdate
                AND c.Dst_GidNumer = x.maxgidnumer
              ")
            .ToListAsync();

        return comarchTwrCost;
    }
}
