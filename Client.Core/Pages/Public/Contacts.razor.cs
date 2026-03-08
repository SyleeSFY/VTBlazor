using Client.Core.Entities.Models;
using Client.Core.Entities.Models.User.Dicipline;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Http.Json;

namespace Client.Core.Pages.Public;

public partial class Contacts : ComponentBase
{
    List<Discipline> disciplines = new List<Discipline>()
    {
         new Discipline{Id = 1,NameDiscipline = "Математика",Course = 1, Group = new TrainedGroup{isAS = true} }
    };
}