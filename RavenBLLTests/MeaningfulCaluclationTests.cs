using Microsoft.VisualStudio.TestTools.UnitTesting;
using RavenBLL;
using RavenMVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenBLL.Tests
{
    [TestClass()]
    public class MeaningfulCaluclationTests
    {
        //private int legalSpeed;

        [TestMethod()]
        public void Calculate_FineAmountTest_Not_Speeding()
        {
            // arrange
            MeaningfulCaluclation calc = new MeaningfulCaluclation();
            decimal expectedFine = 0;
            int history = 0;
            int recordedSpeed = 50;
            

            // act
            decimal actualFine = calc.Calculate_FineAmount(recordedSpeed, MagiConstants.LegalSpeed,history );
            // assert
            Assert.AreEqual(expectedFine, actualFine);
        }
        [TestMethod()]
        public void Calculate_FineAmountTest_11MilesOver()
        {
            // arrange
            MeaningfulCaluclation calc = new MeaningfulCaluclation();
            decimal expectedFine = 90;
            int history = 0;
            int recordedSpeed = 62;
            

            // act
            decimal actualFine = calc.Calculate_FineAmount(recordedSpeed, MagiConstants.LegalSpeed,history);
            // assert
            Assert.AreEqual(expectedFine, actualFine);
        }
        [TestMethod()]
        public void Calculate_FineAmountTest_20MilesOver()
        {
            // arrange
            MeaningfulCaluclation calc = new MeaningfulCaluclation();
            decimal expectedFine = 500;
            int history = 0;
            int recordedSpeed = 70;


            // act
            decimal actualFine = calc.Calculate_FineAmount(recordedSpeed, MagiConstants.LegalSpeed, history);
            // assert
            Assert.AreEqual(expectedFine, actualFine);
        }
        [TestMethod()]
        public void Calculate_FineAmountTest_15MilesOver()
        {
            // arrange
            MeaningfulCaluclation calc = new MeaningfulCaluclation();
            decimal expectedFine = 500;
            int history = 0;
            int recordedSpeed = 65;


            // act
            decimal actualFine = calc.Calculate_FineAmount(recordedSpeed, MagiConstants.LegalSpeed, history);
            // assert
            Assert.AreEqual(expectedFine, actualFine);
        }
        [TestMethod()]
        public void Calculate_FineAmountTest_15MilesOver_WithOneHistory()
        {
            // arrange
            MeaningfulCaluclation calc = new MeaningfulCaluclation();
            decimal expectedFine = 1000;
            int history = 1;
            int recordedSpeed = 65;


            // act
            decimal actualFine = calc.Calculate_FineAmount(recordedSpeed, MagiConstants.LegalSpeed, history);
            // assert
            Assert.AreEqual(expectedFine, actualFine);
        }
        [TestMethod()]
        public void Calculate_FineAmountTest_11MilesOver_WithTwoHistory()
        {
            // arrange
            MeaningfulCaluclation calc = new MeaningfulCaluclation();
            decimal expectedFine = 270;
            int history = 2;
            int recordedSpeed = 61;


            // act
            decimal actualFine = calc.Calculate_FineAmount(recordedSpeed, MagiConstants.LegalSpeed, history);
            // assert
            Assert.AreEqual(expectedFine, actualFine);
        }
        [TestMethod()]
        public void Calculate_FineAmountTest_11MilesOver_WithThreeHistory()
        {
            // arrange
            MeaningfulCaluclation calc = new MeaningfulCaluclation();
            decimal expectedFine = 770;
            int history = 3;
            int recordedSpeed = 61;


            // act
            decimal actualFine = calc.Calculate_FineAmount(recordedSpeed, MagiConstants.LegalSpeed, history);
            // assert
            Assert.AreEqual(expectedFine, actualFine);
        }
        [TestMethod()]
        public void Calculate_FineAmountTest_15MilesOver_WithTwoHistory()
        {
            // arrange
            MeaningfulCaluclation calc = new MeaningfulCaluclation();
            decimal expectedFine =1500;
            int history = 2;
            int recordedSpeed = 70;


            // act
            decimal actualFine = calc.Calculate_FineAmount(recordedSpeed, MagiConstants.LegalSpeed, history);
            // assert
            Assert.AreEqual(expectedFine, actualFine);
        }
    }
    //    [TestMethod()]
    //    public void Calculate_FineAmountTest_Speeding_Over11()
    //    {
    //        // arrange
    //        MeaningfulCaluclation calc = new MeaningfulCaluclation();
    //        decimal expectedFine = 90;
    //        int history = 0;
    //        int recordedSpeed = 61;

    //        // act
    //        decimal actualFine = calc.Calculate_FineAmount(recordedSpeed, history, MagiConstants.LegalSpeed);
    //        // assert
    //        Assert.AreEqual(expectedFine, actualFine);
    //    }
    //    [TestMethod()]
    //    public void Calculate_FineAmountTest_Speeding_Over15()
    //    {
    //        // arrange
    //        MeaningfulCaluclation calc = new MeaningfulCaluclation();
    //        decimal expectedFine = 250;
    //        int history = 0;
    //        int recordedSpeed = 65;

    //        // act
    //        decimal actualFine = calc.Calculate_FineAmount(recordedSpeed, history, MagiConstants.LegalSpeed);
    //        // assert
    //        Assert.AreEqual(expectedFine, actualFine);
    //    }
    //    [TestMethod()]
    //    public void Calculate_FineAmountTest_Speeding_Over11_One_History()
    //    {
    //        // arrange
    //        MeaningfulCaluclation calc = new MeaningfulCaluclation();
    //        decimal expectedFine = 180;
    //        int history = 1;
    //        int recordedSpeed = 61;

    //        // act
    //        decimal actualFine = calc.Calculate_FineAmount(recordedSpeed, history, MagiConstants.LegalSpeed);
    //        // assert
    //        Assert.AreEqual(expectedFine, actualFine);
    //    }
    //    [TestMethod()]
    //    public void Calculate_FineAmountTest_Speeding_Over11_Two_History()
    //    {
    //        // arrange
    //        MeaningfulCaluclation calc = new MeaningfulCaluclation();
    //        decimal expectedFine = 270;
    //        int history = 2;
    //        int recordedSpeed = 61;

    //        // act
    //        decimal actualFine = calc.Calculate_FineAmount(recordedSpeed, history, MagiConstants.LegalSpeed);
    //        // assert
    //        Assert.AreEqual(expectedFine, actualFine);
    //    }
    //    [TestMethod()]
    //    public void Calculate_FineAmountTest_Speeding_Over15_Four_History()
    //    {
    //        // arrange
    //        MeaningfulCaluclation calc = new MeaningfulCaluclation();
    //        decimal expectedFine = 2000;
    //        int history = 4;
    //        int recordedSpeed = 65;

    //        // act
    //        decimal actualFine = calc.Calculate_FineAmount(recordedSpeed, history, MagiConstants.LegalSpeed);
    //        // assert
    //        Assert.AreEqual(expectedFine, actualFine);
    //    }
    //}
}