using System.ComponentModel;

namespace Homuai.App.ValueObjects.Enum
{
    public enum ProductEnum
    {
        [Description("TITLE_UNITY")]
        Unity = 0,
        [Description("TITLE_BOX")]
        Box = 1,
        [Description("TITLE_PACKAGE")]
        Package = 2,
        [Description("TITLE_KILOGRAM")]
        Kilogram = 3
    }
}
