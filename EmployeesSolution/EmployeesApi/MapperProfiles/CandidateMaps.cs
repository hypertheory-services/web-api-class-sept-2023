using AutoMapper;
using EmployeesApi.Data;

namespace EmployeesApi.MapperProfiles;

public class CandidateMaps : Profile
{
    public CandidateMaps()
    {
        CreateMap<CandidateRequestModel, CandidateEntity>()
            .ForMember(dest => dest.DateCreated, opts => opts.MapFrom(_ => DateTimeOffset.UtcNow))
            .ForMember(dest => dest.RequiredSalaryMin, opts => opts.MapFrom(src => src.RequiredSalaryMin!.Value))
            .ForMember(dest => dest.Status, opts => opts.MapFrom(_ => CandidateStatus.AwaitingManager));
           

        CreateMap<CandidateEntity, CandidateResponseModel>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id.ToString()));

        
    }
}

