using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebWallet.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly List<SelectListItem> _periods = new List<SelectListItem>
            {
                new SelectListItem { Value = "7 дни", Text = "7 дни" },
                new SelectListItem { Value = "14 дни", Text = "14 дни" },
                new SelectListItem { Value = "1 месец", Text = "1 месец"  },
                new SelectListItem { Value = "3 месеца", Text = "3 месеца" },
                new SelectListItem { Value = "6 месеца", Text = "6 месеца"  },
                new SelectListItem { Value = "1 година", Text = "1 година"  }
            };

        protected List<SelectListItem> TransactionCategories
        {
            get
            {
                var foodAndDrinks = new SelectListGroup { Name = "Храна и Напитки" };
                var shopping = new SelectListGroup { Name = "Шопинг" };
                var housing = new SelectListGroup { Name = "Жилище" };
                var transportation = new SelectListGroup { Name = "Транспорт" };
                var auto = new SelectListGroup { Name = "Автомобил" };
                var lifeAndEntertainment = new SelectListGroup { Name = "Живот и Забавление" };
                var pcAndCommunication = new SelectListGroup { Name = "Компютри и комумикация" };
                var ficancialExpences = new SelectListGroup { Name = "Финансови разходи" };
                var investment = new SelectListGroup { Name = "Инвестиции" };
                var income = new SelectListGroup { Name = "Приход" };

                return new List<SelectListItem>
                    {
                         new SelectListItem { Value = "Кафе/Бар", Text = "Кафе/Бар", Group = foodAndDrinks },
                         new SelectListItem { Value = "Ресторант", Text = "Ресторант", Group = foodAndDrinks },
                         new SelectListItem { Value = "Хранителни продукти", Text = "Хранителни продукти", Group = foodAndDrinks },
                         new SelectListItem { Value = "Храни и напитки друго", Text = "Храни и напитки друго", Group = foodAndDrinks },
                         new SelectListItem { Value = "Дрехи и обувки", Text = "Дрехи и обувки", Group = shopping },
                         new SelectListItem { Value = "Електроника", Text = "Електроника", Group = shopping },
                         new SelectListItem { Value = "Свободно време и подаръци", Text = "Свободно време и подаръци", Group = shopping },
                         new SelectListItem { Value = "Шопинг друго", Text = "Шопинг друго", Group = shopping },
                         new SelectListItem { Value = "Дом и градина", Text = "Дом и градина", Group = shopping },
                         new SelectListItem { Value = "Красота и здраве", Text = "Красота и здраве", Group = shopping },
                         new SelectListItem { Value = "Бижута и аксесоари", Text = "Бижута и аксесоари", Group = shopping },
                         new SelectListItem { Value = "Деца", Text = "Деца", Group = shopping },
                         new SelectListItem { Value = "Домашни любимци", Text = "Домашни любимци", Group = shopping },
                         new SelectListItem { Value = "Инструменти", Text = "Инструменти", Group = shopping },
                         new SelectListItem { Value = "Битови сметки", Text = "Битови сметки", Group = housing },
                         new SelectListItem { Value = "Ремонт и подръжка", Text = "Ремонт и подръжка", Group = housing },
                         new SelectListItem { Value = "Вноска по жилищен заем", Text = "Вноска по жилищен заем", Group = housing },
                         new SelectListItem { Value = "Застраховка на жилище и имущество", Text = "Застраховка на жилище и имущество", Group = housing },
                         new SelectListItem { Value = "Наем", Text = "Наем", Group = housing },
                         new SelectListItem { Value = "Жилище друго", Text = "Жилище друго", Group = housing },
                         new SelectListItem { Value = "Командировки", Text = "Командировки", Group = transportation },
                         new SelectListItem { Value = "Градски транспорт", Text = "Градски транспорт", Group = transportation },
                         new SelectListItem { Value = "Такси", Text = "Такси", Group = transportation },
                         new SelectListItem { Value = "Самолетен билет", Text = "Самолетен билет", Group = transportation },
                         new SelectListItem { Value = "Транспорт друго", Text = "Транспорт друго", Group = transportation },
                         new SelectListItem { Value = "Гориво", Text = "Гориво", Group = auto },
                         new SelectListItem { Value = "Лизинг", Text = "Лизинг", Group = auto },
                         new SelectListItem { Value = "Паркинг", Text = "Паркинг", Group = auto },
                         new SelectListItem { Value = "Автомобилна застраховка", Text = "Автомобилна застраховка", Group = auto },
                         new SelectListItem { Value = "Наем на автомобил", Text = "Наем на автомобил", Group = auto },
                         new SelectListItem { Value = "Ремонт и подръжка", Text = "Ремонт и подръжка", Group = auto },
                         new SelectListItem { Value = "Автомобил друго", Text = "Автомобил друго", Group = auto },
                         new SelectListItem { Value = "Спорт и фитнес", Text = "Спорт и фитнес", Group = lifeAndEntertainment },
                         new SelectListItem { Value = "Алкохол и цигари", Text = "Алкохол и цигари", Group = lifeAndEntertainment },
                         new SelectListItem { Value = "Книги", Text = "Книги", Group = lifeAndEntertainment },
                         new SelectListItem { Value = "Музика", Text = "Музика", Group = lifeAndEntertainment },
                         new SelectListItem { Value = "Кино и филми", Text = "Кино и филми", Group = lifeAndEntertainment },
                         new SelectListItem { Value = "Игри", Text = "Игри", Group = lifeAndEntertainment },
                         new SelectListItem { Value = "Култирни и спортни мероприатия", Text = "Култирни и спортни мероприатия", Group = lifeAndEntertainment },
                         new SelectListItem { Value = "Концерти", Text = "Концерти", Group = lifeAndEntertainment },
                         new SelectListItem { Value = "Ваканция и хотели", Text = "Ваканция и хотели", Group = lifeAndEntertainment },
                         new SelectListItem { Value = "Лотария и хазарт", Text = "Лотария и хазарт", Group = lifeAndEntertainment },
                         new SelectListItem { Value = "Телевизия", Text = "Телевизия", Group = lifeAndEntertainment },
                         new SelectListItem { Value = "Живот и забавление друго", Text = "Живот и забавление друго", Group = lifeAndEntertainment },
                         new SelectListItem { Value = "Интернет", Text = "Интернет", Group = pcAndCommunication },
                         new SelectListItem { Value = "Телефон", Text = "Телефон", Group = pcAndCommunication },
                         new SelectListItem { Value = "Мобилен телефон", Text = "Мобилен телефон", Group = pcAndCommunication },
                         new SelectListItem { Value = "Пощенси и куриерски услуги", Text = "Пощенси и куриерски услуги", Group = pcAndCommunication },
                         new SelectListItem { Value = "Софруер и приложения", Text = "Софруер и приложения", Group = pcAndCommunication },
                         new SelectListItem { Value = "Компютри и комуникация друго", Text = "Компютри и комуникация друго", Group = pcAndCommunication },
                         new SelectListItem { Value = "Глоби и лихви", Text = "Глоби и лихви", Group = ficancialExpences },
                         new SelectListItem { Value = "Фишове и актове", Text = "Фишове и актове", Group = ficancialExpences },
                         new SelectListItem { Value = "Детска издръжка", Text = "Детска издръжка", Group = ficancialExpences },
                         new SelectListItem { Value = "Застраховки", Text = "Застраховки", Group = ficancialExpences },
                         new SelectListItem { Value = "Заеми", Text = "Заеми", Group = ficancialExpences },
                         new SelectListItem { Value = "Данъци", Text = "Данъци", Group = ficancialExpences },
                         new SelectListItem { Value = "Финансови разходи друго", Text = "Финансови разходи друго", Group = ficancialExpences },
                         new SelectListItem { Value = "Колекции и антични предмете", Text = "Колекции и антични предмете", Group = investment },
                         new SelectListItem { Value = "Финансови инвестиции", Text = "Финансови инвестиции", Group = investment },
                         new SelectListItem { Value = "Спестовни сметки", Text = "Спестовни сметки", Group = investment },
                         new SelectListItem { Value = "Автомобили", Text = "Автомобили", Group = investment },
                         new SelectListItem { Value = "Валута", Text = "Валута", Group = investment },
                         new SelectListItem { Value = "Имот", Text = "Имот", Group = investment },
                         new SelectListItem { Value = "Инвестиции друго", Text = "Инвестиции друго", Group = investment },
                         new SelectListItem { Value = "Социална помощ", Text = "Социална помощ", Group = income },
                         new SelectListItem { Value = "Детска издръжка", Text = "Детска издръжка", Group = income },
                         new SelectListItem { Value = "Подаръци", Text = "Подаръци", Group = income },
                         new SelectListItem { Value = "Лихви и дивиденти", Text = "Лихви и дивиденти", Group = income },
                         new SelectListItem { Value = "Наеми и заеми", Text = "Наеми и заеми", Group = income },
                         new SelectListItem { Value = "Лотария и хазарт", Text = "Лотария и хазарт", Group = income },
                         new SelectListItem { Value = "Продажби", Text = "Продажби", Group = income },
                         new SelectListItem { Value = "Заплати", Text = "Заплати", Group = income }
                };
            }
        }

        protected void ThrowIfNull(object entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
        }

        protected void AddModelErrors(ModelStateDictionary modelState)
        {
            modelState
                .Values
                .SelectMany(x => x.Errors)
                .ToList()
                .ForEach(x => modelState.AddModelError("", x.ErrorMessage));
        }
    }
}