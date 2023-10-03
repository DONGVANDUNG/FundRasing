﻿using System.Threading.Tasks;
using Abp.Application.Services;
using esign.Install.Dto;

namespace esign.Install
{
    public interface IInstallAppService : IApplicationService
    {
        Task Setup(InstallDto input);

        AppSettingsJsonDto GetAppSettingsJson();

        CheckDatabaseOutput CheckDatabase();
    }
}