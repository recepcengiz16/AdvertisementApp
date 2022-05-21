using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.DataAccess.UnitOfWorks;
using AdvertisementApp.Dto;
using AdvertisementApp.Entities;
using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Services
{
    public class ProvidedService_Service : Service<ProvidedServiceCreateDto, ProvidedServiceUpdateDto, ProviderServiceListDto, ProvidedService>, IProvidedService_Service
    {
        public ProvidedService_Service(IMapper mapper, IValidator<ProvidedServiceCreateDto> createDtoValidator, IValidator<ProvidedServiceUpdateDto> updateDtoValidator, IUnitOfWork unitOfWork) : base(mapper, createDtoValidator, updateDtoValidator, unitOfWork)
        {
        }
    }
}
