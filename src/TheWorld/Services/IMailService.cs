﻿namespace TheWorld.Services
{
    public interface IMailService
    {
        bool SendMail(string to, string from, string subect, string body);

    }
}
