using MediatR;
using MwTech.Application.MachineCategories.Commands.EditMachineCategory;
using MwTech.Domain.Entities;

namespace MwTech.Application.MachineCategories.Queries.GetEditMachineCategoryViewModel;

public class GetEditMachineCategoryViewModelQuery : IRequest<EditMachineCategoryViewModel> 
{
    public int Id { get; set; }
}
