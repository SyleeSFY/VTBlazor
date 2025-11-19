using System.ComponentModel;

namespace Client.Core.Shared.Enums.HomePage;

public enum JustifyContentEnum
{
    [Description("flex-start")]
    FlexStart,
    [Description("flex-end")]
    FlexEnd,
    [Description("center")]
    Center,
    [Description("space-between")]
    SpaceBetween,
    [Description("space-around")]
    SpaceAround,
    [Description("space-evenly")]
    SpaceEvenly
}

public static class JustifyContentEnumExtensions
{
    public static string ToCssValue(this JustifyContentEnum value)
    {
        return value switch
        {
            JustifyContentEnum.FlexStart => "flex-start",
            JustifyContentEnum.FlexEnd => "flex-end",
            JustifyContentEnum.Center => "center",
            JustifyContentEnum.SpaceBetween => "space-between",
            JustifyContentEnum.SpaceAround => "space-around",
            JustifyContentEnum.SpaceEvenly => "space-evenly",
            _ => "flex-start"
        };
    }
}
