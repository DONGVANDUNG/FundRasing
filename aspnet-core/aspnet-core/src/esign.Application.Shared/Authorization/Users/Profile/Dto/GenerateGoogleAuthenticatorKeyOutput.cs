﻿using System.Collections.Generic;

namespace esign.Authorization.Users.Profile.Dto
{
    public class GenerateGoogleAuthenticatorKeyOutput
    {
        public string QrCodeSetupImageUrl { get; set; }
        public string GoogleAuthenticatorKey { get; set; }
    }
}
