using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CountDown.Controllers;
using CountDown.Models.Domain;
using CountDown.Models.Repository;
using Moq;
using MvcPaging;
using Ninject;
using NUnit.Framework;

namespace CountDown.IntegrationTests.Controllers
{
    [TestFixture]
    public class A_HomeController_Object
    {
        private HomeController _sut;
        private Mock<IToDoItemRepository> _mockToDoItemRepository;

        [SetUp]
        public void SetUp()
        {
            _mockToDoItemRepository = new Mock<IToDoItemRepository>();
            _sut = new HomeController(_mockToDoItemRepository.Object);
        }

        [Test]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(9)]
        [TestCase(15)]
        [TestCase(8)]
        [Category("Integration Tests: Feature 6")]
        public void Should_Return_A_List_Of_ToDo_Items_That_Does_Not_Exceed_The_Max_Per_Page(int numItems)
        {
            _sut.ControllerContext = IntegrationTestHelper.GetMockControllerContext(true);
            List<ToDoItem> items = new List<ToDoItem>();
            for (int i = 1; i <= numItems; i++)
            {
                items.Add(ControllerTestsSetUp.Kernel.Get<ToDoItem>());
            }
            _mockToDoItemRepository.Setup(
                x =>
                    x.GetPagedToDoItems(0, HomeController.PageSize, It.IsAny<long>(), It.IsAny<bool>(),
                        It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(new PagedList<ToDoItem>(items, 0, HomeController.PageSize));

            var result = _sut.Index(null, null, null, null, null) as ViewResult;
            var count = (result.Model as IPagedList<ToDoItem>).Count();

            Assert.That(count, Is.LessThanOrEqualTo(HomeController.PageSize));
        }

        [Test]
        [TestCase(1, 10)]
        [TestCase(1, 9)]
        [TestCase(2, 20)]
        [TestCase(2, 19)]
        [TestCase(2, 21)]
        [Category("Integration Tests: Feature 6")]
        public void Should_Return_The_Correct_List_Of_ToDo_Items_For_The_Current_Page(int page, int numItems)
        {
            _sut.ControllerContext = IntegrationTestHelper.GetMockControllerContext(true);
            List<ToDoItem> allItems = new List<ToDoItem>();
            for (int i = 1; i <= numItems; i++)
            {
                allItems.Add(ControllerTestsSetUp.Kernel.Get<ToDoItem>());
            }
            _mockToDoItemRepository.Setup(
                x =>
                    x.GetPagedToDoItems(page - 1, HomeController.PageSize, It.IsAny<long>(),
                        It.IsAny<bool>(),
                        It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(new PagedList<ToDoItem>(allItems, page, HomeController.PageSize, numItems));

            var result = _sut.Index(page, null, null, null, null) as ViewResult;
            var resultItems = (result.Model as List<ToDoItem>);

            // Verify that the PagedList contains the correct items
            int startIndex = page*HomeController.PageSize;
            int endIndex = numItems - startIndex < HomeController.PageSize
                ? numItems - startIndex - 1
                : HomeController.PageSize - 1;

            int resultItemsCounter = 0;
            for (int i = startIndex; i <= endIndex; i++)
            {
                Assert.That(allItems[i], Is.EqualTo(resultItems[resultItemsCounter]));
                resultItemsCounter++;
            }
        }
    }
}
