using AutoMapper;
using SJZ.Common.Domain;
using SJZ.Timelines.Domain.TimelineAggregate;
using SJZ.TimelineService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SJZ.TimelineService
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Timeline, TimelineDto>()
                .ForMember(t => t.TimelineId, o => o.MapFrom(s => s.Id))
                .ForMember(t => t.PeriodGroupLevel, o => o.MapFrom(s => Enumeration.ParseCodeToEnum<PeriodGroupLevelType>(s.PeriodGroupLevel)))
                .ForMember(t => t.OwnerId, o => o.MapFrom(s => s.CreatedBy))
                .ForMember(t => t.OwnerName, o => o.MapFrom(s => s.Username))
                .ForMember(t => t.LastChanged, o => o.MapFrom(s => s.UpdatedDate ?? s.CreatedDate));

            CreateMap<Record, RecordDto>()
                .ForMember(t => t.RecordId, o => o.MapFrom(s => s.Id));
        }
    }
}
