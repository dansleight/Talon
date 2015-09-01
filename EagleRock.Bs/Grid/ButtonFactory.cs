using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleRock.Bs.Grid
{
    public class ButtonFactory<TModel> where TModel : class
    {
        #region Properties

        public ButtonColumn<TModel> Container { get; private set; }

        #endregion

        #region Constructor

        public ButtonFactory(ButtonColumn<TModel> container)
        {
            Container = container;
        }

        #endregion

        #region Object Methods

        public virtual ColumnButtonBuilder Button(string method)
        {
            ColumnButton button = new ColumnButton(method);
            this.Container.Buttons.Add(button);
            return new ColumnButtonBuilder(button);
        }

        #endregion
    }
}
