﻿// <copyright file="RestartProcessCommand.cs" company="Heath Stewart">
// Copyright (c) 2017 Heath Stewart
// See the LICENSE.txt file in the project root for more information.
// </copyright>

namespace RestartManager.PowerShell
{
    using System.Management.Automation;
    using RestartManager.Properties;

    /// <summary>
    /// The Restart-RestartManagerProcess cmdlet.
    /// </summary>
    [Cmdlet(VerbsLifecycle.Restart, Nouns.RestartManagerProcess)]
    public class RestartProcessCommand : Cmdlet
    {
        /// <summary>
        /// Gets or sets the <see cref="RestartManagerSession"/> with applications and services to restart.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public RestartManagerSession Session { get; set; }

        /// <inheritdoc/>
        protected override void EndProcessing()
        {
            base.EndProcessing();

            Session.RestartProgress += OnProgress;
            try
            {
                Session.RestartProcesses();
            }
            finally
            {
                Session.RestartProgress -= OnProgress;
            }
        }

        private void OnProgress(object sender, ProgressEventArgs e)
        {
            var progress = new ProgressRecord(GetHashCode(), Resources.Activity, Resources.RestartStatus)
            {
                PercentComplete = e.PercentComplete,
            };

            WriteProgress(progress);
        }
    }
}
