using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Infrastructure.Helper
{
    public static class MapperProfileHelper
    {
        private static IMapper? _mapper;

        /// <summary>
        /// Static instance of mapper to be used in some conversions
        /// </summary>
        public static IMapper? Mapper { get => _mapper; set => _mapper = value; }
    }
}