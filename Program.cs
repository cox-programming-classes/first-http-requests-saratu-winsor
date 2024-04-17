// See https://aka.ms/new-console-template for more information

using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using CS_First_HTTP_Client;

ScheduleService scheduleService = new(ApiService.Current);

await ApiService.Current.AuthenticateAsync(new ("saratu.waya@winsor.edu", "EGH966!@@chi"));
//var user = await ApiService.Current.SendAsync<UserInfo>(HttpMethod.Get, "api/users/self");
//Console.WriteLine(user);

/*
var cycleDays = await ApiService.Current.SendAsync<ImmutableArray<CycleDay>>(HttpMethod.Get, "api/schedule/cycle-day");

foreach (var day in cycleDays)
{
    Console.WriteLine($"{day.date:dddd dd MMMM} is {day.cycleDay}");
}
*/

var schedule = await scheduleService.GetAcademicSchedule();
foreach (var Chemistry in schedule)
{
    Console.WriteLine(Chemistry);
}