namespace Chess.Pieces
{
    class Knight: ChessPieceRaw
    {
        public override string GetIcon()
        {
            return GetIconPrefix() + "knight.png";
        }

        public Knight(Team team) : base(team)
        {
        }
    }
}
