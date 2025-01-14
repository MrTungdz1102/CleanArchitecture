﻿using CleanArchitecture.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ApplicationCore.Interfaces.Commons
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateToken(string userName);
        Task<string> CreateRefreshToken();
      //  Task<ResponseDTO> VerifyRefreshToken();
    }
}
