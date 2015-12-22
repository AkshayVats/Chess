using System.Collections.Generic;

namespace Chess.Pieces
{
    class Knight: ChessPieceRaw
    {
        public override string GetIcon()
        {
            return GetIconPrefix() + "knight.png";
        }

        public override IEnumerable<GridCell> PossibleMoves()
        {
            for (int i = 0; i <= 1; i++)
                for (int j = 0; j <= 1; j++)
                {
                    int s1 = i == 0 ? -1 : 1;
                    int s2 = j == 0 ? -1 : 1;
                    var tmp = new GridCell(2 * s1 + Cell.Row, s2 + Cell.Column);
                    if(IsValid(tmp)!=false)
                        yield return tmp;
                    tmp = new GridCell(s1 + Cell.Row, 2 * s2 + Cell.Column);
                    if (IsValid(tmp) != false)
                        yield return tmp;
                }
        }

        public Knight(Team team, ChessPiece[,] board) : base(team, board)
        {
        }
    }
}
