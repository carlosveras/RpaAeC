namespace RpaAeC.Domain.Abstractions
{
    public class Training<TTraining>
    {
        public readonly TTraining? Value;

        public bool IsSuccess { get; set; }
        public bool IsFailure => !IsSuccess;
        public string? ErrorMessage { get; set; } = string.Empty;

        private Training(TTraining? value, bool isSuccess)
        {
            Value = value;
            IsSuccess = isSuccess;
        }

        private Training(string errorMessage, bool isSuccess)
        {
            Value = default;
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public static implicit operator Training<TTraining>(TTraining? value) => new(value, true);
        public static implicit operator Training<TTraining>(string errorMessage) => new(errorMessage, false);
    }
}
