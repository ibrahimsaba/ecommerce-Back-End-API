using AutoMapper;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class PictureUrlResolver : IValueResolver<Product, ProductResultDto, string>
    {
        private readonly IConfiguration configuration;

        public PictureUrlResolver(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public string Resolve(Product source, ProductResultDto destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.PictureUrl))
            {
                return string.Empty;
            }
            return $"{configuration["BaseUrl"]}/{source.PictureUrl}";
        }
    }
}
