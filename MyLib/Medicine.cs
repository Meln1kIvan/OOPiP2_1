using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    /// <summary>
    /// Класс лекарств
    /// </summary>
    public class Medicine
    {
        /// <summary>
        /// Наименование лекарства
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Лекарственная форма
        /// </summary>
        public string Form { get; set; }
        /// <summary>
        /// Количество
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Единица измерения
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// Дозировка
        /// </summary>
        public string Dosage { get; set; }
        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Производитель
        /// </summary>
        public string Manufacturer { get; set; }
    }
}
