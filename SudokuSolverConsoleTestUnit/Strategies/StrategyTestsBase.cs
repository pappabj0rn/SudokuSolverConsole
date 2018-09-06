using System.Collections.Generic;
using Moq;
using SudokuSolverConsole;

namespace SudokuSolverConsoleTestUnit.Strategies
{
    public abstract class StrategyTestsBase
    {
        protected Mock<IPlayingField> FieldMock = new Mock<IPlayingField>();

        protected IPlayingField Field = new PlayingField("000000000" +
                                                         "000000000" +
                                                         "000000000" +
                                                         "000000000" +
                                                         "000000000" +
                                                         "000000000" +
                                                         "000000000" +
                                                         "000000000" +
                                                         "000000000");

        protected static readonly List<int> CountNine = new List<int>{0,1,2,3,4,5,6,7,8};

        protected StrategyTestsBase()
        {
            FieldMock.Setup(x => x.GetRow(It.IsAny<int>()))
                .Returns((int i) => Field.GetRow(i));

            FieldMock.Setup(x => x.GetRow(It.IsAny<Square>()))
                .Returns((Square s) => Field.GetRow(s));

            FieldMock.Setup(x => x.GetColumn(It.IsAny<int>()))
                .Returns((int i) => Field.GetColumn(i));

            FieldMock.Setup(x => x.GetColumn(It.IsAny<Square>()))
                .Returns((Square s) => Field.GetColumn(s));

            FieldMock.Setup(x => x.GetBigSquare(It.IsAny<int>()))
                .Returns((int i) => Field.GetBigSquare(i));

            FieldMock.Setup(x => x.GetBigSquare(It.IsAny<Square>()))
                .Returns((Square s) => Field.GetBigSquare(s));
        }
    }
}