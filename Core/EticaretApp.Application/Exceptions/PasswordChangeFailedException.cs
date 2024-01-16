using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Exceptions
{
    public class PasswordChangeFailedException : Exception
    {
        public PasswordChangeFailedException() : base("Şifre güncellenirken hata oluştu!")
        {
        }

        public PasswordChangeFailedException(string? message) : base(message)
        {
        }

        public PasswordChangeFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public PasswordChangeFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
