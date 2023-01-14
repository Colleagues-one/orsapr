using System;
using System.Collections.Generic;
using ORSAPR.Model;
using Kompas6Constants3D;
using Kompas6API5;
using NUnit.Framework;

namespace ORSAPR.Tests
{
    [TestFixture]
    public class ORSAPR_Tests
    {    

        /// <summary>
        /// Тест валидации параметров
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="width"></param>
        /// <param name="length"></param>
        /// <param name="height"></param>
        /// <param name="bladeLength"></param>
        /// <param name="innerLength"></param>
        /// <param name="innerWidth"></param>
        [TestCase(false, double.NegativeInfinity, double.PositiveInfinity, double.NegativeInfinity,
            double.PositiveInfinity, double.NegativeInfinity, double.PositiveInfinity,
            TestName = "Тест валидации при бесконечных значениях")]
        [TestCase(false, double.MaxValue + 1, double.MinValue - 1, double.MinValue - 1,
            double.MaxValue + 1, double.MaxValue + 1, double.MinValue - 1,
            TestName = "Тест валидации при значениях болших или меньших допустимых.")]
        [TestCase(false, null, null, null, null, null, null,
            TestName = "Тест валидации при null значениях")]
        [TestCase(false, 0, 0, 0, 0, 0, 0, TestName = "Тест валидации при нулевых значениях.")]
        [TestCase(false, 1098, 10890, 898, 0.0000009, 0.10, 0.00005,
            TestName = "Тест валидации при недопустимых значениях.")]
        [TestCase(true, 30, 300, 24, 150, 75, 15, TestName = "Тест валидации при максимальных значениях.")]
        [TestCase(true, 15, 150, 12, 75, 37.5, 7.5, TestName = "Тест валидации при средних значениях.")]
        [TestCase(true, 10, 100, 6, 40, 10, 5, TestName = "Тест валидации при минимальных значениях.")]
        public void TestValidation(bool expected, double width, double length, double height,
            double bladeLength, double innerLength, double innerWidth)
        {
            bool actual = true;
            try
            {
                ChiselData chiselData = new ChiselData();
                chiselData.Width = width;
                chiselData.Length = length;
                chiselData.Height = height;
                chiselData.BladeLength = bladeLength;
                chiselData.InnerLength = innerLength;
                chiselData.InnerWidth = innerWidth;
            }
            catch
            {
                actual = false;
            }
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// тест зависимости длины от ширины
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="width"></param>
        /// <param name="length"></param>
        [TestCase(true, 15, 150, TestName = "Тест при среднем значении ширины и минимальном" +
            " значении длины.")]
        [TestCase(true, 15, 159, TestName = "Тест при минимальном значении ширины и" +
            " максимальном значении длины.")]
        [TestCase(false, 15, 149, TestName = "Тест при среднем значении ширины и значения" +
            " длины меньше минимума.")]
        [TestCase(false, 15, 160, TestName = "Тест при среднем ширины и значении длины" +
            " больше максимума.")]
        public void TestDependenceOfLengthOnWidth(bool expected, double width, double length)
        {
            bool actual = true;
            try
            {
                ChiselData chiselData = new ChiselData();
                chiselData.Width = width;
                chiselData.Length = length;
            }  
            catch
            {
                actual = false;
            }
             Assert.AreEqual(expected, actual);              
        }
         
        /// <summary>
        /// тест зависимости высоты от ширины
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        [TestCase(true, 15, 9, TestName = "Тест при среднем значении ширины и минимальном" +
            " значении высоты.")]
        [TestCase(true, 15, 12, TestName = "Тест при минимальном значении ширины и максимальном" +
            " значении высоты.")]
        [TestCase(false, 15, 8, TestName = "Тест при среднем значении ширины и значения высоты" +
            " меньше минимума.")]
        [TestCase(false, 15, 13, TestName = "Тест при среднем ширины и значении высоты больше" +
            " максимума.")]
        public void TestDependenceOfHeightOnWidth(bool expected, double width, double height)
        {
            bool actual = true;
            try
            {
                ChiselData chiselData = new ChiselData();
                chiselData.Width = width;
                chiselData.Height = height;
            }
            catch
            {
                actual = false;
            }
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// тест зависимости длины лезвия от длины зубила
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="length"></param>
        /// <param name="bladeLength"></param>
        [TestCase(true, 150, 60, TestName = "Тест при среднем значении длины и минимальном" +
            " значении длины лезвия.")]
        [TestCase(true, 150, 75, TestName = "Тест при минимальном значении длины и максимальном " +
            "значении длины лезвия.")]
        [TestCase(false, 150, 59, TestName = "Тест при среднем значении длины и значения длины" +
            " лезвия меньше минимума.")]
        [TestCase(false, 150, 76, TestName = "Тест при среднем длины и значении длины лезвия" +
            " больше максимума.")]
        public void TestDependenceOfBladeLengthOnLength(bool expected, double length, double bladeLength )
        {
            bool actual = true;
            try
            {
                ChiselData chiselData = new ChiselData();
                chiselData.Length = length;
                chiselData.BladeLength = bladeLength;
            }
            catch
            {
                actual = false;
            }
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// тест зависимости длины выреза от длины зубила
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="length"></param>
        /// <param name="innerLength"></param>
        [TestCase(true, 150, 10, TestName = "Тест при среднем значении длины и" +
            " минимальном значении длины выреза.")]
        [TestCase(true, 150, 37.5, TestName = "Тест при минимальном значении длины" +
            " и максимальном значении длины выреза.")]
        [TestCase(false, 150, 9, TestName = "Тест при среднем значении длины и" +
            " значения длины выреза меньше минимума.")]
        [TestCase(false, 150, 38, TestName = "Тест при среднем длины и значении" +
            " длины выреза больше максимума.")]
        public void TestDependenceOfInnerLengthOnLength(bool expected, double length, double innerLength)
        {
            bool actual = true;
            try
            {
                ChiselData chiselData = new ChiselData();
                chiselData.Length = length;
                chiselData.InnerLength = innerLength;
            }
            catch
            {
                actual = false;
            }
            Assert.AreEqual(expected, actual);
        }
        
        /// <summary>
        /// тест зависимости ширины выреза от ширины зубила и длины выреза зубила
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="width"></param>
        /// <param name="innerLength"></param>
        /// <param name="innerWidth"></param>
        [TestCase(true, 10, 10, 5, TestName = "Тест при минимальном значении ширины," +
            " минимальном значении длины выреза и допустимом значении ширины выреза.")]
        [TestCase(true, 10, 75, 5, TestName = "Тест при минимальном значении ширины, " +
            "максимальном значении длины выреза и допустимом значении ширины выреза.")]
        [TestCase(true, 30, 75, 15, TestName = "Тест при максимальном значении ширины," +
            " максимальном значении длины выреза и максимальном значении ширины выреза.")]
        [TestCase(false, 30, 75, 16, TestName = "Тест при максимальном значении ширины," +
            " максимальном значении длины выреза и значении ширины выреза больше допустимого.")]
        public void TestDependenceOfInnerWidthOnWidthAndInnerLength(bool expected,
            double width, double innerLength, double innerWidth)
        {
            bool actual = true;
            try
            {
                ChiselData chiselData = new ChiselData();
                chiselData.Width = width;
                chiselData.InnerWidth = innerWidth;
            }
            catch
            {
                actual = false;
            }
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// тест запуска компаса и создания 3д детали
        /// </summary>
        /// <param name="width"></param>
        /// <param name="length"></param>
        /// <param name="height"></param>
        /// <param name="bladeLength"></param>
        /// <param name="innerLength"></param>
        /// <param name="innerWidth"></param>
        [TestCase(10, 100, 6, 40, 10, 5, TestName = "Тест создания 3д документа.")]
        public void TestConstructKompasConnector(double width, double length, double height,
            double bladeLength, double innerLength, double innerWidth)
        {
            ChiselData chiselData = new ChiselData();
            chiselData.Width = width;
            chiselData.Length = length;
            chiselData.Height = height;
            chiselData.BladeLength = bladeLength;
            chiselData.InnerLength = innerLength;
            chiselData.InnerWidth = innerWidth;
            var app = new KompasConnector();
            Assert.IsTrue(app.CreateDocument3D());
        }

        /// <summary>
        /// тест создания модели столярного зубила
        /// </summary>
        /// <param name="width"></param>
        /// <param name="length"></param>
        /// <param name="height"></param>
        /// <param name="bladeLength"></param>
        /// <param name="innerLength"></param>
        /// <param name="innerWidth"></param>
        [TestCase(10, 100, 6, 40, 10, 5, TestName = "Тест создания модели детали столярного зубила.")]
        public void TestCreateDetailLocksmith(double width, double length, double height,
            double bladeLength, double innerLength, double innerWidth)
        {
            ChiselData chiselData = new ChiselData();
            chiselData.Width = width;
            chiselData.Length = length;
            chiselData.Height = height;
            chiselData.BladeLength = bladeLength;
            chiselData.InnerLength = innerLength;
            chiselData.InnerWidth = innerWidth;
            var app = new KompasConnector();
            app.CreateDocument3D();
            var manager = new Manager(app);
            try
            {

                manager.BuildModelLocksmith(chiselData);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {

                Assert.IsTrue(false);
            }

            app.DestructApp();
        }

        /// <summary>
        /// тест создания модели зубила пики
        /// </summary>
        /// <param name="width"></param>
        /// <param name="length"></param>
        /// <param name="height"></param>
        /// <param name="bladeLength"></param>
        /// <param name="innerLength"></param>
        /// <param name="innerWidth"></param>
        [TestCase(10, 100, 6, 40, 10, 5, TestName = "Тест создания модели детали зубила пики.")]
        public void TestCreateDetailPeak(double width, double length, double height,
            double bladeLength, double innerLength, double innerWidth)
        {
            ChiselData chiselData = new ChiselData();
            chiselData.Width = width;
            chiselData.Length = length;
            chiselData.Height = height;
            chiselData.BladeLength = bladeLength;
            chiselData.InnerLength = innerLength;
            chiselData.InnerWidth = innerWidth;
            var app = new KompasConnector();
            app.CreateDocument3D();
            var manager = new Manager(app);
            try
            {
                manager.BuildModelPeak(chiselData);
                Assert.IsTrue(true);
            }

            catch (Exception)
            {

                Assert.IsTrue(false);
            }
            app.DestructApp();
        }

        /// <summary>
        /// тест закрытия приложения компас
        /// </summary>
        /// <param name="width"></param>
        /// <param name="length"></param>
        /// <param name="height"></param>
        /// <param name="bladeLength"></param>
        /// <param name="innerLength"></param>
        /// <param name="innerWidth"></param>
        [TestCase(10, 100, 6, 40, 10, 5, TestName = "Тест закрытия приложения компас.")]
        public void TestDestructKompasConnector(double width, double length, double height,
            double bladeLength, double innerLength, double innerWidth)
        {
            try
            {
                ChiselData chiselData = new ChiselData();
                chiselData.Width = width;
                chiselData.Length = length;
                chiselData.Height = height;
                chiselData.BladeLength = bladeLength;
                chiselData.InnerLength = innerLength;
                chiselData.InnerWidth = innerWidth;
                var app = new KompasConnector();
                app.DestructApp();
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }

        }
    }
}
