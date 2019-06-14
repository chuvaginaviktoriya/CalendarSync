﻿using System.Collections.Generic;
using Synchronizer.Models;

namespace SyncService
{
    public static class DbSync
    {
        public static void Synchronize(List<MainSyncItem> synchronizedItems, List<Calendar> calendars)
        {
            var listOfDeleted = new List<MainSyncItem>();

            foreach (var item in synchronizedItems)
                foreach (var calendar in calendars)
                    if (calendar.Type == CalendarType.Google && !calendar.Appointments.Exists(app => app.Id == item.GoogleId))
                            listOfDeleted.Add(item);

            foreach (var item in listOfDeleted)
                synchronizedItems.Remove(item);

            var addedConnections = new List<string>();

            foreach (var calendar in calendars)
            {
                foreach (var appointment in calendar.Appointments)
                    if (appointment.AppointmentStatus == Appointment.Status.New && !addedConnections.Contains(appointment.CreatorId))
                    {
                        synchronizedItems.Add(CreateNewConnection(calendars, appointment.CreatorId));
                        addedConnections.Add(appointment.CreatorId);
                    }
            }
        }
       
        public static MainSyncItem CreateNewConnection(List<Calendar> calendars, string creatorId)
        {
            var newItem = new MainSyncItem();

            foreach (var calendar in calendars)
                foreach (var appointment in calendar.Appointments)
                    if (appointment.Id == creatorId || appointment.CreatorId == creatorId)
                        newItem.AddConnection(appointment.Id, calendar.Type);

            return newItem;
        }
    }
}