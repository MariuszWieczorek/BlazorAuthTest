using MediatR;

namespace MwTech.Application.SettingCategories.Commands.DeleteSettingCategory;

public class DeleteSettingCategoryCommand : IRequest
{
    public int Id { get; set; }
}
