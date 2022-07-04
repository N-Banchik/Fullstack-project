using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTOs.UpdateDtos
{
    public class PasswordChangeDto
    {
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
    }
}
