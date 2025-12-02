using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Client.Core.Widgets.HomeWidgets.HistoryOfCafedra
{
    public partial class HistoryTimeLine : ComponentBase
    {
        private List<string> HistoryList { get; set; } = new List<string>()
        {
            $"В 1961 году был осуществлен первый набор студентов\n на специальность \"Автоматика и телемеханика",
            $"Приказом №144 от 6 июня 1977 года проректора МЭИ\n по Смоленскому филиалу В.Ф.Фёдорова\n была организована кафедра вычислительной техники",
            $"Кафедра компьютерных технологий и управления\n присоединилась к кафедре\n вычислительной техники в 2002 году.",
            $" 2024 года кафедрой руководит\n Борисов Вадим Владимирович.",
        };
    }
}