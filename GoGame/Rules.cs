namespace GoGame
{
    class Rules
    {
        public Board Board;

        public Rules(Board board)
        {
            Board = board;
        }

        public void CheckStoneArroundPositionAndRemoveIfNeeded(int x, int y)
        {
            RemoveStoneIfSurrounded(x, y - 1);
            RemoveStoneIfSurrounded(x, y + 1);
            RemoveStoneIfSurrounded(x - 1, y);
            RemoveStoneIfSurrounded(x + 1, y);
        }

        private void RemoveStoneIfSurrounded(int x, int y)
        {
            var oppositeColor = GetOppositeColor(x, y);

            var almostSurrounded = IsAlmostFullySurrounded(x, y, oppositeColor);

            var fullySurrounded = IsFullySurroundedBy(x, y, oppositeColor);
            if (almostSurrounded)
            {
                Board.MakePositionEmpty(x, y);
                Board.MakePositionEmpty(x+1, y);
            }
            if (fullySurrounded)
            {
                Board.MakePositionEmpty(x, y);
            }
        }

        private bool IsAlmostFullySurrounded(int x, int y, StoneColor oppositeColor)
        {
            if (Board.GetStoneColor(x,y) == StoneColor.None)
            {
                return false;
            }
            var EDGE = 2;
            bool surroundedOnLeft = (x < EDGE || Board.GetStoneColor(x - 1, y) == oppositeColor);
            bool surroundedOnRight = (x > Board.BoardSize - EDGE || Board.GetStoneColor(x + 1, y) == Board.GetStoneColor(x,y));
            bool surroundedOnBottom = (y > Board.BoardSize - EDGE || Board.GetStoneColor(x, y + 1) == oppositeColor);
            bool surroundedOnTop = (y < EDGE || Board.GetStoneColor(x, y - 1) == oppositeColor);


            bool fullySurrounded = surroundedOnLeft && surroundedOnRight && surroundedOnBottom && surroundedOnTop;

            return fullySurrounded;
        }

        private StoneColor GetOppositeColor(int x, int y)
        {
            return Board.GetStoneColor(x, y) == StoneColor.White ? StoneColor.Black : StoneColor.White;
        }

        public bool IsFullySurroundedBy(int x, int y, StoneColor surroundingStoneColor)
        {
            var EDGE = 2;
            bool surroundedOnLeft = (x <EDGE || Board.GetStoneColor(x-1, y) == surroundingStoneColor);
            bool surroundedOnRight = (x > Board.BoardSize - EDGE || Board.GetStoneColor(x + 1, y) == surroundingStoneColor);
            bool surroundedOnBottom = (y > Board.BoardSize - EDGE || Board.GetStoneColor(x, y + 1) == surroundingStoneColor);
            bool surroundedOnTop = (y < EDGE || Board.GetStoneColor(x, y - 1)== surroundingStoneColor);


            bool fullySurrounded = surroundedOnLeft && surroundedOnRight && surroundedOnBottom && surroundedOnTop;

            return fullySurrounded;
        }

        public StoneColor GetWinner()
        {
            var result = new WinnerByCountStrategy(Board).GetWinner();
            if (result != StoneColor.None)
            {
                return result;
            }
            return new KomiWinnerStrategy(Board).GetWinner();
        }

        public void NotifyStoneAdded(StoneColor stoneColor, int x, int y)
        {
           
        }
    }
}