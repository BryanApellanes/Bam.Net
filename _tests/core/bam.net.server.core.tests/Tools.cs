using Bam.Net.ServiceProxy;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Tests
{
    public static class Tools
    {
        public static Mock<IHttpContext> CreateMockContext(string url)
        {
            Mock<IHttpContext> mockContext = new Mock<IHttpContext>();
            Mock<IHttpContext> ctx = new Mock<IHttpContext>();
            FakeRequest fakeRequest = new FakeRequest();
            fakeRequest.SetUrl(url);
            ctx.SetupProperty<IRequest>(c => c.Request, fakeRequest);
            return ctx;
        }
    }
}
