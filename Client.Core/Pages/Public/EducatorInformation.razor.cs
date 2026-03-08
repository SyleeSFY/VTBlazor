using Microsoft.AspNetCore.Components;

namespace Client.Core.Pages.Public;

public partial class EducatorInformation  : ComponentBase
{
    [Parameter]
    public required int Id { get; set; }
}