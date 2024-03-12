using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities.Comarch;

public class ComarchBom
{
    public string kod_towaru { get; set; }
    public int id_towaru { get; set; }
    public string nazwa_towaru { get; set; }
    public int id_receptury { get; set; }
    public string symbol_receptury { get; set; }
    public decimal ilosc_ewidencyjna { get; set; }
    public string jm { get; set; }
    public decimal koszt_ewidencyjny { get; set; }
    public short receptura_domyslna { get; set; }
    public int id_skladnika { get; set; }
    public string kod_skladnika { get; set; }
    public string nazwa_skladnika { get; set; }
    public decimal ilosc_skladnika { get; set; }
    public string jm_skladnika { get; set; }
    public decimal waga_brutto { get; set; }
    public decimal waga_netto { get; set; }
    public string czas_mieszania { get; set; }
    public string czas_filtrowania_1kg { get; set; }
    public string db_name { get; set; }
    public string server_name { get; set; }

}

