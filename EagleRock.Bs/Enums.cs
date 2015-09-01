using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleRock.Bs
{
    public enum ColumnType
    {
        Bound,
        Custom,
        Render,
        Button
    }

    public enum ColumnResponsiveAction
    {
        Show,
        Hide
    }

    public enum ColumnResponsiveSize
    {
        xs,
        sm,
        md,
        lg
    }

    public enum SortDir
    {
        asc,
        desc
    }

    public enum PagingType
    {
        simple_numbers,
        simple,
        full,
        full_numbers
    }

    public enum PageSize
    {
        All
    }
}
