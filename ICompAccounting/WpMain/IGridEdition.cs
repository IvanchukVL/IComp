using ICompAccounting.ModelView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ICompAccounting.WpMain
{
    public interface IGridEdition111
    {
        AppCommand NewRow { get; }
        AppCommand EditRow { get; }
        AppCommand OpenWindow { get; }
    }
}
