using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace April.UserControls.Base
{
    public interface ICheckableControl
    {
        /// <summary>
        /// Проверять или нет компонент на то, что данные не пустые.
        /// </summary>
        bool EmptyDataCheck { get; set; }

        /// <summary>
        /// Текст, который будет выводиться пользователю, если данные не заполнены.
        /// </summary>
        string EmptyDataText { get; set; }

        /// <summary>
        /// Проверять на наличие данных в компоненте
        /// </summary>
        /// <returns></returns>
        bool Check();
    }
}
