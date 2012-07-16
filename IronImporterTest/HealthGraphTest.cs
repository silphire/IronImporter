using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IronImporter;

namespace IronImporterTest
{
    /// <summary>
    /// HealthGraphTest の概要の説明
    /// </summary>
    [TestClass]
    public class HealthGraphTest : HealthGraph
    {
        public HealthGraphTest()
        {
            //
            // TODO: コンストラクタ ロジックをここに追加します
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///現在のテストの実行についての情報および機能を
        ///提供するテスト コンテキストを取得または設定します。
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

        #region 追加のテスト属性
        //
        // テストを作成する際には、次の追加属性を使用できます:
        //
        // クラス内で最初のテストを実行する前に、ClassInitialize を使用してコードを実行してください
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // クラス内のテストをすべて実行したら、ClassCleanup を使用してコードを実行してください
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 各テストを実行する前に、TestInitialize を使用してコードを実行してください
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 各テストを実行した後に、TestCleanup を使用してコードを実行してください
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMinimum()
        {
            HealthGraph obj = new HealthGraph();

            obj.Type = "Run";
            Assert.AreEqual("{\"type\":\"Run\"}", obj.ToJSON());
        }

        [TestMethod]
        public void TestPath()
        {
            HealthGraph obj = new HealthGraph();
            List<Path> path = new List<Path>();
            Path p;

            p = new Path();
            p.Timestamp = 0.0;
            p.Latitude = 130.0;
            p.Longitude = 140.0;
            path.Add(p);

            p = new Path();
            p.Timestamp = 10.0;
            p.Latitude = 140.0;
            p.Longitude = 140.0;
            path.Add(p);

            obj.Type = "Run";
            obj.Path = path;
            Assert.AreEqual("{\"type\":\"Run\",\"path\":[{\"timestamp\":0,\"latitude\":130,\"longitude\":140},{\"timestamp\":10,\"latitude\":140,\"longitude\":140}]}", obj.ToJSON());
        }
    }
}
