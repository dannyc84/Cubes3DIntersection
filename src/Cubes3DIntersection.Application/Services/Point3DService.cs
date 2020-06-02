using Cubes3DIntersection.Application.Interfaces;
using Cubes3DIntersection.Application.Mappers;
using Cubes3DIntersection.Application.Models;
using Cubes3DIntersection.Core.Entities;
using Cubes3DIntersection.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cubes3DIntersection.Core.Interfaces;

namespace Cubes3DIntersection.Application.Services
{
    public class Point3DService : IPoint3DService
    {
        private readonly IGenericRepository<Point3D> _point3DRepository;
        private readonly IAppLogger<Point3DService> _logger;

        private const string serviceName = nameof(Point3DService);
        private const string withIdEqualTo = "with Id =";

        public Point3DService(IGenericRepository<Point3D> repo, IAppLogger<Point3DService> logger)
        {
            _point3DRepository = repo ?? throw new ArgumentNullException(nameof(repo));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<Point3DModel>> GetPoints3DList()
        {
            var pointsList = await _point3DRepository.GetAll();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<Point3DModel>>(pointsList);
            return mapped.ToList();
        }

        public async Task<Point3DModel> GetPointById(int point3DId)
        {
            var point = await _point3DRepository.GetById(point3DId);
            var mapped = ObjectMapper.Mapper.Map<Point3DModel>(point);
            return mapped;
        }

        public async Task<Point3DModel> Create(Point3DModel point3D)
        {
            await ValidateIfExist(point3D);

            var mappedEntity = ObjectMapper.Mapper.Map<Point3D>(point3D);
            if (mappedEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");

            var newEntity = await _point3DRepository.Create(mappedEntity);
            _logger.LogInformation($"Entity successfully added - {serviceName}");

            var newMappedEntity = ObjectMapper.Mapper.Map<Point3DModel>(newEntity);
            return newMappedEntity;
        }

        private async Task ValidateIfExist(Point3DModel point3D)
        {
            if (point3D == null) throw new ArgumentNullException(nameof(point3D));

            var existingEntity = await _point3DRepository.GetById(point3D.Id);
            if (existingEntity != null)
                throw new ApplicationException($"{point3D} with this id already exists");
        }

        public async Task Update(Point3DModel pointCoordinates)
        {
            var mappedEntity = ObjectMapper.Mapper.Map<Point3D>(pointCoordinates);
            if (mappedEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");

            await _point3DRepository.Update(mappedEntity);
            _logger.LogInformation($"Entity successfully updated - {serviceName}");
        }
    }
}