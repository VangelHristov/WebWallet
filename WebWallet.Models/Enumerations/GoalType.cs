using System.ComponentModel.DataAnnotations;

namespace WebWallet.Models.Enumerations
{
    public enum GoalType
    {
        [Display(Name = "Недвижим имот")]
        Home,

        [Display(Name = "Ремонт на жилище")]
        HomeImprovements,

        [Display(Name = "Автомобил")]
        Auto,

        [Display(Name = "Електроника")]
        Electornics,

        [Display(Name = "Дрехи")]
        Clothing,

        [Display(Name = "Пътуване")]
        Travel,

        [Display(Name = "Почивка")]
        Vacation,

        [Display(Name = "Пенсиониране")]
        Retirement,

        [Display(Name = "Здраве и Красота")]
        HealthBeauty,

        [Display(Name = "Друго")]
        Other
    }
}