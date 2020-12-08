using AutoMapper;
using AutoMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Infrastructure.Mapper
{
    public class AutoMapperConfiguration
    {
        #region Fields

        private static IMapper _mapper;

        #endregion

        #region Properties

        public static IMapper Mapper
        {
            get => _mapper;
            set => _mapper = value;
        }

        #endregion

        #region Methods

        public static void Init(IMapperConfigurationExpression configurationExpression)
        {
            MapperConfiguration configuration = new MapperConfiguration(configurationExpression as MapperConfigurationExpression);
            Mapper = configuration.CreateMapper();
        }

        #endregion
    }
}
