using System.ComponentModel.DataAnnotations;

namespace DotNetCore.Data.Enums
{
    public enum Gender
    {
        [Display(Name = "Unknown")]
        Unknown = 0,

        [Display(Name = "Male")]
        Male = 1,

        [Display(Name = "Female")]
        Female = 2,

        [Display(Name = "N/A")]
        NotAvailable = 9
    }
}