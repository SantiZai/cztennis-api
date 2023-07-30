using System.Runtime.InteropServices;

namespace api
{
    public class PaymentResponse
    {
        private long id;
        private string status;
        private string detail;

        public PaymentResponse(long id, string status, string detail)
        {
            this.id = id;
            this.status = status;
            this.detail = detail;
        }

        public long GetId()
        {
            return id;
        }

        public void SetId(long id)
        {
            this.id = id;
        }

        public String GetStatus()
        {
            return status;
        }

        public void SetStatus(string status)
        {
            this.status = status;
        }

        public String GetDetail()
        {
            return detail;
        }

        public void SetDetail(string detail)
        {
            this.detail = detail;
        }
    }
}
