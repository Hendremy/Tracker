using Hendricé.Rémy.Poo.Tracker.Datas;
using Hendricé.Rémy.Poo.Tracker.Domains;
using Hendricé.Rémy.Poo.Tracker.Presentations;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Hendricé.Rémy.Poo.Tracker.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var viewMock = new Mock<IAuthenticateView>();
            var repoMock = new Mock<ITrackerRepository>();
            var authMock = new Mock<IAuthenticate>();
            var authPres = new AuthenticatePresenter(viewMock.Object, repoMock.Object, authMock.Object);
            var eventArgs = new AuthenticateEventArgs("D007", "DaniBond");
            //When

            repoMock.Setup(repo => repo.GetUsersCredentials()).Returns(new List<UserCredentials>());
            viewMock.Raise(v => v.AuthenticationTried += null, viewMock, eventArgs);

            //Then
            viewMock.Verify(v => v.ShowLoginError(It.IsAny<string>()), Times.Once);
        }
    }
}