using Moq;

using RESTApisDemo.Authentication;

namespace RESTAPIsDemo.Test.Authentication
{
    [TestFixture]
    public abstract class LoginControllerTestBase
    {
        protected LoginController? loginController;
        protected Mock<IAuthenticationManager>? authManagerMock;

        [SetUp]
        public void Setup ()
        {
            this.authManagerMock = new Mock<IAuthenticationManager> ();
            this.loginController = new LoginController (this.authManagerMock.Object);
        }

        [TearDown]
        public void TearDown ()
        {
            this.authManagerMock = null;
            this.loginController = null;
        }
    }
}