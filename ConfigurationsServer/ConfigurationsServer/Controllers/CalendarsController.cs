﻿using System.Collections.Generic;
using Calendars;
using Databases;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationsServer.Controllers
{
    [Route("api/[controller]/")]
    public class CalendarsController : Controller
    {
        [EnableCors("Policy")]
        [HttpGet("{user}")]
        public Calendar[] GetCalendars(string user)
        {
            var database = new MongoDatabase();
            var authorizeConfigs = database.GetAuthorizationParameters(user);

            var googleCalendar = new GoogleCalendar(authorizeConfigs.ToGoogle());
            var service = googleCalendar.GetService();

            var calendars = service.CalendarList.List().Execute();

            var calendarList = new List<Calendar>();

            foreach (var calendar in calendars.Items)
            {
                calendarList.Add(new Calendar()
                {
                    Id = calendar.Id,
                    Name = calendar.Summary
                });
            }
            return calendarList.ToArray();
        }

    }
}