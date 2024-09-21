using AutoMapper;


namespace ApplicationLayer.AutoMapper
{
    public class AutoMapper<Source, Target>
    {
        private IMapper mapper;

        public AutoMapper()
        {
            mapper = new Mapper(new MapperConfiguration
               (config => config.CreateMap<Source, Target>().ReverseMap()));
        }

		public Target Map(Source convertableObj)
		{
			return this.mapper.Map<Target>(convertableObj);
		}

		public Source Map(Target convertableObj)
		{
			return this.mapper.Map<Source>(convertableObj);
		}

	}
}
