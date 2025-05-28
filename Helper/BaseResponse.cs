using System.ComponentModel.DataAnnotations;

namespace billing_backend.Helper
{
    public class BaseResponse
    {
        private bool _success;

        [Required]
        public bool Success
        {
            get => _success;
            set
            {
                _success = value;
                Code = _success ? 200 : 400;
            }
        }

        [Required]
        public string Message { get; set; }

        [Required]
        public int Code { get; set; }

    }

    public class BaseResponseObjectModel<T>
    {
        private bool _success;

        [Required]
        public bool Success
        {
            get => _success;
            set
            {
                _success = value;
                Code = _success ? 200 : 400;
            }
        }

        [Required]
        public string? Message { get; set; }

        [Required]
        public int Code { get; set; }
        public T Data { get; set; }
    }
}
