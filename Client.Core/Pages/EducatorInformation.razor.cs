using Microsoft.AspNetCore.Components;

namespace Client.Core.Pages;

public partial class EducatorInformation  : ComponentBase
{
    [Parameter]
    public required int Id { get; set; }
}