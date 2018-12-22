using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebWallet.Models.Enumerations;

namespace WebWallet.ViewModels.Transaction
{
    public class TransactionVM
    {
        private readonly Dictionary<string, string[]> _categories = new Dictionary<string, string[]>
        {
            { "Храна и Напитки",
                new string[]
                {
                    "Кафе/Бар",
                    "Ресторант",
                    "Хранителни продукти",
                    "Храни и напитки друго"
                }
            },
            { "Шопинг",
                new string[]
                {
                    "Дрехи и обувки",
                    "Електроника",
                    "Свободно време и подаръци",
                    "Шопинг друго",
                    "Дом и градина",
                    "Красота и здраве",
                    "Бижута и аксесоари",
                    "Деца",
                    "Домашни любимци",
                    "Инструменти"
                }
            },
            { "Жилище",
                new string[]
                {
                    "Битови сметки",
                    "Ремонт и подръжка",
                    "Вноска по жилищен заем",
                    "Застраховка на жилище и имущество",
                    "Наем",
                    "Жилище друго"
                }
            },
            { "Транспорт",
                new string[]
                {
                    "Командировки",
                    "Градски транспорт",
                    "Такси",
                    "Самолетен билет",
                    "Транспорт друго"
                }
            },
            { "Автомобил",
                new string[]
                {
                    "Гориво",
                    "Лизинг",
                    "Паркинг",
                    "Автомобилна застраховка",
                    "Наем на автомобил",
                    "Ремонт и подръжка",
                    "Автомобил друго"
                }
            },
            { "Живот и Забавление",
                new string[]
                {
                    "Спорт и фитнес",
                    "Алкохол и цигари",
                    "Книги",
                    "Музика",
                    "Кино и филми",
                    "Игри",
                    "Култирни и спортни мероприатия",
                    "Концерти",
                    "Ваканция и хотели",
                    "Лотария и хазарт",
                    "Телевизия",
                    "Живот и забавление друго"
                }
            },
            { "Компютри и комумикация",
                new string[]
                {
                    "Интернет",
                    "Телефон",
                    "Мобилен телефон",
                    "Пощенси и куриерски услуги",
                    "Софруер и приложения",
                    "Компютри и комуникация друго"
                }
            },
            { "Финансови разходи",
                new string[]
                {
                    "Глоби и лихви",
                    "Фишове и актове",
                    "Детска издръжка",
                    "Застраховки",
                    "Заеми",
                    "Данъци",
                    "Финансови разходи друго"
                }
            },
            { "Инвестиции",
                new string[]
                {
                    "Колекции и антични предмете",
                    "Финансови инвестиции",
                    "Спестовни сметки",
                    "Автомобили",
                    "Валута",
                    "Имот",
                    "Инвестиции друго"
                }
            },
            { "Приход",
                new string[]
                {
                    "Социална помощ",
                    "Детска издръжка",
                    "Подаръци",
                    "Лихви и дивиденти",
                    "Наеми и заеми",
                    "Лотария и хазарт",
                    "Продажби",
                    "Заплати"
                }
            }
        };

        private string _category;

        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Име")]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        [Required]
        [Display(Name = "Сума")]
        [Range(0.1, maximum: (double)decimal.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Тип")]
        public TransactionType TransactionType { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Категория")]
        public string Category
        {
            get { return this._category; }
            set
            {
                if (!this.Categories.Values.Any(x => x.Contains(value)))
                {
                    throw new ArgumentException();
                }

                this._category = value;
            }
        }

        [Display(Name = "Забележка")]
        public string Note { get; set; }

        public string BudgetId { get; set; }

        public string GoalId { get; set; }

        public string InvestmentId { get; set; }

        public string RecurringPaymentId { get; set; }

        public string UserId { get; set; }

        public string AccountId { get; set; }

        public Dictionary<string, string[]> Categories => this._categories;
    }
}