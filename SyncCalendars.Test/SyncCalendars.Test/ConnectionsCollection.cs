﻿using Synchronizer;
using System;
using System.Collections.Generic;

namespace SyncCalendars.Test
{
    public static class ConnectionsCollection
    {
        public static List<MainSyncItem> GetConnectionForUpdating()
        {
            var connection = new List<MainSyncItem>();

            connection.Add(new MainSyncItem
            {
                GoogleId = "google_1",
                OutlookId = "outlook_1"
            });

            connection.Add(new MainSyncItem
            {
                GoogleId = "google_2",
                OutlookId = "outlook_2"
            });

            return connection;
        }

        public static List<MainSyncItem> GetConnectionForAdding()
        {
            var connection = new List<MainSyncItem>();

            return connection;
        }

        public static List<MainSyncItem> GetConnectionForDeleting()
        {
            var connection = new List<MainSyncItem>();

            connection.Add(new MainSyncItem
            {
                GoogleId = "google_1",
                OutlookId = "outlook_1"
            });

            connection.Add(new MainSyncItem
            {
                GoogleId = "google_2",
                OutlookId = "deletedOutlook_2"
            });

            connection.Add(new MainSyncItem
            {
                GoogleId = "deletedGoogle_2",
                OutlookId = "outlook_2"
            });
            return connection;
        }

    }
}
