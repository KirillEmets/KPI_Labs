using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using IIG.PasswordHashingUtils;

namespace Lab2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Default()
        {
            String password = "123456";
            String res = PasswordHasher.GetHash(password);
            
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void MultiByte()
        {
            String password = "123♥日本語";
            String res = PasswordHasher.GetHash(password);

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void EmptyPassword()
        {
            String password = String.Empty;
            String res = PasswordHasher.GetHash(password);

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void SamePasswordsSameResult()
        {
            String password1 = "123456";
            String password2 = "123456";
            String res1 = PasswordHasher.GetHash(password1);
            String res2 = PasswordHasher.GetHash(password2);

            Assert.IsNotNull(res1);
            Assert.AreEqual(res1, res2);
        }

        [TestMethod]
        public void DifferentPasswordsDifferentResult()
        {
            String password1 = "123456";
            String password2 = "123450";
            String res1 = PasswordHasher.GetHash(password1);
            String res2 = PasswordHasher.GetHash(password2);

            Assert.IsNotNull(res1);
            Assert.AreNotEqual(res1, res2);
        }

        [TestMethod]
        public void NullPasswordNullResult()
        {
            String password1 = null;
            var res1 = PasswordHasher.GetHash(password1);

            Assert.IsNull(res1);
        }

        [TestMethod]
        public void WithSaltIsDifferentFromDefault()
        {
            String salt = "salt";
            String password = "123456";

            var res1 = PasswordHasher.GetHash(password);
            var res2 = PasswordHasher.GetHash(password, salt);
            
            Assert.AreNotEqual(res1, res2);
        }

        [TestMethod]
        public void WithSaltIsDifferentFromDefaultReverseOrder()
        {
            String salt = "salt";
            String password = "123456";

            var res1 = PasswordHasher.GetHash(password, salt);
            var res2 = PasswordHasher.GetHash(password);
            // Если сначала вызвать getHash с salt, а потом без, значения выходят одинаковые

            Assert.AreNotEqual(res1, res2);
        }

        [TestMethod]
        public void EmptySaltSameAsDefault()
        {
            String salt = string.Empty;
            String password = "123456";

            var res1 = PasswordHasher.GetHash(password);
            var res2 = PasswordHasher.GetHash(password, salt);

            Assert.AreEqual(res1, res2);
        }

        [TestMethod]
        public void SameSaltSameModSameResults()
        {
            String salt = "somesalt";
            uint mod1 = 8;
            uint mod2 = 8;

            String password = "123456";

            var res1 = PasswordHasher.GetHash(password, salt, mod1);
            var res2 = PasswordHasher.GetHash(password, salt, mod2);

            Assert.AreEqual(res1, res2);
        }

        [TestMethod]
        public void SameSaltDifferentModDifferentResults()
        {
            String salt = "somesalt";
            uint mod1 = 8;
            uint mod2 = 7;

            String password = "123456";

            var res1 = PasswordHasher.GetHash(password, salt, mod1);
            var res2 = PasswordHasher.GetHash(password, salt, mod2);

            Assert.AreNotEqual(res1, res2);
        }

        [TestMethod]
        public void DifferentSaltSameModDifferentResults()
        {
            String salt1 = "somesalt1";
            String salt2 = "two";
            uint mod = 8;

            String password = "123456";

            var res1 = PasswordHasher.GetHash(password, salt1, mod);
            var res2 = PasswordHasher.GetHash(password, salt2, mod);

            Assert.AreNotEqual(res1, res2);
        }

        [TestMethod]
        public void SameInitSameRsults()
        {
            String salt = "salt";
            uint mod = 4;
            String password = "123456";

            PasswordHasher.Init(salt, mod);
            var res1 = PasswordHasher.GetHash(password);

            PasswordHasher.Init(salt, mod);
            var res2 = PasswordHasher.GetHash(password);

            Assert.AreEqual(res1, res2);
        }

        [TestMethod]
        public void DifferentInitDifferentRsults()
        {
            String salt1 = "salt";
            uint mod1 = 4;
            String salt2 = "qwerty";
            uint mod2 = 3;
            String password = "123456";

            PasswordHasher.Init(salt1, mod1);
            var res1 = PasswordHasher.GetHash(password);

            PasswordHasher.Init(salt2, mod2);
            var res2 = PasswordHasher.GetHash(password);

            Assert.AreNotEqual(res1, res2);
        }

        [TestMethod]
        public void AdlerModMaxUint()
        {
            String salt = "salt";
            uint mod = uint.MaxValue;
            String password = "123456";

            PasswordHasher.Init(salt, mod);
            var res = PasswordHasher.GetHash(password);

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void AdlerModMaxUintM1()
        {
            String salt = "salt";
            uint mod = uint.MaxValue - 1;
            String password = "123456";

            PasswordHasher.Init(salt, mod);
            var res = PasswordHasher.GetHash(password);

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void AdlerModMinUint()
        {
            String salt = "salt";
            uint mod = uint.MinValue;
            String password = "123456";
            
            PasswordHasher.Init(salt, mod);
            var res = PasswordHasher.GetHash(password);

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void AdlerModMinUintP1()
        {
            String salt = "salt";
            uint mod = uint.MinValue + 1;
            String password = "123456";

            PasswordHasher.Init(salt, mod);
            var res = PasswordHasher.GetHash(password);

            Assert.IsNotNull(res);
        }
    }
}
