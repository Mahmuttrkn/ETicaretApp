using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Features.Commands.AppUser.verify_reset_token
{
    public class VerifyResetTokenCommandRequest: IRequest<VerifyResetTokenCommandResponse>
    {
        public string ResetToken { get; set; }
        public string UserId { get; set; }
    }
}
