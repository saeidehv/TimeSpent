using System;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Test
{
    [TestClass]
    public class ObjectBaseTests
    {
        [TestMethod]
        public void test_clean_property_change()
        {
            TestClass obj = new TestClass();
            bool isPropertyChanged = false;

            obj.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "CleanProp")
                    isPropertyChanged = true;
            };

            obj.CleanProp = "tested";

            Assert.IsTrue(isPropertyChanged);
        }

        [TestMethod]
        public void test_dirty_set()
        {
            TestClass obj = new TestClass();
            Assert.IsFalse(obj.IsDirty, "Object should be clean");

            obj.DirtyProp = "test";
            Assert.IsTrue(obj.IsDirty, "Object should be dirty");
        }


        [TestMethod]
        public void test_property_change_single_subscription()
        {
            TestClass obj = new TestClass();
            int counter = 0;

            PropertyChangedEventHandler handler1 = new
                PropertyChangedEventHandler((s, e) => { counter++; });

            PropertyChangedEventHandler handler2 = new
                PropertyChangedEventHandler((s, e) => { counter++; });


            obj.PropertyChanged += handler1;
            obj.PropertyChanged += handler1;
            obj.PropertyChanged += handler2;
            obj.PropertyChanged += handler2;
            obj.PropertyChanged += handler1;

            obj.CleanProp = "test";

            Assert.IsTrue(counter == 2);

        }

        [TestMethod]
        public void test_object_validation()
        {
            TestClass obj = new TestClass();
            Assert.IsFalse(obj.IsValid);

            obj.StringProp = "test";
            Assert.IsTrue(obj.IsValid);
        }
    }
}
