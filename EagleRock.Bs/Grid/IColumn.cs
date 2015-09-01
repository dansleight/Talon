using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleRock.Bs.Grid
{
    public interface IColumn
    {
        ColumnResponsiveAction? ResponsiveAction { get; set; }

        ColumnResponsiveSize? ResponsiveSize { get; set; }

        string Title { get; set; }

        string Format { get; set; }

        bool Hidden { get; set; }

        int Width { get; set; }

        bool Sortable { get; set; }

        string ClassName { get; set; }

        bool Searchable { get; set; }

        int SortOrder { get; set; }

        SortDir SortDirection { get; set; }

        string Render { get; set; }
    }
}
