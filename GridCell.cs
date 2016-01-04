using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    struct GridCell
    {
        public static readonly GridCell NullCell = new GridCell(8, 8);
        public int Row { get; set; }
        public int Column { get; set; }

        public GridCell(int row, int col)
        {
            Row = row;
            Column = col;
        }
        public static bool operator ==(GridCell a, GridCell b)
        {
            return a.Column == b.Column && a.Row == b.Row;
        }
        public static bool operator !=(GridCell a, GridCell b)
        {
            return a.Column != b.Column || a.Row != b.Row;
        }
        public override bool Equals(object obj)
        {
            if (obj is GridCell) return this == (GridCell)obj;
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return Row.GetHashCode() * 2003 + Column.GetHashCode();
            }
        }
    }
}
