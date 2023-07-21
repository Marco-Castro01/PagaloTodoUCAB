
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Tests.MockData
{
    public static class BuildDataContextFaker
    {
        public static Guid AdminRequestOK()
        {
            return new Guid("f1da2b15-922e-44ce-92bb-07b069b43dfc");
        }
        public static AdminResponse AdminResponseOK()
        {
            var adminResponse = new AdminResponse()
            {
                Id = new Guid("f1da2b15-922e-44ce-92bb-07b069b43dfc"),
                cedula = "29625888",
                nickName = "Bizmarck",
                status = true,
                email = "deigamerarteaga@gmail.com",
            };
            return adminResponse;
        }

        public static Guid DeleteCampoRequestOK()
        {
            return new Guid("f1da2b15-922e-44ce-92bb-07b069b43dfc");
        }

    }
}