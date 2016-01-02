/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Encryption;
using Bam.Net.ServiceProxy;
using Bam.Net.Shop;
using Bam.Net.Data;
using System.Reflection;
using FakeItEasy;

namespace Bam.Net.Shop.Tests
{
    [Serializable]
    public class UnitTests : CommandLineTestInterface
    {
        public static void InitSchemas()
        {
            SQLiteRegistrar.Register<Shopper>();
            Db.TryEnsureSchema<Shopper>();
        }

        [UnitTest]
        public void ShouldBeAbleToGetCurrentShopper()
        {
            InitSchemas();
            string userName = MethodBase.GetCurrentMethod().Name;
            Shopper currentShopper = GetTestShopper(userName);
            Expect.AreEqual(userName, currentShopper.Name);
        }

        [UnitTest]
        public void ShouldBeAbleToAddItemToCart()
        {
            InitSchemas();
            Shopper currentShopper = GetTestShopper(MethodBase.GetCurrentMethod().Name.RandomLetters(8));
            ShopItem item = CreateTestItem();
            ShoppingCartInfo cartInfo = currentShopper.AddToShoppingCart(item.Id.Value);
            ShoppingCart cart = cartInfo.ToShoppingCart();
            Expect.AreEqual(1, cart.ShoppingCartItemsByShoppingCartId.Count);
            Expect.AreEqual(item.Name, cart.ShoppingCartItemsByShoppingCartId[0].ShopItemOfShopItemId.Name);
        }

        [UnitTest]
        public void ShouldBeAbleToGetCartFromCartInfo()
        {
            InitSchemas();
            Shopper currentShopper = GetTestShopper(MethodBase.GetCurrentMethod().Name.RandomLetters(8));
            ShoppingCartInfo cartInfo = currentShopper.GetShoppingCartInfo();
            ShoppingCart cart = cartInfo.ToShoppingCart();
            Expect.AreEqual(cartInfo.Id, cart.Id);
        }

        [UnitTest]
        public void CartInfoShouldHaveItemIds()
        {
            InitSchemas();
            Shopper currentShopper = GetTestShopper(MethodBase.GetCurrentMethod().Name.RandomLetters(8));
            ShopItem itemOne = CreateTestItem();
            ShopItem itemTwo = CreateTestItem();
            currentShopper.AddToShoppingCart(itemOne.Id.Value, itemTwo.Id.Value);
            ShoppingCartInfo cartInfo = currentShopper.GetShoppingCartInfo();
            Expect.AreEqual(2, cartInfo.ItemIds.Length);
            Expect.IsTrue(cartInfo.ItemIds.Contains(itemOne.Id.Value));
            Expect.IsTrue(cartInfo.ItemIds.Contains(itemTwo.Id.Value));
        }

        [UnitTest]
        public void ShouldBeAbleToRemoveItemFromCart()
        {
            InitSchemas();
            Shopper currentShopper = GetTestShopper(MethodBase.GetCurrentMethod().Name.RandomLetters(8));
            ShopItem itemOne = CreateTestItem();
            ShopItem itemTwo = CreateTestItem();
            currentShopper.AddToShoppingCart(itemOne.Id.Value, itemTwo.Id.Value);
            currentShopper.RemoveFromShoppingCart(itemOne.Id.Value);
            ShoppingCartInfo cartInfo = currentShopper.GetShoppingCartInfo();
            Expect.AreEqual(1, cartInfo.ItemIds.Length);
            Expect.AreEqual(itemTwo.Id, cartInfo.ItemIds[0]);
        }

        [UnitTest]
        public void ShouldBeAbleToGetById()
        {
            InitSchemas();
            ShopItem item = CreateTestItem();
            ShopItem retrieved = ShopItem.GetById(item.Id.Value);
            Expect.IsNotNull(retrieved);
            Expect.AreEqual(item, retrieved);
        }

        private static Shopper GetTestShopper(string userName)
        {
            IUserResolver resolver = A.Fake<IUserResolver>();
            A.CallTo(() => resolver.GetCurrentUser()).Returns(userName);
            Shopper testObject = new Shopper { UserResolver = resolver };
            Shopper currentShopper = testObject.GetCurrent();
            return currentShopper;
        }

        private static ShopItem CreateTestItem()
        {
            ShopItem item = new ShopItem();
            item.Name = "Test Item ({0})"._Format("".RandomLetters(5));
            item.Source = "Test Source ({0})"._Format("".RandomLetters(5));
            item.SourceId = "Test Source Id ({0})"._Format("".RandomLetters(5));
            //item.Price = 0;
            item.Save();
            return item;
        }

    }
}
