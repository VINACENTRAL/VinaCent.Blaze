﻿using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using JetBrains.Annotations;
using System;
using VinaCent.Blaze.Users.Dto;

namespace VinaCent.Blaze.AppCore.AuditLogs.Dto
{
    /// <summary>
    ///
    /// </summary>
    [AutoMap(typeof(AuditLog))]
    public class AuditLogDto : AuditLogListDto
    {
        /// <summary>
        /// 
        /// </summary>
        [CanBeNull]
        public UserDto UserDto { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [CanBeNull]
        public UserDto ImpersonatorUserDto { get; set; }
    }
}