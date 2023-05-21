using System.ComponentModel;

namespace Utils.CrossCuttingConcerns.Enums
{
    public enum Sex
    {
        [Description("Male")]
        Male = 1,

        [Description("Female")]
        Female,

        [Description("Other")]
        Other
    }
}
