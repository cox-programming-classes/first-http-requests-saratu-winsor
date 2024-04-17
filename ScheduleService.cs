using System.Collections.Immutable;

namespace CS_First_HTTP_Client;

public class ScheduleService
{
    private readonly ApiService _api; //dependency injection

    public ScheduleService(ApiService api)
    {
        _api = api;
    }

    public async Task<ImmutableArray<Schedule>> GetAcademicSchedule(bool detailed = false)
    {
        return await _api.SendAsync<ImmutableArray<Schedule>>(
            HttpMethod.Get, $"api/schedule/academics?{detailed}");
    }
} 