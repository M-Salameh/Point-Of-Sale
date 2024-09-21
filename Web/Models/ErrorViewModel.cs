
namespace Web.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string ErrorMsg { get; set; }

        public string ErrorTraceBack { get; set; }
    }
}
