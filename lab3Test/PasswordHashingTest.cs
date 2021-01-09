using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIG.PasswordHashingUtils;

namespace lab3Test
{
    [TestClass]
    public class PasswordHashingTest
    {
        [TestMethod]
        public void GetHashTest()
        {
            Assert.IsNotNull(PasswordHasher.GetHash("testpassword"));
        }

        [TestMethod]
        public void GetHashEmptyTest()
        {
            Assert.IsNotNull(PasswordHasher.GetHash(""));
        }

        [TestMethod]
        public void GetHashNullTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => PasswordHasher.GetHash(null));
        }

        [TestMethod]
        public void GetHashSpaceTest()
        {
            Assert.IsNotNull(PasswordHasher.GetHash("                 "));
        }

        [TestMethod]
        public void GetHashEqualTest()
        {
            Assert.AreEqual(PasswordHasher.GetHash("testpassword"), PasswordHasher.GetHash("testpassword"));
        }

        [TestMethod]
        public void GetHashNotEqualTest()
        {
            Assert.AreNotEqual(PasswordHasher.GetHash("testpassword"), PasswordHasher.GetHash("nottestpassword"));
        }

        [TestMethod]
        public void GetHashCaseTest()
        {
            Assert.AreNotEqual(PasswordHasher.GetHash("testpassword"), PasswordHasher.GetHash("TestPassword"));
        }

        [TestMethod]
        public void GetHashSpecialTest()
        {
            Assert.IsNotNull(PasswordHasher.GetHash("✔️✔️✔️"));
            Assert.IsNotNull(PasswordHasher.GetHash("123"));
            Assert.IsNotNull(PasswordHasher.GetHash("ґєїёэ"));
            Assert.IsNotNull(PasswordHasher.GetHash("?????."));
        }

        [TestMethod]
        public void GetHashAdlerTest()
        {
            Assert.IsNotNull(PasswordHasher.GetHash("testpassword", null, 1));
        }

        [TestMethod]
        public void GetHashAdlerEqualTest()
        {
            Assert.AreEqual(PasswordHasher.GetHash("testpassword", null, 1), PasswordHasher.GetHash("testpassword", null, 1));
        }

        [TestMethod]
        public void GetHashAdlerNotEqualTest()
        {
            Assert.AreNotEqual(PasswordHasher.GetHash("testpassword", null, 1), PasswordHasher.GetHash("testpassword", null, 32));
        }

        [TestMethod]
        public void GetHashSaltTest()
        {
            Assert.IsNotNull(PasswordHasher.GetHash("testpassword", "sweet", null));
        }

        [TestMethod]
        public void GetHashSaltEqualTest()
        {
            Assert.AreEqual(PasswordHasher.GetHash("testpassword", "sweet", null), PasswordHasher.GetHash("testpassword", "sweet", null));
        }

        [TestMethod]
        public void GetHashSaltNotEqualTest()
        {
            Assert.AreNotEqual(PasswordHasher.GetHash("testpassword", "sweet", null), PasswordHasher.GetHash("testpassword", "notsweet", null));
        }

        [TestMethod]
        public void GetHashFullTest()
        {
            Assert.IsNotNull(PasswordHasher.GetHash("testpassword", "sweet", 1));
        }

        [TestMethod]
        public void GetHashSaltEmptyTest()
        {
            Assert.IsNotNull(PasswordHasher.GetHash("testpassword", "", 1));
        }

        [TestMethod]
        public void GetHashSaltSpaceTest()
        {
            Assert.IsNotNull(PasswordHasher.GetHash("testpassword", "   ", 1));
        }

        [TestMethod]
        public void GetHashFullEqualTest()
        {
            Assert.AreEqual(PasswordHasher.GetHash("testpassword", "sweet", 1), PasswordHasher.GetHash("testpassword", "sweet", 1));
        }

        [TestMethod]
        public void GetHashLengthTest()
        {
            Assert.AreEqual(PasswordHasher.GetHash("testpassword", "sweet", 1).Length, 64);
        }

        [TestMethod]
        public void GetHashEqualLengthTest()
        {
            Assert.AreEqual(PasswordHasher.GetHash("testpassword", "sweet", 1).Length, (PasswordHasher.GetHash("nottestpassword").Length));
        }

        [TestMethod]
        public void GetHashDiffLengthTest()
        {
            Assert.AreEqual(PasswordHasher.GetHash("testpassword", "bitter", 32).Length, PasswordHasher.GetHash("nottestpassword", "sweet", 1).Length);
        }

        [TestMethod]
        public void InitTest()
        {
            PasswordHasher.Init("sweet", 32);
            Assert.IsNotNull(PasswordHasher.GetHash("password", "sweet", 32));
        }

        [TestMethod]
        public void InitEmptyTest()
        {
            PasswordHasher.Init("", 32);
            Assert.IsNotNull(PasswordHasher.GetHash("", "", 32));
        }
        public void InitSpaceTest()
        {
            PasswordHasher.Init("     ", 32);
            Assert.IsNotNull(PasswordHasher.GetHash("   ", "     ", 32));
        }

        [TestMethod]
        public void InitEqualTest()
        {
            Assert.AreNotEqual(PasswordHasher.GetHash("password"), PasswordHasher.GetHash("password", "sweet", 1)); 
            PasswordHasher.Init("sweet", 1);
            Assert.AreEqual(PasswordHasher.GetHash("password"), PasswordHasher.GetHash("password", "sweet", 1));
        }

        [TestMethod]
        public void InitEqualSecTest()
        {
            PasswordHasher.Init(null, 32);
            Assert.AreEqual(PasswordHasher.GetHash("password"), PasswordHasher.GetHash("password", null, 32));
        }
    }
}
