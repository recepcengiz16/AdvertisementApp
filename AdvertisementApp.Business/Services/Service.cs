﻿using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.Business.ValidationExtensions;
using AdvertisementApp.Common;
using AdvertisementApp.DataAccess.UnitOfWorks;
using AdvertisementApp.Dto.Interfaces;
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
    public class Service<CreateDto, UpdateDto, ListDto,T> : IService<CreateDto, UpdateDto, ListDto,T>
        where CreateDto : class, IDto, new()
        where UpdateDto : class, IUpdateDto, new()
        where ListDto : class, IDto, new()
        where T:BaseEntity
    {
        private readonly IMapper _mapper;
        private readonly IValidator<CreateDto> _createDtoValidator;
        private readonly IValidator<UpdateDto> _updateDtoValidator;
        private readonly IUnitOfWork _unitOfWork;

        public Service(IMapper mapper, IValidator<CreateDto> createDtoValidator, IValidator<UpdateDto> updateDtoValidator, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _createDtoValidator = createDtoValidator;
            _updateDtoValidator = updateDtoValidator;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResponse<CreateDto>> CreateAsync(CreateDto dto)
        {
            var result = _createDtoValidator.Validate(dto);
            if (result.IsValid)
            {
                var createdEntity = _mapper.Map<T>(dto);
                await _unitOfWork.GetRepository<T>().CreateAsync(createdEntity);
                await _unitOfWork.SaveChangesAsync();
                return new Response<CreateDto>(ResponseType.Success, dto);
            }
            return new Response<CreateDto>(dto, result.ConvertToCustomValidationError());
        }

        public async Task<IResponse<List<ListDto>>> GetAllAsync()
        {
            var data = await _unitOfWork.GetRepository<T>().GetAllAsync();
            var dto = _mapper.Map<List<ListDto>>(data);
            return new Response<List<ListDto>>(ResponseType.Success, dto);
        }

        public async Task<IResponse<IDto>> GetByIdAsync<IDto>(int id)
        {
            var data = await _unitOfWork.GetRepository<T>().GetByFilterAsync(x => x.Id == id);
            if (data==null)
            {
                return new Response<IDto>(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
            }
            var dto = _mapper.Map<IDto>(data);
            return new Response<IDto>(ResponseType.Success, dto);
        }

        public async Task<IResponse> RemoveAsync(int id)
        {
            var data = await _unitOfWork.GetRepository<T>().FindAsync(id);
            if(data==null) return new Response(ResponseType.NotFound, $"{id} ye ait data bulunamadı");

            _unitOfWork.GetRepository<T>().Remove(data);
            await _unitOfWork.SaveChangesAsync();
            return new Response(ResponseType.Success);
        }

        public async Task<IResponse<UpdateDto>> UpdateAsync(UpdateDto dto)
        {
            var result = _updateDtoValidator.Validate(dto);
            if (result.IsValid)
            {
                var unchangedData = await _unitOfWork.GetRepository<T>().FindAsync(dto.Id);
                if (unchangedData == null) return new Response<UpdateDto>(ResponseType.NotFound, $"{dto.Id} ye ait data bulunamadı");

                var entity = _mapper.Map<T>(unchangedData);
                _unitOfWork.GetRepository<T>().Update(entity, unchangedData);
                await _unitOfWork.SaveChangesAsync();
                return new Response<UpdateDto>(ResponseType.Success, dto);
            }
            return new Response<UpdateDto>(dto, result.ConvertToCustomValidationError());
        }
    }
}
