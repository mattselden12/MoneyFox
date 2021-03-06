﻿using System.Globalization;
using MoneyFox.Business.Helpers;
using MoneyFox.Foundation.Resources;

namespace MoneyFox.Business.ViewModels.DesignTime
{
    public class DesignTimeSettingsBackgroundJobViewModel : ISettingsBackgroundJobViewModel
    {
        public DesignTimeSettingsBackgroundJobViewModel()
        {
            Resources = new LocalizedResources(typeof(Strings), CultureInfo.CurrentUICulture);
        }

        /// <inheritdoc />
        public LocalizedResources Resources { get; }

        public bool IsAutoBackupEnabled { get; } = true;
        public int BackupSyncRecurrence { get; } = 3;
    }
}
