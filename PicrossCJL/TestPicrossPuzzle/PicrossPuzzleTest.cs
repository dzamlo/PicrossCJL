using PicrossCJL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

namespace TestPicrossPuzzle
{
    
    
    /// <summary>
    ///Classe de test pour PicrossPuzzleTest, destinée à contenir tous
    ///les tests unitaires PicrossPuzzleTest
    ///</summary>
    [TestClass()]
    public class PicrossPuzzleTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Obtient ou définit le contexte de test qui fournit
        ///des informations sur la série de tests active ainsi que ses fonctionnalités.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Attributs de tests supplémentaires
        // 
        //Vous pouvez utiliser les attributs supplémentaires suivants lorsque vous écrivez vos tests :
        //
        //Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test dans la classe
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Utilisez ClassCleanup pour exécuter du code après que tous les tests ont été exécutés dans une classe
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///Test pour Constructeur PicrossPuzzle
        ///</summary>
        [TestMethod()]
        public void PicrossPuzzleConstructorTest()
        {
            string filename = string.Empty; // TODO: initialisez à une valeur appropriée
            //PicrossPuzzle target = new PicrossPuzzle(filename);
            Assert.Inconclusive("TODO: implémentez le code pour vérifier la cible");
        }

        /// <summary>
        ///Test pour Constructeur PicrossPuzzle
        ///</summary>
        [TestMethod()]
        public void PicrossPuzzleConstructorTest1()
        {
            Bitmap img = null; // TODO: initialisez à une valeur appropriée
            Nullable<Size> size = new Nullable<Size>(); // TODO: initialisez à une valeur appropriée
            int threshold = 0; // TODO: initialisez à une valeur appropriée
            PicrossPuzzle target = new PicrossPuzzle(img, size, threshold);
            Assert.Inconclusive("TODO: implémentez le code pour vérifier la cible");
        }

        /// <summary>
        ///Test pour Constructeur PicrossPuzzle
        ///</summary>
        [TestMethod()]
        public void PicrossPuzzleConstructorTest2()
        {
            PicrossPuzzle puzzle = null; // TODO: initialisez à une valeur appropriée
            PicrossPuzzle target = new PicrossPuzzle(puzzle);
            Assert.Inconclusive("TODO: implémentez le code pour vérifier la cible");
        }

        /// <summary>
        ///Test pour Constructeur PicrossPuzzle
        ///</summary>
        [TestMethod()]
        public void PicrossPuzzleConstructorTest3()
        {
            PicrossPuzzle.CellValue[,] cells = null; // TODO: initialisez à une valeur appropriée
            int[][] linesValues = null; // TODO: initialisez à une valeur appropriée
            int[][] columnsValues = null; // TODO: initialisez à une valeur appropriée
            PicrossPuzzle target = new PicrossPuzzle(cells, linesValues, columnsValues);
            Assert.Inconclusive("TODO: implémentez le code pour vérifier la cible");
        }

        /// <summary>
        ///Test pour BitmapToCellsValueArray
        ///</summary>
        [TestMethod()]
        [DeploymentItem("PicrossCJL.dll")]
        public void BitmapToCellsValueArrayTest()
        {
            PrivateObject param0 = null; // TODO: initialisez à une valeur appropriée
            PicrossPuzzle_Accessor target = new PicrossPuzzle_Accessor(param0); // TODO: initialisez à une valeur appropriée
            Bitmap bmp = null; // TODO: initialisez à une valeur appropriée
            int threshold = 0; // TODO: initialisez à une valeur appropriée
            PicrossPuzzle.CellValue[,] expected = null; // TODO: initialisez à une valeur appropriée
            PicrossPuzzle.CellValue[,] actual;
            //actual = target.BitmapToCellsValueArray(bmp, threshold);
            //Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }

        /// <summary>
        ///Test pour BitmapToColumnsValue
        ///</summary>
        [TestMethod()]
        public void BitmapToColumnsValueTest()
        {
            PicrossPuzzle puzzle = new PicrossPuzzle(new PicrossPuzzle.CellValue[4, 4], new int[4][], new int[4][]);

            // Set values for the 4x4 CellValue array
            puzzle.Cells[0, 0] = PicrossPuzzle.CellValue.Filled;
            puzzle.Cells[1, 0] = PicrossPuzzle.CellValue.Filled;
            puzzle.Cells[2, 0] = PicrossPuzzle.CellValue.Empty;
            puzzle.Cells[3, 0] = PicrossPuzzle.CellValue.Filled;

            puzzle.Cells[0, 1] = PicrossPuzzle.CellValue.Filled;
            puzzle.Cells[1, 1] = PicrossPuzzle.CellValue.Crossed;
            puzzle.Cells[2, 1] = PicrossPuzzle.CellValue.Filled;
            puzzle.Cells[3, 1] = PicrossPuzzle.CellValue.Filled;

            puzzle.Cells[0, 2] = PicrossPuzzle.CellValue.Empty;
            puzzle.Cells[1, 2] = PicrossPuzzle.CellValue.Filled;
            puzzle.Cells[2, 2] = PicrossPuzzle.CellValue.Crossed;
            puzzle.Cells[3, 2] = PicrossPuzzle.CellValue.Crossed;

            puzzle.Cells[0, 3] = PicrossPuzzle.CellValue.Filled;
            puzzle.Cells[1, 3] = PicrossPuzzle.CellValue.Filled;
            puzzle.Cells[2, 3] = PicrossPuzzle.CellValue.Filled;
            puzzle.Cells[3, 3] = PicrossPuzzle.CellValue.Empty;

            int[][] columnValues = puzzle.BitmapToColumnsValue(puzzle.Cells);

            // Column 0
            Assert.AreEqual(2, columnValues[0][0]);
            Assert.AreEqual(1, columnValues[0][1]);

            // Column 1
            Assert.AreEqual(1, columnValues[1][0]);
            Assert.AreEqual(2, columnValues[1][1]);

            // Column 2
            Assert.AreEqual(1, columnValues[2][0]);
            Assert.AreEqual(1, columnValues[2][1]);

            // Column 3
            Assert.AreEqual(2, columnValues[3][0]);
        }

        /// <summary>
        ///Test pour BitmapToLinesValue
        ///</summary>
        [TestMethod()]
        public void BitmapToLinesValueTest()
        {
            PicrossPuzzle puzzle = new PicrossPuzzle(new PicrossPuzzle.CellValue[4, 4], new int[4][], new int[4][]);

            // Set values for the 4x4 CellValue array
            puzzle.Cells[0, 0] = PicrossPuzzle.CellValue.Filled;
            puzzle.Cells[1, 0] = PicrossPuzzle.CellValue.Filled;
            puzzle.Cells[2, 0] = PicrossPuzzle.CellValue.Empty;
            puzzle.Cells[3, 0] = PicrossPuzzle.CellValue.Filled;

            puzzle.Cells[0, 1] = PicrossPuzzle.CellValue.Filled;
            puzzle.Cells[1, 1] = PicrossPuzzle.CellValue.Crossed;
            puzzle.Cells[2, 1] = PicrossPuzzle.CellValue.Filled;
            puzzle.Cells[3, 1] = PicrossPuzzle.CellValue.Filled;

            puzzle.Cells[0, 2] = PicrossPuzzle.CellValue.Empty;
            puzzle.Cells[1, 2] = PicrossPuzzle.CellValue.Filled;
            puzzle.Cells[2, 2] = PicrossPuzzle.CellValue.Crossed;
            puzzle.Cells[3, 2] = PicrossPuzzle.CellValue.Crossed;

            puzzle.Cells[0, 3] = PicrossPuzzle.CellValue.Filled;
            puzzle.Cells[1, 3] = PicrossPuzzle.CellValue.Filled;
            puzzle.Cells[2, 3] = PicrossPuzzle.CellValue.Filled;
            puzzle.Cells[3, 3] = PicrossPuzzle.CellValue.Empty;

            int[][] linesValues = puzzle.BitmapToLinesValue(puzzle.Cells);

            // Line 0
            Assert.AreEqual(2, linesValues[0][0]);
            Assert.AreEqual(1, linesValues[0][1]);

            // Line 1
            Assert.AreEqual(1, linesValues[1][0]);
            Assert.AreEqual(2, linesValues[1][1]);

            // Line 2
            Assert.AreEqual(1, linesValues[2][0]);

            // Line 3
            Assert.AreEqual(3, linesValues[3][0]);
        }

        /// <summary>
        ///Test pour CheckPuzzleColumn
        ///</summary>
        [TestMethod()]
        public void CheckPuzzleColumnTest()
        {
            PicrossPuzzle puzzle = null; // TODO: initialisez à une valeur appropriée
            PicrossPuzzle target = new PicrossPuzzle(puzzle); // TODO: initialisez à une valeur appropriée
            int columnNo = 0; // TODO: initialisez à une valeur appropriée
            PicrossPuzzle.PuzzleState expected = new PicrossPuzzle.PuzzleState(); // TODO: initialisez à une valeur appropriée
            PicrossPuzzle.PuzzleState actual;
            actual = target.CheckPuzzleColumn(columnNo);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }

        /// <summary>
        ///Test pour CheckPuzzleLine
        ///</summary>
        [TestMethod()]
        public void CheckPuzzleLineTest()
        {
            PicrossPuzzle puzzle = null; // TODO: initialisez à une valeur appropriée
            PicrossPuzzle target = new PicrossPuzzle(puzzle); // TODO: initialisez à une valeur appropriée
            int lineNo = 0; // TODO: initialisez à une valeur appropriée
            PicrossPuzzle.PuzzleState expected = new PicrossPuzzle.PuzzleState(); // TODO: initialisez à une valeur appropriée
            PicrossPuzzle.PuzzleState actual;
            actual = target.CheckPuzzleLine(lineNo);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }

        /// <summary>
        ///Test pour GetPuzzleState
        ///</summary>
        [TestMethod()]
        public void GetPuzzleStateTest()
        {
            PicrossPuzzle puzzle = null; // TODO: initialisez à une valeur appropriée
            PicrossPuzzle target = new PicrossPuzzle(puzzle); // TODO: initialisez à une valeur appropriée
            PicrossPuzzle.PuzzleState expected = new PicrossPuzzle.PuzzleState(); // TODO: initialisez à une valeur appropriée
            PicrossPuzzle.PuzzleState actual;
            actual = target.GetPuzzleState();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }

        /// <summary>
        ///Test pour LoadFromFile
        ///</summary>
        [TestMethod()]
        public void LoadFromFileTest()
        {
            PicrossPuzzle puzzle = null; // TODO: initialisez à une valeur appropriée
            PicrossPuzzle target = new PicrossPuzzle(puzzle); // TODO: initialisez à une valeur appropriée
            string filename = string.Empty; // TODO: initialisez à une valeur appropriée
            //target.LoadFromFile(filename);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour SaveToFile
        ///</summary>
        [TestMethod()]
        public void SaveToFileTest()
        {
            PicrossPuzzle puzzle = null; // TODO: initialisez à une valeur appropriée
            PicrossPuzzle target = new PicrossPuzzle(puzzle); // TODO: initialisez à une valeur appropriée
            string filename = string.Empty; // TODO: initialisez à une valeur appropriée
            target.SaveToFile(filename);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            PicrossPuzzle puzzle = null; // TODO: initialisez à une valeur appropriée
            PicrossPuzzle target = new PicrossPuzzle(puzzle); // TODO: initialisez à une valeur appropriée
            string expected = string.Empty; // TODO: initialisez à une valeur appropriée
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }

        /// <summary>
        ///Test pour Cells
        ///</summary>
        [TestMethod()]
        public void CellsTest()
        {
            PicrossPuzzle puzzle = null; // TODO: initialisez à une valeur appropriée
            PicrossPuzzle target = new PicrossPuzzle(puzzle); // TODO: initialisez à une valeur appropriée
            PicrossPuzzle.CellValue[,] expected = null; // TODO: initialisez à une valeur appropriée
            PicrossPuzzle.CellValue[,] actual;
            target.Cells = expected;
            actual = target.Cells;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }

        /// <summary>
        ///Test pour ColumnsValues
        ///</summary>
        [TestMethod()]
        [DeploymentItem("PicrossCJL.dll")]
        public void ColumnsValuesTest()
        {
            PrivateObject param0 = null; // TODO: initialisez à une valeur appropriée
            PicrossPuzzle_Accessor target = new PicrossPuzzle_Accessor(param0); // TODO: initialisez à une valeur appropriée
            int[][] expected = null; // TODO: initialisez à une valeur appropriée
            int[][] actual;
            target.ColumnsValues = expected;
            actual = target.ColumnsValues;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }

        /// <summary>
        ///Test pour LinesValues
        ///</summary>
        [TestMethod()]
        [DeploymentItem("PicrossCJL.dll")]
        public void LinesValuesTest()
        {
            PrivateObject param0 = null; // TODO: initialisez à une valeur appropriée
            PicrossPuzzle_Accessor target = new PicrossPuzzle_Accessor(param0); // TODO: initialisez à une valeur appropriée
            int[][] expected = null; // TODO: initialisez à une valeur appropriée
            int[][] actual;
            target.LinesValues = expected;
            actual = target.LinesValues;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }

        /// <summary>
        ///Test pour Size
        ///</summary>
        [TestMethod()]
        public void SizeTest()
        {
            PicrossPuzzle puzzle = null; // TODO: initialisez à une valeur appropriée
            PicrossPuzzle target = new PicrossPuzzle(puzzle); // TODO: initialisez à une valeur appropriée
            Size actual;
            actual = target.Size;
            Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }
    }
}
