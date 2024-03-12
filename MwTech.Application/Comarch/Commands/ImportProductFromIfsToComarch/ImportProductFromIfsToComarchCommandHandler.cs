using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;


namespace MwTech.Application.Comarch.Commands.ImportProductFromIfsToComarch;

/*
public class ImportProductFromIfsToComarchCommandHandler : IRequestHandler<ImportProductFromIfsToComarchCommand>
{
    private readonly IOracleDbContext _oracleContext;
    private readonly IApplicationDbContext _context;
    private readonly IScadaIfsDbContext _scadaContext;

    public ImportProductFromIfsToComarchCommandHandler(IOracleDbContext oracleContext, IApplicationDbContext context, IScadaIfsDbContext scadaContext)
    {
        _oracleContext = oracleContext;
        _context = context;
        _scadaContext = scadaContext;
    }
    public async Task Handle(ImportProductFromIfsToComarchCommand request, CancellationToken cancellationToken)
    {

        IQueryable<IfsScadaMaterial> ifsMaterials = _oracleContext.IfsScadaMaterials
                   .FromSqlInterpolated(
                   @$"SELECT *
                   FROM IFSINFO.SCADA_MATERIALS")
                 .AsNoTracking()
                 .AsQueryable();



        var ifsMaterialsList = await ifsMaterials.ToListAsync();

        int sessionID = -1;
        int towarId = -1;

         ComarchTools.zaloguj(ref sessionID);
         int a1 =  ComarchTools.nowyProduct(sessionID, ref towarId, "XXXAXXX", "XXXAXXX", "szt.",1, "IFS");
         ComarchTools.wyloguj(ref sessionID);


        return;

    }

    
    public static partial class ComarchTools
    {

        public static int zaloguj(ref int SessionId)
        {
            var Login = new XLLoginInfo_20202
            {
                Wersja = 20202,
                ProgramID = "test",
                OpeIdent = "admin",
                OpeHaslo = "18Mx594",
                Baza = "KABAT_TEST"
                //Baza = "import"


            };

            cdn_api.cdn_api.XLLogin(Login, ref SessionId);


            return SessionId;
        }

        public static int wyloguj(ref int SessionId)
        {
            if (SessionId < 1)
            {
                return -1;
            }

            int retValue = cdn_api.cdn_api.XLLogout(SessionId);
            SessionId = -1;

            return retValue;
        }


        public static int nowyProduct(int SessionId, ref int TowarID, string kod, string nazwa, string jm, int typ, string grupa)
        {
            if (SessionId < 1)
            {
                return -1;
            }

            var Towar = new XLTowarInfo_20202
            {
                Wersja = 20202,
                Typ = typ,
                Kod = kod,
                Nazwa = nazwa,
                TwrGrupa = grupa,
                Jm = jm
            };

            var retValue = cdn_api.cdn_api.XLNowyTowar(SessionId, ref TowarID, Towar);

            if (retValue != 0)
            {
                switch (retValue)
                {
                    case 292:
                        //MessageBox.Show($"kod błędu {retValue.ToString()} - brak jednostki miary" );
                        break;
                    case 82:
                        // MessageBox.Show($"kod błędu {retValue.ToString()} - nie podano nazwy");
                        break;
                    case 83:
                        //   MessageBox.Show($"kod błędu {retValue.ToString()} - Jest już towar o takim kodzie");
                        break;
                    default:
                        // MessageBox.Show($"kod błędu {retValue.ToString()} ");
                        break;

                }

            }
            return retValue;
        }
    }
    

}
*/