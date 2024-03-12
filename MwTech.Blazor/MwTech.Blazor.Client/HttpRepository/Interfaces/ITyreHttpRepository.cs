using MwTech.Shared.Tyres.Tyres.Commands.AddTyre;
using MwTech.Shared.Tyres.Tyres.Commands.EditTyre;
using MwTech.Shared.Tyres.Tyres.Models;
using MwTech.Shared.Tyres.Tyres.Queries.GetTyres;

namespace MwTech.Blazor.Client.HttpRepository.Interfaces;

public interface ITyreHttpRepository
{
    Task<TyresViewModel> GetAll();
    Task<TyresViewModel> GetFiltered(TyreFilter filter, int pageNumber);
    Task Add(AddTyreCommand command);
    Task Edit(EditTyreCommand command);
    Task<EditTyreCommand> GetEdit(int Id);
    Task Delete(int id);
   
}

